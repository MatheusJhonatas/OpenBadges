
using Issuance.Domain.Enums;
using Issuance.Domain.Events;
using Issuance.Domain.ValueObjects;

namespace Issuance.Domain.Aggregates;

public sealed class Assertion
{
    #region Properties
    private readonly List<object> _domainEvents = new();
    public Guid Id { get; private set; }
    public Guid BadgeClassId { get; private set; }
    public RecipientIdentity Recipient { get; private set; }
    public DateTime IssuedOn { get; private set; }
    public EAssertionStatus Status { get; private set; }
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
        Status = EAssertionStatus.Active;

        AddDomainEvent(new BadgeIssuedEvent(
            Id, 
            BadgeClassId, 
            Recipient.HashedEmail, 
            IssuedOn
        ));
    }
    public void Revoke()
    {
        if (Status == EAssertionStatus.Revoked)
            throw new InvalidOperationException("Assertion is already revoked.");

        Status = EAssertionStatus.Revoked;
        RevokedOn = DateTime.UtcNow;

         AddDomainEvent(new BadgeRevokedEvent(
        Id,
        BadgeClassId,
        Recipient.HashedEmail,
        RevokedOn.Value
    ));
    }
    public void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    // public static Assertion Issue(Guid badgeClassId, RecipientIdentity recipient)
    // {
    // var assertion = new Assertion(badgeClassId, recipient);

    // // exemplo futuro:
    // // assertion.AddDomainEvent(new BadgeIssuedEvent(assertion.Id));

    // return assertion;
    // }
    #endregion
}