using BadgeCatalog.Domain.ValueObjects;
namespace BadgeCatalog.Domain.Aggregates;

public sealed class Assertion
{
    #region Properties
    private readonly List<object> _domainEvents = new();
    public Guid Id { get; private set; }
    public Guid BadgeClassId { get; private set; }
    public RecipientIdentity Recipient { get; private set; }
    public DateTime IssuedOn { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? RevokedOn { get; private set; }
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();
    #endregion
    #region Constructors
    private Assertion() { } // For EF Core
    public Assertion(Guid badgeClassId, RecipientIdentity recipient)
    {
        if (badgeClassId == Guid.Empty)
            throw new ArgumentException("BadgeClassId cannot be empty.");

        Id = Guid.NewGuid();
        BadgeClassId = badgeClassId;
        Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        IssuedOn = DateTime.UtcNow;
        IsRevoked = false;
    }
    public void Revoke()
    {
        if (IsRevoked)
            throw new InvalidOperationException("Assertion is already revoked.");

        IsRevoked = true;
        RevokedOn = DateTime.UtcNow;
    }
    public void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    #endregion
}