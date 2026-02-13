namespace BadgeCatalog.Application.Commands.CreateBadgeClass;

public sealed record CreateBadgeClass(
    string Name, 
    string Description, 
    string ImageUrl, 
    string CriteriaNarrative)
{
    
}