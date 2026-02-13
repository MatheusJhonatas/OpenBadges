namespace BadgeCatalog.Application.Commands.CreateBadgeClass;

public sealed record CreateBadgeClassCommand(
    string Name, 
    string Description, 
    string ImageUrl, 
    string CriteriaNarrative)
{
    
}