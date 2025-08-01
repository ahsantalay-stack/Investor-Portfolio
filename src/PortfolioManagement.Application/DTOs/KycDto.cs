using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Application.DTOs;

public class KycDocumentDto
{
    public Guid Id { get; set; }
    public Guid InvestorId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public long FileSizeInBytes { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? VerificationNotes { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UploadKycDocumentDto
{
    public DocumentType DocumentType { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public string FileContent { get; set; } = string.Empty; // Base64 encoded
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public DateTime? ExpiryDate { get; set; }
}

public class KycStatusDto
{
    public Guid InvestorId { get; set; }
    public KycStatus Status { get; set; }
    public string StatusText => Status.ToString();
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionReason { get; set; }
    public List<KycDocumentDto> Documents { get; set; } = new();
    public List<DocumentType> RequiredDocuments { get; set; } = new();
    public List<DocumentType> MissingDocuments { get; set; } = new();
    public bool CanProceedToInvestment { get; set; }
}

public class VerifyKycDocumentDto
{
    public Guid DocumentId { get; set; }
    public bool IsApproved { get; set; }
    public string? Notes { get; set; }
    public string? RejectionReason { get; set; }
}