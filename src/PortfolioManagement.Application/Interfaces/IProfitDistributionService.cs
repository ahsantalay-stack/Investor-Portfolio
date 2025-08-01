using PortfolioManagement.Application.DTOs;

namespace PortfolioManagement.Application.Interfaces;

public interface IProfitDistributionService
{
    // Profit Distribution Management
    Task<ProfitDistributionDto> CreateProfitDistributionAsync(CreateProfitDistributionDto createDto);
    Task<ProfitDistributionDto?> GetProfitDistributionByIdAsync(Guid id);
    Task<List<ProfitDistributionDto>> GetInvestorProfitDistributionsAsync(Guid investorId);
    Task<List<ProfitDistributionDto>> GetInvestmentProfitDistributionsAsync(Guid investmentId);
    Task<(IEnumerable<ProfitDistributionDto> Distributions, int TotalCount)> GetProfitDistributionsAsync(
        int pageIndex = 0,
        int pageSize = 50,
        Guid? investorId = null,
        Guid? investmentId = null,
        bool? isPaid = null,
        DateTime? fromDate = null,
        DateTime? toDate = null);

    // Payment Processing
    Task<bool> ProcessProfitPaymentAsync(ProcessProfitPaymentDto paymentDto);
    Task<bool> MarkDistributionAsPaidAsync(Guid distributionId, string paymentMethod, string? paymentReference = null);
    Task<List<ProfitDistributionDto>> GetPendingDistributionsAsync();
    Task<bool> ProcessBatchPaymentsAsync(List<Guid> distributionIds, string paymentMethod, string? batchReference = null);

    // Analytics and Reporting
    Task<ProfitSummaryDto> GetInvestorProfitSummaryAsync(Guid investorId);
    Task<CompanyProfitSummaryDto> GetCompanyProfitSummaryAsync();
    Task<List<MonthlyProfitBreakdownDto>> GetMonthlyProfitBreakdownAsync(int year);
    Task<object> GetProfitAnalyticsAsync(DateTime fromDate, DateTime toDate);

    // Automated Distribution
    Task<int> GenerateMonthlyDistributionsAsync();
    Task<int> GenerateQuarterlyDistributionsAsync();
    Task<int> GenerateYearlyDistributionsAsync();
    Task<bool> CalculateAndDistributeProfitAsync(Guid investmentId, decimal totalProfit, DateTime periodStart, DateTime periodEnd);
}