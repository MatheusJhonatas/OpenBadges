using BadgeCatalog.Domain.ValueObjects;

namespace BadgeCatalog.Domain.Aggregates;

public sealed class BadgeClass
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
    public DateTime UpdatedAt { get; private set; }
    public string Slug { get; private set; }
    #endregion

    #region Constructors
    private BadgeClass() { } // EF Core

    public BadgeClass(string name, string description, BadgeImage image, BadgeCriteria criteria)
    {
        Id = Guid.NewGuid();

        SetName(name);
        SetDescription(description);
        SetImage(image);
        SetCriteria(criteria);

        IsActive = true;
        Version = 1;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    #endregion

    #region Methods

    public void Deactivate()
    {
        IsActive = false;
        Version++;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        Name = name;
        Slug = GenerateSlug(name);
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        Description = description;
    }

    private void SetImage(BadgeImage image)
    {
        Image = image ?? throw new ArgumentNullException(nameof(image));
    }

    private void SetCriteria(BadgeCriteria criteria)
    {
        Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
    }

    public void Update(string name, string description)
    {
        SetName(name);
        SetDescription(description);

        Version++;
        UpdatedAt = DateTime.UtcNow;
    }

    private string GenerateSlug(string name)
    {
        return name
            .ToLower()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace(",", "");
    }

    #endregion
}