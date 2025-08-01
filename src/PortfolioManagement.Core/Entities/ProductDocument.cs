namespace PortfolioManagement.Core.Entities;

public class ProductDocument : BaseEntity
{
    public Guid FinancingProductId { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty; // Factsheet, Legal, Sharia Certificate, etc.
    public string FilePath { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public long FileSizeInBytes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = true; // Whether investors can download
    public int DisplayOrder { get; set; } = 0;
    
    // Navigation properties
    public virtual FinancingProduct FinancingProduct { get; set; } = null!;
}