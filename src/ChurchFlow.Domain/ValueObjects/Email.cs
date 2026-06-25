using System.Text.RegularExpressions;

namespace ChurchFlow.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.");

        value = value.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(
                value,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new ArgumentException("Invalid email format.");
        }

        return new Email(value);
    }

    public override string ToString()
    {
        return Value;
    }
}