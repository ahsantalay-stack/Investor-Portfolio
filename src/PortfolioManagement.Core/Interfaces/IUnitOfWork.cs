using PortfolioManagement.Core.Entities;

namespace PortfolioManagement.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Investor> Investors { get; }
    IRepository<FinancingProduct> FinancingProducts { get; }
    IRepository<Investment> Investments { get; }
    IRepository<KycDocument> KycDocuments { get; }
    IRepository<ProfitDistribution> ProfitDistributions { get; }
    IRepository<InvestmentTransaction> InvestmentTransactions { get; }
    IRepository<ProductDocument> ProductDocuments { get; }
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}