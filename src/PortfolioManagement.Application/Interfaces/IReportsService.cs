using PortfolioManagement.Application.DTOs;

namespace PortfolioManagement.Application.Interfaces;

public interface IReportsService
{
    // Investor Reports
    Task<InvestorReportDto> GenerateInvestorReportAsync(Guid investorId, DateTime periodStart, DateTime periodEnd);
    Task<List<InvestorReportDto>> GenerateAllInvestorReportsAsync(DateTime periodStart, DateTime periodEnd);
    Task<byte[]> ExportInvestorReportAsync(Guid investorId, DateTime periodStart, DateTime periodEnd, string format = "pdf", string language = "en");

    // Company Analytics
    Task<CompanyAnalyticsDto> GenerateCompanyAnalyticsAsync(DateTime periodStart, DateTime periodEnd);
    Task<List<ProductPerformanceDto>> GetProductPerformanceAnalyticsAsync(DateTime periodStart, DateTime periodEnd);
    Task<KycStatisticsDto> GetKycStatisticsAsync();
    Task<List<RegionalDistributionDto>> GetRegionalDistributionAsync();
    Task<List<MonthlyTrendDto>> GetMonthlyTrendsAsync(int year);

    // Tax Reports (KSA specific)
    Task<TaxReportDto> GenerateTaxReportAsync(Guid investorId, int taxYear);
    Task<List<TaxReportDto>> GenerateAllTaxReportsAsync(int taxYear);
    Task<byte[]> ExportTaxReportAsync(Guid investorId, int taxYear, string format = "pdf", string language = "en");
    Task<decimal> CalculateZakatAmountAsync(Guid investorId, int year);
    Task<object> GetTaxSummaryAsync(int taxYear);

    // Custom Reports
    Task<object> GenerateCustomReportAsync(string reportType, Dictionary<string, object> parameters);
    Task<byte[]> ExportCustomReportAsync(string reportType, Dictionary<string, object> parameters, string format = "pdf");

    // Export Functionality
    Task<byte[]> ExportReportAsync(ExportRequestDto exportRequest);
    Task<string> GenerateReportUrlAsync(string reportType, Dictionary<string, object> parameters);

    // Scheduled Reports
    Task<bool> ScheduleMonthlyReportsAsync();
    Task<bool> ScheduleQuarterlyReportsAsync();
    Task<bool> ScheduleYearlyReportsAsync();
    Task<List<object>> GetScheduledReportsAsync();

    // Dashboard Analytics
    Task<object> GetInvestorDashboardAnalyticsAsync(Guid investorId);
    Task<object> GetAdminDashboardAnalyticsAsync();
    Task<object> GetRealTimeAnalyticsAsync();
}