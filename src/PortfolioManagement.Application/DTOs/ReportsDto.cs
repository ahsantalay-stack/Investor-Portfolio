namespace PortfolioManagement.Application.DTOs;

public class InvestorReportDto
{
    public Guid InvestorId { get; set; }
    public string InvestorNameEn { get; set; } = string.Empty;
    public string InvestorNameAr { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime ReportGeneratedAt { get; set; } = DateTime.UtcNow;
    public string ReportPeriod { get; set; } = string.Empty;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    
    // Investment Summary
    public decimal TotalInvestedAmount { get; set; }
    public decimal TotalProfitEarned { get; set; }
    public decimal TotalReturnAmount { get; set; }
    public decimal OverallReturnRate { get; set; }
    public int TotalInvestments { get; set; }
    public int ActiveInvestments { get; set; }
    public int CompletedInvestments { get; set; }
    
    // Detailed Investments
    public List<InvestmentReportDetailDto> Investments { get; set; } = new();
    
    // Profit Distributions
    public List<ProfitDistributionReportDto> ProfitDistributions { get; set; } = new();
    
    // Tax Information (KSA specific)
    public decimal TaxableIncome { get; set; }
    public decimal ZakatAmount { get; set; }
    public decimal WithholdingTax { get; set; }
}

public class InvestmentReportDetailDto
{
    public Guid InvestmentId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductNameAr { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public DateTime InvestmentDate { get; set; }
    public DateTime ExpectedMaturityDate { get; set; }
    public DateTime? ActualMaturityDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal ProfitEarned { get; set; }
    public decimal CurrentValue { get; set; }
    public bool IsShariaCompliant { get; set; }
}

public class ProfitDistributionReportDto
{
    public Guid DistributionId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProfitAmount { get; set; }
    public DateTime DistributionDate { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? PaymentMethod { get; set; }
}

public class CompanyAnalyticsDto
{
    public DateTime ReportDate { get; set; } = DateTime.UtcNow;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    
    // Investment Statistics
    public decimal TotalInvestmentVolume { get; set; }
    public decimal TotalProfitGenerated { get; set; }
    public decimal CompanyTotalShare { get; set; }
    public decimal InvestorTotalShare { get; set; }
    public int TotalInvestors { get; set; }
    public int ActiveInvestors { get; set; }
    public int TotalInvestments { get; set; }
    public int ActiveInvestments { get; set; }
    
    // Product Performance
    public List<ProductPerformanceDto> ProductPerformances { get; set; } = new();
    
    // KYC Statistics
    public KycStatisticsDto KycStatistics { get; set; } = new();
    
    // Geographic Distribution (KSA regions)
    public List<RegionalDistributionDto> RegionalDistribution { get; set; } = new();
    
    // Monthly Trends
    public List<MonthlyTrendDto> MonthlyTrends { get; set; } = new();
}

public class ProductPerformanceDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductNameAr { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal TotalTargetAmount { get; set; }
    public decimal TotalInvestedAmount { get; set; }
    public decimal FundingPercentage { get; set; }
    public decimal AverageReturnRate { get; set; }
    public decimal TotalProfitGenerated { get; set; }
    public int InvestorCount { get; set; }
    public bool IsShariaCompliant { get; set; }
    public DateTime LaunchDate { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class KycStatisticsDto
{
    public int TotalInvestors { get; set; }
    public int KycApproved { get; set; }
    public int KycPending { get; set; }
    public int KycRejected { get; set; }
    public int DocumentsUploaded { get; set; }
    public int DocumentsVerified { get; set; }
    public double AverageProcessingDays { get; set; }
}

public class RegionalDistributionDto
{
    public string Region { get; set; } = string.Empty; // Riyadh, Jeddah, Dammam, etc.
    public string RegionAr { get; set; } = string.Empty;
    public int InvestorCount { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal Percentage { get; set; }
}

public class MonthlyTrendDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public string MonthNameAr { get; set; } = string.Empty;
    public decimal NewInvestments { get; set; }
    public decimal ProfitsDistributed { get; set; }
    public int NewInvestors { get; set; }
    public int CompletedInvestments { get; set; }
}

public class TaxReportDto
{
    public Guid InvestorId { get; set; }
    public string InvestorName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public int TaxYear { get; set; }
    public DateTime ReportGeneratedAt { get; set; } = DateTime.UtcNow;
    
    // Income Summary
    public decimal TotalProfitIncome { get; set; }
    public decimal TaxableIncome { get; set; }
    public decimal NonTaxableIncome { get; set; }
    
    // Zakat Calculation (Islamic tax)
    public decimal ZakatableAmount { get; set; }
    public decimal ZakatDue { get; set; }
    public decimal ZakatRate { get; set; } = 2.5m; // Standard rate is 2.5%
    
    // Withholding Tax
    public decimal WithholdingTaxPaid { get; set; }
    public decimal WithholdingTaxRate { get; set; }
    
    // Detailed Transactions
    public List<TaxTransactionDto> Transactions { get; set; } = new();
}

public class TaxTransactionDto
{
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } = string.Empty; // Investment, Profit, etc.
    public bool IsZakatable { get; set; }
    public bool IsTaxable { get; set; }
}

public class ExportRequestDto
{
    public string ReportType { get; set; } = string.Empty; // "investor", "company", "tax"
    public string Format { get; set; } = "pdf"; // "pdf", "excel", "csv"
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public Guid? InvestorId { get; set; }
    public string Language { get; set; } = "en"; // "en", "ar"
    public List<string> Sections { get; set; } = new(); // Specific sections to include
}