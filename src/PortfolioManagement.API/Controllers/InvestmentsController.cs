using Microsoft.AspNetCore.Mvc;
using PortfolioManagement.Application.DTOs;
using PortfolioManagement.Application.Interfaces;

namespace PortfolioManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Investments")]
public class InvestmentsController : ControllerBase
{
    private readonly IInvestmentService _investmentService;
    private readonly ILogger<InvestmentsController> _logger;

    public InvestmentsController(IInvestmentService investmentService, ILogger<InvestmentsController> logger)
    {
        _investmentService = investmentService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new investment with priority allocation
    /// </summary>
    [HttpPost("investors/{investorId}")]
    [ProducesResponseType(typeof(InvestmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvestmentDto>> CreateInvestment(Guid investorId, [FromBody] CreateInvestmentDto createInvestmentDto)
    {
        try
        {
            var investment = await _investmentService.CreateInvestmentAsync(investorId, createInvestmentDto);
            return CreatedAtAction(nameof(GetInvestment), new { id = investment.Id }, investment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = "Invalid request", details = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating investment for investor {InvestorId}", investorId);
            return BadRequest(new { message = "Error creating investment", details = ex.Message });
        }
    }

    /// <summary>
    /// Get investment by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InvestmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvestmentDto>> GetInvestment(Guid id)
    {
        var investment = await _investmentService.GetInvestmentByIdAsync(id);
        return investment == null ? NotFound() : Ok(investment);
    }

    /// <summary>
    /// Get all investments for a specific investor
    /// </summary>
    [HttpGet("investors/{investorId}")]
    [ProducesResponseType(typeof(List<InvestmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<InvestmentDto>>> GetInvestorInvestments(Guid investorId)
    {
        try
        {
            var investments = await _investmentService.GetInvestorInvestmentsAsync(investorId);
            return Ok(investments);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get investments with filtering and pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetInvestments(
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 50,
        [FromQuery] Guid? investorId = null,
        [FromQuery] Guid? productId = null)
    {
        var (investments, totalCount) = await _investmentService.GetInvestmentsAsync(pageIndex, pageSize, investorId, productId);
        
        return Ok(new
        {
            data = investments,
            totalCount,
            pageIndex,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize),
            filters = new
            {
                investorId,
                productId
            }
        });
    }

    /// <summary>
    /// Get investment summary for investor (dashboard data)
    /// </summary>
    [HttpGet("investors/{investorId}/summary")]
    [ProducesResponseType(typeof(InvestmentSummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvestmentSummaryDto>> GetInvestmentSummary(Guid investorId)
    {
        try
        {
            var summary = await _investmentService.GetInvestmentSummaryAsync(investorId);
            return Ok(summary);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get comprehensive portfolio performance for investor
    /// </summary>
    [HttpGet("investors/{investorId}/portfolio")]
    [ProducesResponseType(typeof(PortfolioPerformanceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PortfolioPerformanceDto>> GetPortfolioPerformance(Guid investorId)
    {
        try
        {
            var performance = await _investmentService.GetPortfolioPerformanceAsync(investorId);
            return Ok(performance);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get monthly performance breakdown for specific year
    /// </summary>
    [HttpGet("investors/{investorId}/performance/{year}")]
    [ProducesResponseType(typeof(List<MonthlyPerformanceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<MonthlyPerformanceDto>>> GetMonthlyPerformance(Guid investorId, int year)
    {
        try
        {
            var performance = await _investmentService.GetMonthlyPerformanceAsync(investorId, year);
            return Ok(performance);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get complete dashboard data for investor
    /// </summary>
    [HttpGet("investors/{investorId}/dashboard")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetInvestorDashboard(Guid investorId)
    {
        try
        {
            var dashboardData = await _investmentService.GetInvestorDashboardDataAsync(investorId);
            return Ok(dashboardData);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    #region Priority Allocation System

    /// <summary>
    /// Process investment allocation with priority system (lowest return gets priority)
    /// </summary>
    [HttpPost("products/{productId}/allocate")]
    [ProducesResponseType(typeof(InvestmentAllocationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvestmentAllocationDto>> ProcessInvestmentAllocation(
        Guid productId, 
        [FromBody] decimal requestedAmount)
    {
        try
        {
            var allocation = await _investmentService.ProcessInvestmentAllocationAsync(productId, requestedAmount);
            return Ok(allocation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = "Invalid allocation request", details = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing investment allocation for product {ProductId}", productId);
            return BadRequest(new { message = "Error processing allocation", details = ex.Message });
        }
    }

    /// <summary>
    /// Get priority queue for a specific product (shows investment order based on lowest return)
    /// </summary>
    [HttpGet("products/{productId}/priority-queue")]
    [ProducesResponseType(typeof(List<InvestmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<InvestmentDto>>> GetPriorityQueue(Guid productId)
    {
        try
        {
            var priorityQueue = await _investmentService.GetPriorityQueueForProductAsync(productId);
            return Ok(priorityQueue);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Recalculate priority ranks for a product (Admin operation)
    /// </summary>
    [HttpPost("products/{productId}/recalculate-priorities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RecalculatePriorityRanks(Guid productId)
    {
        try
        {
            await _investmentService.RecalculatePriorityRanksAsync(productId);
            return Ok(new { message = "Priority ranks recalculated successfully" });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error recalculating priority ranks for product {ProductId}", productId);
            return BadRequest(new { message = "Error recalculating priorities", details = ex.Message });
        }
    }

    #endregion

    #region Investment Operations

    /// <summary>
    /// Cancel an investment
    /// </summary>
    [HttpPost("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CancelInvestment(Guid id, [FromBody] string reason)
    {
        var result = await _investmentService.CancelInvestmentAsync(id, reason);
        return result 
            ? Ok(new { message = "Investment cancelled successfully" })
            : NotFound();
    }

    /// <summary>
    /// Complete an investment (Admin operation)
    /// </summary>
    [HttpPost("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CompleteInvestment(Guid id)
    {
        var result = await _investmentService.CompleteInvestmentAsync(id);
        return result 
            ? Ok(new { message = "Investment completed successfully" })
            : NotFound();
    }

    /// <summary>
    /// Activate a pending investment (Admin operation)
    /// </summary>
    [HttpPost("{id}/activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ActivateInvestment(Guid id)
    {
        var result = await _investmentService.ActivateInvestmentAsync(id);
        return result 
            ? Ok(new { message = "Investment activated successfully" })
            : NotFound();
    }

    #endregion
}