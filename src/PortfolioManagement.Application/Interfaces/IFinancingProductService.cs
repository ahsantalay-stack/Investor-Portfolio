using PortfolioManagement.Application.DTOs;

namespace PortfolioManagement.Application.Interfaces;

public interface IFinancingProductService
{
    Task<FinancingProductDto> CreateProductAsync(CreateFinancingProductDto createProductDto);
    Task<FinancingProductDto> UpdateProductAsync(Guid productId, CreateFinancingProductDto updateProductDto);
    Task<FinancingProductDto?> GetProductByIdAsync(Guid productId);
    Task<(IEnumerable<FinancingProductDto> Products, int TotalCount)> GetProductsAsync(ProductFilterDto filter);
    Task<List<FinancingProductDto>> GetActiveProductsAsync();
    Task<List<FinancingProductDto>> GetShariaCompliantProductsAsync();
    Task<bool> DeactivateProductAsync(Guid productId);
    Task<bool> ActivateProductAsync(Guid productId);
    
    // Product Documents
    Task<ProductDocumentDto> UploadProductDocumentAsync(Guid productId, string documentName, string documentType, byte[] fileContent, string fileName, string contentType);
    Task<List<ProductDocumentDto>> GetProductDocumentsAsync(Guid productId);
    Task<byte[]> DownloadProductDocumentAsync(Guid documentId);
    Task<bool> DeleteProductDocumentAsync(Guid documentId);
    
    // Investment Availability
    Task<bool> IsInvestmentAvailableAsync(Guid productId, decimal amount);
    Task<decimal> GetAvailableInvestmentAmountAsync(Guid productId);
    Task<List<FinancingProductDto>> GetRecommendedProductsAsync(Guid investorId);
}