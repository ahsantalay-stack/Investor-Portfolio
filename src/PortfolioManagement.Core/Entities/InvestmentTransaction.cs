namespace PortfolioManagement.Core.Entities;

public enum TransactionType
{
    Investment = 1,
    ProfitPayment = 2,
    PrincipalRepayment = 3,
    Cancellation = 4,
    Adjustment = 5
}

public class InvestmentTransaction : BaseEntity
{
    public Guid InvestmentId { get; set; }
    public Guid InvestorId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Reference { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsReversed { get; set; } = false;
    public Guid? ReversalTransactionId { get; set; }
    public string? PaymentMethod { get; set; }
    public string? ExternalReference { get; set; }
    
    // Navigation properties
    public virtual Investment Investment { get; set; } = null!;
    public virtual Investor Investor { get; set; } = null!;
    public virtual InvestmentTransaction? ReversalTransaction { get; set; }
}