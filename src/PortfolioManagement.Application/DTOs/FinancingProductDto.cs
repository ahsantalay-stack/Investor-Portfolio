using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Application.DTOs;

public class FinancingProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public decimal MinInvestmentAmount { get; set; }
    public decimal MaxInvestmentAmount { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public int TermInMonths { get; set; }
    public ReturnFrequency ReturnFrequency { get; set; }
    public string ReturnFrequencyText => ReturnFrequency.ToString();
    public RiskLevel RiskLevel { get; set; }
    public string RiskLevelText => RiskLevel.ToString();
    public bool IsShariaCompliant { get; set; }
    public string? ShariaComplianceCertificate { get; set; }
    public decimal TotalTargetAmount { get; set; }
    public decimal TotalInvestedAmount { get; set; }
    public decimal AvailableAmount { get; set; }
    public decimal FundingProgress => TotalTargetAmount > 0 ? (TotalInvestedAmount / TotalTargetAmount) * 100 : 0;
    public bool IsActive { get; set; }
    public DateTime LaunchDate { get; set; }
    public DateTime? MaturityDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal CompanyProfitShare { get; set; }
    public string? ProductImageUrl { get; set; }
    public List<ProductDocumentDto> Documents { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class CreateFinancingProductDto
{
    public string Name { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public decimal MinInvestmentAmount { get; set; }
    public decimal MaxInvestmentAmount { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public int TermInMonths { get; set; }
    public ReturnFrequency ReturnFrequency { get; set; }
    public RiskLevel RiskLevel { get; set; }
    public bool IsShariaCompliant { get; set; } = true;
    public string? ShariaComplianceCertificate { get; set; }
    public decimal TotalTargetAmount { get; set; }
    public DateTime LaunchDate { get; set; }
    public DateTime? MaturityDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal CompanyProfitShare { get; set; }
}

public class ProductDocumentDto
{
    public Guid Id { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public long FileSizeInBytes { get; set; }
    public bool IsPublic { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductFilterDto
{
    public RiskLevel? RiskLevel { get; set; }
    public ReturnFrequency? ReturnFrequency { get; set; }
    public decimal? MinInvestmentAmount { get; set; }
    public decimal? MaxInvestmentAmount { get; set; }
    public decimal? MinReturnRate { get; set; }
    public decimal? MaxReturnRate { get; set; }
    public string? Category { get; set; }
    public bool? IsShariaCompliant { get; set; }
    public bool? IsActive { get; set; }
    public string? SearchTerm { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public string SortBy { get; set; } = "CreatedAt";
    public string SortDirection { get; set; } = "desc";
}