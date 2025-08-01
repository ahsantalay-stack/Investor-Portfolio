using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Application.DTOs;

public class InvestorDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstNameEn { get; set; } = string.Empty;
    public string LastNameEn { get; set; } = string.Empty;
    public string FirstNameAr { get; set; } = string.Empty;
    public string LastNameAr { get; set; } = string.Empty;
    public string FullNameEn => $"{FirstNameEn} {LastNameEn}".Trim();
    public string FullNameAr => $"{FirstNameAr} {LastNameAr}".Trim();
    public string NationalId { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public KycStatus KycStatus { get; set; }
    public DateTime? KycApprovedAt { get; set; }
    public RiskLevel RiskTolerance { get; set; }
    public decimal TotalInvestedAmount { get; set; }
    public decimal TotalProfitEarned { get; set; }
    public bool IsActive { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public string? PreferredLanguage { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateInvestorDto
{
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstNameEn { get; set; } = string.Empty;
    public string LastNameEn { get; set; } = string.Empty;
    public string FirstNameAr { get; set; } = string.Empty;
    public string LastNameAr { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; } = "SA";
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public RiskLevel RiskTolerance { get; set; } = RiskLevel.Medium;
    public string? PreferredLanguage { get; set; } = "en";
}

public class UpdateInvestorDto
{
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstNameEn { get; set; } = string.Empty;
    public string LastNameEn { get; set; } = string.Empty;
    public string FirstNameAr { get; set; } = string.Empty;
    public string LastNameAr { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public RiskLevel RiskTolerance { get; set; }
    public string? PreferredLanguage { get; set; }
    public string? BankAccountNumber { get; set; }
    public string? BankName { get; set; }
    public string? IbanNumber { get; set; }
}