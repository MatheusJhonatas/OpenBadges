public sealed class BadgeTemplateId
{
    public string Value { get; }

    private BadgeTemplateId() { } // EF

    public BadgeTemplateId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("TemplateId cannot be empty.", nameof(value));

        Value = value;
    }
}