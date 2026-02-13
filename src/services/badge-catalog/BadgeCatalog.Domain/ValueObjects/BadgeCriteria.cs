using System.Runtime.CompilerServices;

namespace BadgeCatalog.Domain.ValueObjects;

public sealed class BadgeCriteria
{
    public string Narrative { get; }


    private BadgeCriteria() { } // For EF Core

    public BadgeCriteria(string narrative)
    {
        if (string.IsNullOrWhiteSpace(narrative))
            throw new ArgumentException("Narrative cannot be empty.", nameof(narrative));
        Narrative = narrative;
    }
}