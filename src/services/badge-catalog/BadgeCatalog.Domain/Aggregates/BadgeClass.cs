using BadgeCatalog.Domain.ValueObjects;
namespace BadgeCatalog.Domain.Aggregates;

public class BadgeClass
{
    #region Properties
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public BadgeImage Image { get; private set; }
    public BadgeCriteria Criteria { get; private set; }
    public bool IsActive { get; private set; }
    public int Version { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    #endregion
    #region Constructors
    private BadgeClass() { } // For EF Core
    public BadgeClass(string name, string description, BadgeImage image, BadgeCriteria criteria)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Image = image ?? throw new ArgumentNullException(nameof(image));
        Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        IsActive = true;
        Version = 1;
        CreatedAt = DateTime.UtcNow;
        UpdateAt = DateTime.UtcNow;
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
    private void SetImage(BadgeImage image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));
        Image = image;
    }
    private void SetCriteria(BadgeCriteria criteria)
    {
        if (criteria == null)
            throw new ArgumentNullException(nameof(criteria));
        Criteria = criteria;
    }
    public void Update(string name, string description)
    {
        SetName(name);
        SetDescription(description);

        Version++;
        UpdateAt = DateTime.UtcNow;
    }
    #endregion
}