using Microsoft.AspNetCore.Mvc;
using PortfolioManagement.Application.DTOs;
using PortfolioManagement.Application.Interfaces;

namespace PortfolioManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Reports & Analytics")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(IReportsService reportsService, ILogger<ReportsController> logger)
    {
        _reportsService = reportsService;
        _logger = logger;
    }

    #region Investor Reports

    /// <summary>
    /// Generate comprehensive report for specific investor
    /// </summary>
    [HttpGet("investors/{investorId}")]
    [ProducesResponseType(typeof(InvestorReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvestorReportDto>> GenerateInvestorReport(
        Guid investorId,
        [FromQuery] DateTime periodStart,
        [FromQuery] DateTime periodEnd)
    {
        try
        {
            var report = await _reportsService.GenerateInvestorReportAsync(investorId, periodStart, periodEnd);
            return Ok(report);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating investor report for {InvestorId}", investorId);
            return BadRequest(new { message = "Error generating report", details = ex.Message });
        }
    }

    /// <summary>
    /// Generate reports for all investors
    /// </summary>
    [HttpGet("investors")]
    [ProducesResponseType(typeof(List<InvestorReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<InvestorReportDto>>> GenerateAllInvestorReports(
        [FromQuery] DateTime periodStart,
        [FromQuery] DateTime periodEnd)
    {
        try
        {
            var reports = await _reportsService.GenerateAllInvestorReportsAsync(periodStart, periodEnd);
            return Ok(reports);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating all investor reports");
            return BadRequest(new { message = "Error generating reports", details = ex.Message });
        }
    }

    /// <summary>
    /// Export investor report in various formats (PDF, Excel, CSV)
    /// </summary>
    [HttpGet("investors/{investorId}/export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ExportInvestorReport(
        Guid investorId,
        [FromQuery] DateTime periodStart,
        [FromQuery] DateTime periodEnd,
        [FromQuery] string format = "pdf",
        [FromQuery] string language = "en")
    {
        try
        {
            var fileContent = await _reportsService.ExportInvestorReportAsync(investorId, periodStart, periodEnd, format, language);
            var contentType = format.ToLower() switch
            {
                "pdf" => "application/pdf",
                "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "csv" => "text/csv",
                _ => "application/octet-stream"
            };
            
            var fileName = $"investor-report-{investorId}-{periodStart:yyyy-MM-dd}-{periodEnd:yyyy-MM-dd}.{format}";
            return File(fileContent, contentType, fileName);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting investor report for {InvestorId}", investorId);
            return BadRequest(new { message = "Error exporting report", details = ex.Message });
        }
    }

    #endregion

    #region Company Analytics

    /// <summary>
    /// Generate comprehensive company analytics
    /// </summary>
    [HttpGet("company/analytics")]
    [ProducesResponseType(typeof(CompanyAnalyticsDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CompanyAnalyticsDto>> GenerateCompanyAnalytics(
        [FromQuery] DateTime periodStart,
        [FromQuery] DateTime periodEnd)
    {
        try
        {
            var analytics = await _reportsService.GenerateCompanyAnalyticsAsync(periodStart, periodEnd);
            return Ok(analytics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating company analytics");
            return BadRequest(new { message = "Error generating analytics", details = ex.Message });
        }
    }

    /// <summary>
    /// Get product performance analytics
    /// </summary>
    [HttpGet("products/performance")]
    [ProducesResponseType(typeof(List<ProductPerformanceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductPerformanceDto>>> GetProductPerformanceAnalytics(
        [FromQuery] DateTime periodStart,
        [FromQuery] DateTime periodEnd)
    {
        var performance = await _reportsService.GetProductPerformanceAnalyticsAsync(periodStart, periodEnd);
        return Ok(performance);
    }

    /// <summary>
    /// Get KYC processing statistics
    /// </summary>
    [HttpGet("kyc/statistics")]
    [ProducesResponseType(typeof(KycStatisticsDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<KycStatisticsDto>> GetKycStatistics()
    {
        var statistics = await _reportsService.GetKycStatisticsAsync();
        return Ok(statistics);
    }

    /// <summary>
    /// Get regional distribution of investors (KSA regions)
    /// </summary>
    [HttpGet("regional-distribution")]
    [ProducesResponseType(typeof(List<RegionalDistributionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RegionalDistributionDto>>> GetRegionalDistribution()
    {
        var distribution = await _reportsService.GetRegionalDistributionAsync();
        return Ok(distribution);
    }

    /// <summary>
    /// Get monthly trends for specific year
    /// </summary>
    [HttpGet("trends/monthly/{year}")]
    [ProducesResponseType(typeof(List<MonthlyTrendDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MonthlyTrendDto>>> GetMonthlyTrends(int year)
    {
        var trends = await _reportsService.GetMonthlyTrendsAsync(year);
        return Ok(trends);
    }

    #endregion

    #region Tax Reports (KSA Specific)

    /// <summary>
    /// Generate tax report for investor (includes Zakat calculation)
    /// </summary>
    [HttpGet("tax/investors/{investorId}/{taxYear}")]
    [ProducesResponseType(typeof(TaxReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxReportDto>> GenerateTaxReport(Guid investorId, int taxYear)
    {
        try
        {
            var report = await _reportsService.GenerateTaxReportAsync(investorId, taxYear);
            return Ok(report);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating tax report for investor {InvestorId}", investorId);
            return BadRequest(new { message = "Error generating tax report", details = ex.Message });
        }
    }

    /// <summary>
    /// Generate tax reports for all investors for specific year
    /// </summary>
    [HttpGet("tax/all/{taxYear}")]
    [ProducesResponseType(typeof(List<TaxReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TaxReportDto>>> GenerateAllTaxReports(int taxYear)
    {
        try
        {
            var reports = await _reportsService.GenerateAllTaxReportsAsync(taxYear);
            return Ok(reports);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating all tax reports for year {Year}", taxYear);
            return BadRequest(new { message = "Error generating tax reports", details = ex.Message });
        }
    }

    /// <summary>
    /// Export tax report in various formats
    /// </summary>
    [HttpGet("tax/investors/{investorId}/{taxYear}/export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ExportTaxReport(
        Guid investorId,
        int taxYear,
        [FromQuery] string format = "pdf",
        [FromQuery] string language = "en")
    {
        try
        {
            var fileContent = await _reportsService.ExportTaxReportAsync(investorId, taxYear, format, language);
            var contentType = format.ToLower() switch
            {
                "pdf" => "application/pdf",
                "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
            
            var fileName = $"tax-report-{investorId}-{taxYear}.{format}";
            return File(fileContent, contentType, fileName);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting tax report for investor {InvestorId}", investorId);
            return BadRequest(new { message = "Error exporting tax report", details = ex.Message });
        }
    }

    /// <summary>
    /// Calculate Zakat amount for investor (Islamic obligation)
    /// </summary>
    [HttpGet("zakat/investors/{investorId}/{year}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CalculateZakatAmount(Guid investorId, int year)
    {
        try
        {
            var zakatAmount = await _reportsService.CalculateZakatAmountAsync(investorId, year);
            return Ok(new
            {
                investorId,
                year,
                zakatAmount,
                zakatRate = 2.5m,
                currency = "SAR",
                calculatedAt = DateTime.UtcNow
            });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating Zakat for investor {InvestorId}", investorId);
            return BadRequest(new { message = "Error calculating Zakat", details = ex.Message });
        }
    }

    /// <summary>
    /// Get tax summary for specific year
    /// </summary>
    [HttpGet("tax/summary/{taxYear}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTaxSummary(int taxYear)
    {
        var summary = await _reportsService.GetTaxSummaryAsync(taxYear);
        return Ok(summary);
    }

    #endregion

    #region Custom Reports & Export

    /// <summary>
    /// Generate custom report based on parameters
    /// </summary>
    [HttpPost("custom/{reportType}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GenerateCustomReport(
        string reportType,
        [FromBody] Dictionary<string, object> parameters)
    {
        try
        {
            var report = await _reportsService.GenerateCustomReportAsync(reportType, parameters);
            return Ok(report);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating custom report of type {ReportType}", reportType);
            return BadRequest(new { message = "Error generating custom report", details = ex.Message });
        }
    }

    /// <summary>
    /// Export report based on request configuration
    /// </summary>
    [HttpPost("export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ExportReport([FromBody] ExportRequestDto exportRequest)
    {
        try
        {
            var fileContent = await _reportsService.ExportReportAsync(exportRequest);
            var contentType = exportRequest.Format.ToLower() switch
            {
                "pdf" => "application/pdf",
                "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "csv" => "text/csv",
                _ => "application/octet-stream"
            };
            
            var fileName = $"{exportRequest.ReportType}-report-{exportRequest.PeriodStart:yyyy-MM-dd}-{exportRequest.PeriodEnd:yyyy-MM-dd}.{exportRequest.Format}";
            return File(fileContent, contentType, fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting report");
            return BadRequest(new { message = "Error exporting report", details = ex.Message });
        }
    }

    #endregion

    #region Dashboard Analytics

    /// <summary>
    /// Get analytics data for investor dashboard
    /// </summary>
    [HttpGet("dashboard/investors/{investorId}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetInvestorDashboardAnalytics(Guid investorId)
    {
        try
        {
            var analytics = await _reportsService.GetInvestorDashboardAnalyticsAsync(investorId);
            return Ok(analytics);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dashboard analytics for investor {InvestorId}", investorId);
            return BadRequest(new { message = "Error getting dashboard analytics", details = ex.Message });
        }
    }

    /// <summary>
    /// Get analytics data for admin dashboard
    /// </summary>
    [HttpGet("dashboard/admin")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAdminDashboardAnalytics()
    {
        try
        {
            var analytics = await _reportsService.GetAdminDashboardAnalyticsAsync();
            return Ok(analytics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting admin dashboard analytics");
            return BadRequest(new { message = "Error getting dashboard analytics", details = ex.Message });
        }
    }

    /// <summary>
    /// Get real-time analytics data
    /// </summary>
    [HttpGet("dashboard/realtime")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetRealTimeAnalytics()
    {
        try
        {
            var analytics = await _reportsService.GetRealTimeAnalyticsAsync();
            return Ok(analytics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting real-time analytics");
            return BadRequest(new { message = "Error getting real-time analytics", details = ex.Message });
        }
    }

    #endregion

    #region Scheduled Reports

    /// <summary>
    /// Schedule monthly report generation
    /// </summary>
    [HttpPost("schedule/monthly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ScheduleMonthlyReports()
    {
        try
        {
            var result = await _reportsService.ScheduleMonthlyReportsAsync();
            return result 
                ? Ok(new { message = "Monthly reports scheduled successfully" })
                : BadRequest(new { message = "Failed to schedule monthly reports" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error scheduling monthly reports");
            return BadRequest(new { message = "Error scheduling reports", details = ex.Message });
        }
    }

    /// <summary>
    /// Schedule quarterly report generation
    /// </summary>
    [HttpPost("schedule/quarterly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ScheduleQuarterlyReports()
    {
        try
        {
            var result = await _reportsService.ScheduleQuarterlyReportsAsync();
            return result 
                ? Ok(new { message = "Quarterly reports scheduled successfully" })
                : BadRequest(new { message = "Failed to schedule quarterly reports" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error scheduling quarterly reports");
            return BadRequest(new { message = "Error scheduling reports", details = ex.Message });
        }
    }

    /// <summary>
    /// Schedule yearly report generation
    /// </summary>
    [HttpPost("schedule/yearly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ScheduleYearlyReports()
    {
        try
        {
            var result = await _reportsService.ScheduleYearlyReportsAsync();
            return result 
                ? Ok(new { message = "Yearly reports scheduled successfully" })
                : BadRequest(new { message = "Failed to schedule yearly reports" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error scheduling yearly reports");
            return BadRequest(new { message = "Error scheduling reports", details = ex.Message });
        }
    }

    /// <summary>
    /// Get all scheduled reports
    /// </summary>
    [HttpGet("schedule")]
    [ProducesResponseType(typeof(List<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<object>>> GetScheduledReports()
    {
        var reports = await _reportsService.GetScheduledReportsAsync();
        return Ok(reports);
    }

    #endregion
}