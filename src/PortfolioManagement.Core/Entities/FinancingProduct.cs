using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Core.Entities;

public class FinancingProduct : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public decimal MinInvestmentAmount { get; set; }
    public decimal MaxInvestmentAmount { get; set; }
    public decimal ExpectedReturnRate { get; set; } // Annual percentage
    public int TermInMonths { get; set; }
    public ReturnFrequency ReturnFrequency { get; set; }
    public RiskLevel RiskLevel { get; set; }
    public bool IsShariaCompliant { get; set; } = true;
    public string? ShariaComplianceCertificate { get; set; }
    public decimal TotalTargetAmount { get; set; }
    public decimal TotalInvestedAmount { get; set; } = 0;
    public decimal AvailableAmount => TotalTargetAmount - TotalInvestedAmount;
    public bool IsActive { get; set; } = true;
    public DateTime LaunchDate { get; set; }
    public DateTime? MaturityDate { get; set; }
    public string Category { get; set; } = string.Empty; // Real Estate, Trade Finance, etc.
    public decimal CompanyProfitShare { get; set; } // Percentage for company
    public string? ProductImageUrl { get; set; }
    public string? DocumentUrl { get; set; }
    
    // Navigation properties
    public virtual ICollection<Investment> Investments { get; set; } = new List<Investment>();
    public virtual ICollection<ProductDocument> ProductDocuments { get; set; } = new List<ProductDocument>();
}