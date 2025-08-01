namespace PortfolioManagement.Core.Enums;

public enum KycStatus
{
    NotStarted = 0,
    InProgress = 1,
    DocumentsSubmitted = 2,
    UnderReview = 3,
    Approved = 4,
    Rejected = 5,
    RequiresAdditionalInfo = 6
}