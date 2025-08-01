using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Application.DTOs;

public class InvestmentDto
{
    public Guid Id { get; set; }
    public Guid InvestorId { get; set; }
    public Guid FinancingProductId { get; set; }
    public string InvestorName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public InvestmentStatus Status { get; set; }
    public string StatusText => Status.ToString();
    public DateTime InvestmentDate { get; set; }
    public DateTime ExpectedMaturityDate { get; set; }
    public DateTime? ActualMaturityDate { get; set; }
    public decimal TotalProfitEarned { get; set; }
    public decimal PrincipalRepaid { get; set; }
    public decimal OutstandingAmount { get; set; }
    public int PriorityRank { get; set; }
    public decimal ExpectedTotalReturn => InvestmentAmount * (1 + (ExpectedReturnRate / 100));
    public decimal ExpectedProfitAmount => ExpectedTotalReturn - InvestmentAmount;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateInvestmentDto
{
    public Guid FinancingProductId { get; set; }
    public decimal InvestmentAmount { get; set; }
    public string? Notes { get; set; }
}

public class InvestmentSummaryDto
{
    public Guid InvestorId { get; set; }
    public int TotalInvestments { get; set; }
    public int ActiveInvestments { get; set; }
    public int CompletedInvestments { get; set; }
    public decimal TotalInvestedAmount { get; set; }
    public decimal TotalProfitEarned { get; set; }
    public decimal TotalOutstandingAmount { get; set; }
    public decimal OverallReturnRate { get; set; }
    public List<InvestmentDto> RecentInvestments { get; set; } = new();
}

public class PortfolioPerformanceDto
{
    public Guid InvestorId { get; set; }
    public decimal TotalPortfolioValue { get; set; }
    public decimal TotalInvestedAmount { get; set; }
    public decimal TotalProfitEarned { get; set; }
    public decimal OverallReturnPercentage { get; set; }
    public List<InvestmentPerformanceDto> InvestmentPerformances { get; set; } = new();
    public List<MonthlyPerformanceDto> MonthlyPerformances { get; set; } = new();
}

public class InvestmentPerformanceDto
{
    public Guid InvestmentId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal ProfitEarned { get; set; }
    public decimal ReturnPercentage { get; set; }
    public InvestmentStatus Status { get; set; }
}

public class MonthlyPerformanceDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public decimal ProfitEarned { get; set; }
    public decimal InvestmentsMade { get; set; }
    public decimal NetCashFlow { get; set; }
}

public class InvestmentAllocationDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal RequestedAmount { get; set; }
    public decimal AllocatedAmount { get; set; }
    public int PriorityRank { get; set; }
    public bool IsFullyAllocated => RequestedAmount == AllocatedAmount;
    public string AllocationStatus { get; set; } = string.Empty;
}