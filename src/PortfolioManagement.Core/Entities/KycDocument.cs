using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Core.Entities;

public class KycDocument : BaseEntity
{
    public Guid InvestorId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public long FileSizeInBytes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public string? VerifiedBy { get; set; }
    public string? VerificationNotes { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime? ExpiryDate { get; set; } // For documents with expiry like ID
    
    // Navigation properties
    public virtual Investor Investor { get; set; } = null!;
}