using PortfolioManagement.Core.Enums;

namespace PortfolioManagement.Core.Entities;

public class Investment : BaseEntity
{
    public Guid InvestorId { get; set; }
    public Guid FinancingProductId { get; set; }
    public decimal InvestmentAmount { get; set; }
    public decimal ExpectedReturnRate { get; set; }
    public InvestmentStatus Status { get; set; } = InvestmentStatus.Pending;
    public DateTime InvestmentDate { get; set; }
    public DateTime ExpectedMaturityDate { get; set; }
    public DateTime? ActualMaturityDate { get; set; }
    public decimal TotalProfitEarned { get; set; } = 0;
    public decimal PrincipalRepaid { get; set; } = 0;
    public decimal OutstandingAmount => InvestmentAmount - PrincipalRepaid;
    public int PriorityRank { get; set; } // Lower number = higher priority (based on lowest return rate)
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? CancelledAt { get; set; }
    
    // Navigation properties
    public virtual Investor Investor { get; set; } = null!;
    public virtual FinancingProduct FinancingProduct { get; set; } = null!;
    public virtual ICollection<ProfitDistribution> ProfitDistributions { get; set; } = new List<ProfitDistribution>();
    public virtual ICollection<InvestmentTransaction> Transactions { get; set; } = new List<InvestmentTransaction>();
}