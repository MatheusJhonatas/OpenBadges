namespace BadgeCatalog.Domain.Aggregates;

public class BadgeClass
{
    #region Properties
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public bool IsActive { get; private set; }
    #endregion
    #region Constructors
    private BadgeClass() { } // For EF Core
    public BadgeClass(string name, string description, string imageUrl)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        IsActive = true;
    }
    #endregion
    #region Methods
    public void Desactivate() => IsActive = false;
    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        Name = name;
    }
    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");
        Description = description;
    }
    private void SetImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Image URL cannot be empty.");
        ImageUrl = imageUrl;
    }
    #endregion
}