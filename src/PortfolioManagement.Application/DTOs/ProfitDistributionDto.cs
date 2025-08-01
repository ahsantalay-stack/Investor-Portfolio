namespace PortfolioManagement.Application.DTOs;

public class ProfitDistributionDto
{
    public Guid Id { get; set; }
    public Guid InvestmentId { get; set; }
    public Guid InvestorId { get; set; }
    public string InvestorName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal ProfitAmount { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal CompanyShare { get; set; }
    public decimal InvestorShare { get; set; }
    public DateTime DistributionDate { get; set; }
    public DateTime PeriodStartDate { get; set; }
    public DateTime PeriodEndDate { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? PaymentReference { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public string Status => IsPaid ? "Paid" : "Pending";
    public DateTime CreatedAt { get; set; }
}

public class CreateProfitDistributionDto
{
    public Guid InvestmentId { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal CompanySharePercentage { get; set; }
    public DateTime PeriodStartDate { get; set; }
    public DateTime PeriodEndDate { get; set; }
    public DateTime DistributionDate { get; set; }
    public string? Notes { get; set; }
}

public class ProcessProfitPaymentDto
{
    public Guid ProfitDistributionId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string? PaymentReference { get; set; }
    public string? Notes { get; set; }
}

public class ProfitSummaryDto
{
    public Guid InvestorId { get; set; }
    public decimal TotalProfitEarned { get; set; }
    public decimal TotalProfitPending { get; set; }
    public decimal TotalProfitPaid { get; set; }
    public int TotalDistributions { get; set; }
    public int PendingDistributions { get; set; }
    public int PaidDistributions { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public DateTime? NextExpectedPayment { get; set; }
    public List<ProfitDistributionDto> RecentDistributions { get; set; } = new();
}

public class CompanyProfitSummaryDto
{
    public decimal TotalProfitGenerated { get; set; }
    public decimal TotalCompanyShare { get; set; }
    public decimal TotalInvestorShare { get; set; }
    public decimal TotalProfitDistributed { get; set; }
    public decimal TotalProfitPending { get; set; }
    public int TotalInvestments { get; set; }
    public int ActiveInvestments { get; set; }
    public List<MonthlyProfitBreakdownDto> MonthlyBreakdown { get; set; } = new();
}

public class MonthlyProfitBreakdownDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public decimal TotalProfit { get; set; }
    public decimal CompanyShare { get; set; }
    public decimal InvestorShare { get; set; }
    public int DistributionCount { get; set; }
}