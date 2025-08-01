namespace PortfolioManagement.Core.Entities;

public class ProfitDistribution : BaseEntity
{
    public Guid InvestmentId { get; set; }
    public Guid InvestorId { get; set; }
    public decimal ProfitAmount { get; set; }
    public decimal TotalProfit { get; set; } // Total profit before company share
    public decimal CompanyShare { get; set; }
    public decimal InvestorShare { get; set; }
    public DateTime DistributionDate { get; set; }
    public DateTime PeriodStartDate { get; set; }
    public DateTime PeriodEndDate { get; set; }
    public bool IsPaid { get; set; } = false;
    public DateTime? PaidAt { get; set; }
    public string? PaymentReference { get; set; }
    public string? PaymentMethod { get; set; } // Bank Transfer, etc.
    public string? Notes { get; set; }
    
    // Navigation properties
    public virtual Investment Investment { get; set; } = null!;
    public virtual Investor Investor { get; set; } = null!;
}