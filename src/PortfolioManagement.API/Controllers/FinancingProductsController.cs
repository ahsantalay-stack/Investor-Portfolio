using Microsoft.AspNetCore.Mvc;
using PortfolioManagement.Application.DTOs;
using PortfolioManagement.Application.Interfaces;

namespace PortfolioManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Financing Products")]
public class FinancingProductsController : ControllerBase
{
    private readonly IFinancingProductService _financingProductService;
    private readonly ILogger<FinancingProductsController> _logger;

    public FinancingProductsController(
        IFinancingProductService financingProductService, 
        ILogger<FinancingProductsController> logger)
    {
        _financingProductService = financingProductService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new financing product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(FinancingProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinancingProductDto>> CreateProduct([FromBody] CreateFinancingProductDto createProductDto)
    {
        try
        {
            var product = await _financingProductService.CreateProductAsync(createProductDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating financing product");
            return BadRequest(new { message = "Error creating product", details = ex.Message });
        }
    }

    /// <summary>
    /// Get financing product by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FinancingProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FinancingProductDto>> GetProduct(Guid id)
    {
        var product = await _financingProductService.GetProductByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    /// <summary>
    /// Get financing products with advanced filtering for marketplace
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetProducts([FromQuery] ProductFilterDto filter)
    {
        var (products, totalCount) = await _financingProductService.GetProductsAsync(filter);
        
        return Ok(new
        {
            data = products,
            totalCount,
            pageIndex = filter.PageIndex,
            pageSize = filter.PageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
            filters = new
            {
                riskLevel = filter.RiskLevel,
                returnFrequency = filter.ReturnFrequency,
                minInvestmentAmount = filter.MinInvestmentAmount,
                maxInvestmentAmount = filter.MaxInvestmentAmount,
                minReturnRate = filter.MinReturnRate,
                maxReturnRate = filter.MaxReturnRate,
                category = filter.Category,
                isShariaCompliant = filter.IsShariaCompliant,
                isActive = filter.IsActive,
                searchTerm = filter.SearchTerm
            }
        });
    }

    /// <summary>
    /// Get all active financing products for marketplace
    /// </summary>
    [HttpGet("active")]
    [ProducesResponseType(typeof(List<FinancingProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<FinancingProductDto>>> GetActiveProducts()
    {
        var products = await _financingProductService.GetActiveProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get all Shariah-compliant products
    /// </summary>
    [HttpGet("sharia-compliant")]
    [ProducesResponseType(typeof(List<FinancingProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<FinancingProductDto>>> GetShariaCompliantProducts()
    {
        var products = await _financingProductService.GetShariaCompliantProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get recommended products for specific investor
    /// </summary>
    [HttpGet("recommended/{investorId}")]
    [ProducesResponseType(typeof(List<FinancingProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<FinancingProductDto>>> GetRecommendedProducts(Guid investorId)
    {
        try
        {
            var products = await _financingProductService.GetRecommendedProductsAsync(investorId);
            return Ok(products);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update financing product
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FinancingProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinancingProductDto>> UpdateProduct(Guid id, [FromBody] CreateFinancingProductDto updateProductDto)
    {
        try
        {
            var product = await _financingProductService.UpdateProductAsync(id, updateProductDto);
            return Ok(product);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product {ProductId}", id);
            return BadRequest(new { message = "Error updating product", details = ex.Message });
        }
    }

    /// <summary>
    /// Deactivate product
    /// </summary>
    [HttpPatch("{id}/deactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeactivateProduct(Guid id)
    {
        var result = await _financingProductService.DeactivateProductAsync(id);
        return result ? Ok(new { message = "Product deactivated successfully" }) : NotFound();
    }

    /// <summary>
    /// Activate product
    /// </summary>
    [HttpPatch("{id}/activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ActivateProduct(Guid id)
    {
        var result = await _financingProductService.ActivateProductAsync(id);
        return result ? Ok(new { message = "Product activated successfully" }) : NotFound();
    }

    /// <summary>
    /// Check investment availability
    /// </summary>
    [HttpGet("{id}/availability")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CheckInvestmentAvailability(Guid id, [FromQuery] decimal amount)
    {
        try
        {
            var isAvailable = await _financingProductService.IsInvestmentAvailableAsync(id, amount);
            var availableAmount = await _financingProductService.GetAvailableInvestmentAmountAsync(id);
            
            return Ok(new
            {
                productId = id,
                requestedAmount = amount,
                isAvailable,
                availableAmount,
                message = isAvailable 
                    ? "Investment amount is available" 
                    : $"Only {availableAmount:C} SAR is available for investment"
            });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    #region Product Documents

    /// <summary>
    /// Upload product document
    /// </summary>
    [HttpPost("{id}/documents")]
    [ProducesResponseType(typeof(ProductDocumentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDocumentDto>> UploadProductDocument(
        Guid id,
        [FromForm] IFormFile file,
        [FromForm] string documentName,
        [FromForm] string documentType)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "File is required" });

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileContent = memoryStream.ToArray();

            var document = await _financingProductService.UploadProductDocumentAsync(
                id, documentName, documentType, fileContent, file.FileName, file.ContentType);

            return CreatedAtAction(nameof(GetProductDocuments), new { id }, document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading product document for product {ProductId}", id);
            return BadRequest(new { message = "Error uploading document", details = ex.Message });
        }
    }

    /// <summary>
    /// Get all documents for product
    /// </summary>
    [HttpGet("{id}/documents")]
    [ProducesResponseType(typeof(List<ProductDocumentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDocumentDto>>> GetProductDocuments(Guid id)
    {
        try
        {
            var documents = await _financingProductService.GetProductDocumentsAsync(id);
            return Ok(documents);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Download product document
    /// </summary>
    [HttpGet("documents/{documentId}/download")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DownloadProductDocument(Guid documentId)
    {
        try
        {
            var fileContent = await _financingProductService.DownloadProductDocumentAsync(documentId);
            return File(fileContent, "application/octet-stream");
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Delete product document
    /// </summary>
    [HttpDelete("documents/{documentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProductDocument(Guid documentId)
    {
        var result = await _financingProductService.DeleteProductDocumentAsync(documentId);
        return result 
            ? Ok(new { message = "Document deleted successfully" })
            : NotFound();
    }

    #endregion
}