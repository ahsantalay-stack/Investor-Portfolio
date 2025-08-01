using Microsoft.AspNetCore.Mvc;
using PortfolioManagement.Application.DTOs;
using PortfolioManagement.Application.Interfaces;
using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Investors")]
public class InvestorsController : ControllerBase
{
    private readonly IInvestorService _investorService;
    private readonly ILogger<InvestorsController> _logger;

    public InvestorsController(IInvestorService investorService, ILogger<InvestorsController> logger)
    {
        _investorService = investorService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new investor account
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(InvestorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvestorDto>> CreateInvestor([FromBody] CreateInvestorDto createInvestorDto)
    {
        try
        {
            var investor = await _investorService.CreateInvestorAsync(createInvestorDto);
            return CreatedAtAction(nameof(GetInvestor), new { id = investor.Id }, investor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating investor");
            return BadRequest(new { message = "Error creating investor", details = ex.Message });
        }
    }

    /// <summary>
    /// Get investor by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InvestorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvestorDto>> GetInvestor(Guid id)
    {
        var investor = await _investorService.GetInvestorByIdAsync(id);
        return investor == null ? NotFound() : Ok(investor);
    }

    /// <summary>
    /// Get investor by email
    /// </summary>
    [HttpGet("by-email/{email}")]
    [ProducesResponseType(typeof(InvestorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvestorDto>> GetInvestorByEmail(string email)
    {
        var investor = await _investorService.GetInvestorByEmailAsync(email);
        return investor == null ? NotFound() : Ok(investor);
    }

    /// <summary>
    /// Get investor by National ID (KSA specific)
    /// </summary>
    [HttpGet("by-national-id/{nationalId}")]
    [ProducesResponseType(typeof(InvestorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvestorDto>> GetInvestorByNationalId(string nationalId)
    {
        var investor = await _investorService.GetInvestorByNationalIdAsync(nationalId);
        return investor == null ? NotFound() : Ok(investor);
    }

    /// <summary>
    /// Get investors with filtering and pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetInvestors(
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 50,
        [FromQuery] KycStatus? kycStatus = null,
        [FromQuery] string? searchTerm = null)
    {
        var (investors, totalCount) = await _investorService.GetInvestorsAsync(pageIndex, pageSize, kycStatus, searchTerm);
        
        return Ok(new
        {
            data = investors,
            totalCount,
            pageIndex,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }

    /// <summary>
    /// Update investor profile
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(InvestorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvestorDto>> UpdateInvestor(Guid id, [FromBody] UpdateInvestorDto updateInvestorDto)
    {
        try
        {
            var investor = await _investorService.UpdateInvestorAsync(id, updateInvestorDto);
            return Ok(investor);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating investor {InvestorId}", id);
            return BadRequest(new { message = "Error updating investor", details = ex.Message });
        }
    }

    /// <summary>
    /// Deactivate investor account
    /// </summary>
    [HttpPatch("{id}/deactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeactivateInvestor(Guid id)
    {
        var result = await _investorService.DeactivateInvestorAsync(id);
        return result ? Ok(new { message = "Investor deactivated successfully" }) : NotFound();
    }

    /// <summary>
    /// Activate investor account
    /// </summary>
    [HttpPatch("{id}/activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ActivateInvestor(Guid id)
    {
        var result = await _investorService.ActivateInvestorAsync(id);
        return result ? Ok(new { message = "Investor activated successfully" }) : NotFound();
    }

    #region KYC Operations

    /// <summary>
    /// Get KYC status for investor
    /// </summary>
    [HttpGet("{id}/kyc/status")]
    [ProducesResponseType(typeof(KycStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<KycStatusDto>> GetKycStatus(Guid id)
    {
        try
        {
            var kycStatus = await _investorService.GetKycStatusAsync(id);
            return Ok(kycStatus);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Upload KYC document
    /// </summary>
    [HttpPost("{id}/kyc/documents")]
    [ProducesResponseType(typeof(KycDocumentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<KycDocumentDto>> UploadKycDocument(Guid id, [FromBody] UploadKycDocumentDto uploadDto)
    {
        try
        {
            var document = await _investorService.UploadKycDocumentAsync(id, uploadDto);
            return CreatedAtAction(nameof(GetKycDocuments), new { id }, document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading KYC document for investor {InvestorId}", id);
            return BadRequest(new { message = "Error uploading document", details = ex.Message });
        }
    }

    /// <summary>
    /// Get all KYC documents for investor
    /// </summary>
    [HttpGet("{id}/kyc/documents")]
    [ProducesResponseType(typeof(List<KycDocumentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<KycDocumentDto>>> GetKycDocuments(Guid id)
    {
        try
        {
            var documents = await _investorService.GetInvestorDocumentsAsync(id);
            return Ok(documents);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Download KYC document
    /// </summary>
    [HttpGet("kyc/documents/{documentId}/download")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DownloadKycDocument(Guid documentId)
    {
        try
        {
            var fileContent = await _investorService.DownloadKycDocumentAsync(documentId);
            return File(fileContent, "application/octet-stream");
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Verify KYC document (Admin only)
    /// </summary>
    [HttpPost("kyc/documents/verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> VerifyKycDocument([FromBody] VerifyKycDocumentDto verifyDto)
    {
        try
        {
            var result = await _investorService.VerifyKycDocumentAsync(verifyDto);
            return result 
                ? Ok(new { message = "Document verification updated successfully" })
                : BadRequest(new { message = "Failed to update document verification" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying KYC document {DocumentId}", verifyDto.DocumentId);
            return BadRequest(new { message = "Error verifying document", details = ex.Message });
        }
    }

    /// <summary>
    /// Approve investor KYC (Admin only)
    /// </summary>
    [HttpPost("{id}/kyc/approve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ApproveKyc(Guid id)
    {
        var result = await _investorService.ApproveKycAsync(id);
        return result 
            ? Ok(new { message = "KYC approved successfully" })
            : NotFound();
    }

    /// <summary>
    /// Reject investor KYC (Admin only)
    /// </summary>
    [HttpPost("{id}/kyc/reject")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RejectKyc(Guid id, [FromBody] string rejectionReason)
    {
        var result = await _investorService.RejectKycAsync(id, rejectionReason);
        return result 
            ? Ok(new { message = "KYC rejected successfully" })
            : NotFound();
    }

    /// <summary>
    /// Delete KYC document
    /// </summary>
    [HttpDelete("kyc/documents/{documentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteKycDocument(Guid documentId)
    {
        var result = await _investorService.DeleteKycDocumentAsync(documentId);
        return result 
            ? Ok(new { message = "Document deleted successfully" })
            : NotFound();
    }

    #endregion
}