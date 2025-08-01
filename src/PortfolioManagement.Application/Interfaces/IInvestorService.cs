using PortfolioManagement.Application.DTOs;
using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Application.Interfaces;

public interface IInvestorService
{
    Task<InvestorDto> CreateInvestorAsync(CreateInvestorDto createInvestorDto);
    Task<InvestorDto> UpdateInvestorAsync(Guid investorId, UpdateInvestorDto updateInvestorDto);
    Task<InvestorDto?> GetInvestorByIdAsync(Guid investorId);
    Task<InvestorDto?> GetInvestorByEmailAsync(string email);
    Task<InvestorDto?> GetInvestorByNationalIdAsync(string nationalId);
    Task<(IEnumerable<InvestorDto> Investors, int TotalCount)> GetInvestorsAsync(
        int pageIndex = 0, 
        int pageSize = 50, 
        KycStatus? kycStatus = null,
        string? searchTerm = null);
    Task<bool> DeactivateInvestorAsync(Guid investorId);
    Task<bool> ActivateInvestorAsync(Guid investorId);
    
    // KYC Operations
    Task<KycStatusDto> GetKycStatusAsync(Guid investorId);
    Task<KycDocumentDto> UploadKycDocumentAsync(Guid investorId, UploadKycDocumentDto uploadDto);
    Task<bool> VerifyKycDocumentAsync(VerifyKycDocumentDto verifyDto);
    Task<bool> ApproveKycAsync(Guid investorId);
    Task<bool> RejectKycAsync(Guid investorId, string rejectionReason);
    Task<List<KycDocumentDto>> GetInvestorDocumentsAsync(Guid investorId);
    Task<byte[]> DownloadKycDocumentAsync(Guid documentId);
    Task<bool> DeleteKycDocumentAsync(Guid documentId);
}