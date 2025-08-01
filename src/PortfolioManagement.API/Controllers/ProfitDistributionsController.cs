using Microsoft.AspNetCore.Mvc;
using PortfolioManagement.Application.DTOs;
using PortfolioManagement.Application.Interfaces;

namespace PortfolioManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Profit Distributions")]
public class ProfitDistributionsController : ControllerBase
{
    private readonly IProfitDistributionService _profitDistributionService;
    private readonly ILogger<ProfitDistributionsController> _logger;

    public ProfitDistributionsController(
        IProfitDistributionService profitDistributionService,
        ILogger<ProfitDistributionsController> logger)
    {
        _profitDistributionService = profitDistributionService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new profit distribution
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProfitDistributionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProfitDistributionDto>> CreateProfitDistribution([FromBody] CreateProfitDistributionDto createDto)
    {
        try
        {
            var distribution = await _profitDistributionService.CreateProfitDistributionAsync(createDto);
            return CreatedAtAction(nameof(GetProfitDistribution), new { id = distribution.Id }, distribution);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating profit distribution");
            return BadRequest(new { message = "Error creating profit distribution", details = ex.Message });
        }
    }

    /// <summary>
    /// Get profit distribution by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProfitDistributionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfitDistributionDto>> GetProfitDistribution(Guid id)
    {
        var distribution = await _profitDistributionService.GetProfitDistributionByIdAsync(id);
        return distribution == null ? NotFound() : Ok(distribution);
    }

    /// <summary>
    /// Get profit distributions with filtering and pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetProfitDistributions(
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 50,
        [FromQuery] Guid? investorId = null,
        [FromQuery] Guid? investmentId = null,
        [FromQuery] bool? isPaid = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        var (distributions, totalCount) = await _profitDistributionService.GetProfitDistributionsAsync(
            pageIndex, pageSize, investorId, investmentId, isPaid, fromDate, toDate);

        return Ok(new
        {
            data = distributions,
            totalCount,
            pageIndex,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize),
            filters = new
            {
                investorId,
                investmentId,
                isPaid,
                fromDate,
                toDate
            }
        });
    }

    /// <summary>
    /// Get profit distributions for specific investor
    /// </summary>
    [HttpGet("investors/{investorId}")]
    [ProducesResponseType(typeof(List<ProfitDistributionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProfitDistributionDto>>> GetInvestorProfitDistributions(Guid investorId)
    {
        try
        {
            var distributions = await _profitDistributionService.GetInvestorProfitDistributionsAsync(investorId);
            return Ok(distributions);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get profit distributions for specific investment
    /// </summary>
    [HttpGet("investments/{investmentId}")]
    [ProducesResponseType(typeof(List<ProfitDistributionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProfitDistributionDto>>> GetInvestmentProfitDistributions(Guid investmentId)
    {
        try
        {
            var distributions = await _profitDistributionService.GetInvestmentProfitDistributionsAsync(investmentId);
            return Ok(distributions);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get pending profit distributions (Admin view)
    /// </summary>
    [HttpGet("pending")]
    [ProducesResponseType(typeof(List<ProfitDistributionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProfitDistributionDto>>> GetPendingDistributions()
    {
        var distributions = await _profitDistributionService.GetPendingDistributionsAsync();
        return Ok(distributions);
    }

    #region Payment Processing

    /// <summary>
    /// Process profit payment for distribution
    /// </summary>
    [HttpPost("process-payment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ProcessProfitPayment([FromBody] ProcessProfitPaymentDto paymentDto)
    {
        try
        {
            var result = await _profitDistributionService.ProcessProfitPaymentAsync(paymentDto);
            return result 
                ? Ok(new { message = "Payment processed successfully" })
                : BadRequest(new { message = "Failed to process payment" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing profit payment for distribution {DistributionId}", paymentDto.ProfitDistributionId);
            return BadRequest(new { message = "Error processing payment", details = ex.Message });
        }
    }

    /// <summary>
    /// Mark distribution as paid
    /// </summary>
    [HttpPost("{id}/mark-paid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> MarkAsPaid(
        Guid id, 
        [FromBody] MarkAsPaidDto markAsPaidDto)
    {
        var result = await _profitDistributionService.MarkDistributionAsPaidAsync(
            id, markAsPaidDto.PaymentMethod, markAsPaidDto.PaymentReference);
        
        return result 
            ? Ok(new { message = "Distribution marked as paid successfully" })
            : NotFound();
    }

    /// <summary>
    /// Process batch payments for multiple distributions
    /// </summary>
    [HttpPost("batch-payment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ProcessBatchPayments([FromBody] BatchPaymentDto batchPaymentDto)
    {
        try
        {
            var result = await _profitDistributionService.ProcessBatchPaymentsAsync(
                batchPaymentDto.DistributionIds, 
                batchPaymentDto.PaymentMethod, 
                batchPaymentDto.BatchReference);
            
            return result 
                ? Ok(new { message = $"Processed {batchPaymentDto.DistributionIds.Count} payments successfully" })
                : BadRequest(new { message = "Failed to process batch payments" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing batch payments");
            return BadRequest(new { message = "Error processing batch payments", details = ex.Message });
        }
    }

    #endregion

    #region Analytics and Reporting

    /// <summary>
    /// Get profit summary for specific investor
    /// </summary>
    [HttpGet("investors/{investorId}/summary")]
    [ProducesResponseType(typeof(ProfitSummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfitSummaryDto>> GetInvestorProfitSummary(Guid investorId)
    {
        try
        {
            var summary = await _profitDistributionService.GetInvestorProfitSummaryAsync(investorId);
            return Ok(summary);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get company-wide profit summary (Admin only)
    /// </summary>
    [HttpGet("company-summary")]
    [ProducesResponseType(typeof(CompanyProfitSummaryDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CompanyProfitSummaryDto>> GetCompanyProfitSummary()
    {
        var summary = await _profitDistributionService.GetCompanyProfitSummaryAsync();
        return Ok(summary);
    }

    /// <summary>
    /// Get monthly profit breakdown for specific year
    /// </summary>
    [HttpGet("analytics/monthly/{year}")]
    [ProducesResponseType(typeof(List<MonthlyProfitBreakdownDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MonthlyProfitBreakdownDto>>> GetMonthlyProfitBreakdown(int year)
    {
        var breakdown = await _profitDistributionService.GetMonthlyProfitBreakdownAsync(year);
        return Ok(breakdown);
    }

    /// <summary>
    /// Get comprehensive profit analytics for date range
    /// </summary>
    [HttpGet("analytics")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetProfitAnalytics(
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        var analytics = await _profitDistributionService.GetProfitAnalyticsAsync(fromDate, toDate);
        return Ok(analytics);
    }

    #endregion

    #region Automated Distribution

    /// <summary>
    /// Generate monthly profit distributions (Admin/System operation)
    /// </summary>
    [HttpPost("generate/monthly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GenerateMonthlyDistributions()
    {
        try
        {
            var count = await _profitDistributionService.GenerateMonthlyDistributionsAsync();
            return Ok(new { message = $"Generated {count} monthly distributions" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating monthly distributions");
            return BadRequest(new { message = "Error generating distributions", details = ex.Message });
        }
    }

    /// <summary>
    /// Generate quarterly profit distributions (Admin/System operation)
    /// </summary>
    [HttpPost("generate/quarterly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GenerateQuarterlyDistributions()
    {
        try
        {
            var count = await _profitDistributionService.GenerateQuarterlyDistributionsAsync();
            return Ok(new { message = $"Generated {count} quarterly distributions" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating quarterly distributions");
            return BadRequest(new { message = "Error generating distributions", details = ex.Message });
        }
    }

    /// <summary>
    /// Generate yearly profit distributions (Admin/System operation)
    /// </summary>
    [HttpPost("generate/yearly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GenerateYearlyDistributions()
    {
        try
        {
            var count = await _profitDistributionService.GenerateYearlyDistributionsAsync();
            return Ok(new { message = $"Generated {count} yearly distributions" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating yearly distributions");
            return BadRequest(new { message = "Error generating distributions", details = ex.Message });
        }
    }

    /// <summary>
    /// Calculate and distribute profit for specific investment
    /// </summary>
    [HttpPost("calculate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CalculateAndDistributeProfit([FromBody] CalculateProfitDto calculateProfitDto)
    {
        try
        {
            var result = await _profitDistributionService.CalculateAndDistributeProfitAsync(
                calculateProfitDto.InvestmentId,
                calculateProfitDto.TotalProfit,
                calculateProfitDto.PeriodStart,
                calculateProfitDto.PeriodEnd);
            
            return result 
                ? Ok(new { message = "Profit calculated and distributed successfully" })
                : BadRequest(new { message = "Failed to calculate and distribute profit" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating profit for investment {InvestmentId}", calculateProfitDto.InvestmentId);
            return BadRequest(new { message = "Error calculating profit", details = ex.Message });
        }
    }

    #endregion
}

// Supporting DTOs for request bodies
public class MarkAsPaidDto
{
    public string PaymentMethod { get; set; } = string.Empty;
    public string? PaymentReference { get; set; }
}

public class BatchPaymentDto
{
    public List<Guid> DistributionIds { get; set; } = new();
    public string PaymentMethod { get; set; } = string.Empty;
    public string? BatchReference { get; set; }
}

public class CalculateProfitDto
{
    public Guid InvestmentId { get; set; }
    public decimal TotalProfit { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}