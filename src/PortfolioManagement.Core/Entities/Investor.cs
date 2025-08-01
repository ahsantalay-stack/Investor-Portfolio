using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Core.Entities;

public class Investor : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstNameEn { get; set; } = string.Empty;
    public string LastNameEn { get; set; } = string.Empty;
    public string FirstNameAr { get; set; } = string.Empty;
    public string LastNameAr { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; } = "SA"; // Default to Saudi Arabia
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public KycStatus KycStatus { get; set; } = KycStatus.NotStarted;
    public DateTime? KycApprovedAt { get; set; }
    public string? KycRejectionReason { get; set; }
    public RiskLevel RiskTolerance { get; set; } = RiskLevel.Medium;
    public decimal TotalInvestedAmount { get; set; } = 0;
    public decimal TotalProfitEarned { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public bool TwoFactorEnabled { get; set; } = false;
    public string? BankAccountNumber { get; set; }
    public string? BankName { get; set; }
    public string? IbanNumber { get; set; }
    public string? PreferredLanguage { get; set; } = "en"; // en, ar
    
    // Navigation properties
    public virtual ICollection<Investment> Investments { get; set; } = new List<Investment>();
    public virtual ICollection<KycDocument> KycDocuments { get; set; } = new List<KycDocument>();
    public virtual ICollection<ProfitDistribution> ProfitDistributions { get; set; } = new List<ProfitDistribution>();
}