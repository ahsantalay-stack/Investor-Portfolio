using PortfolioManagement.Application.DTOs;

namespace PortfolioManagement.Application.Interfaces;

public interface IInvestmentService
{
    Task<InvestmentDto> CreateInvestmentAsync(Guid investorId, CreateInvestmentDto createInvestmentDto);
    Task<InvestmentDto?> GetInvestmentByIdAsync(Guid investmentId);
    Task<List<InvestmentDto>> GetInvestorInvestmentsAsync(Guid investorId);
    Task<(IEnumerable<InvestmentDto> Investments, int TotalCount)> GetInvestmentsAsync(
        int pageIndex = 0,
        int pageSize = 50,
        Guid? investorId = null,
        Guid? productId = null);
    
    // Priority Allocation System
    Task<InvestmentAllocationDto> ProcessInvestmentAllocationAsync(Guid productId, decimal requestedAmount);
    Task<List<InvestmentDto>> GetPriorityQueueForProductAsync(Guid productId);
    Task RecalculatePriorityRanksAsync(Guid productId);
    
    // Portfolio Management
    Task<InvestmentSummaryDto> GetInvestmentSummaryAsync(Guid investorId);
    Task<PortfolioPerformanceDto> GetPortfolioPerformanceAsync(Guid investorId);
    Task<List<MonthlyPerformanceDto>> GetMonthlyPerformanceAsync(Guid investorId, int year);
    
    // Investment Operations
    Task<bool> CancelInvestmentAsync(Guid investmentId, string reason);
    Task<bool> CompleteInvestmentAsync(Guid investmentId);
    Task<bool> ActivateInvestmentAsync(Guid investmentId);
    
    // Dashboard Data
    Task<object> GetInvestorDashboardDataAsync(Guid investorId);
}