using ChurchFlow.Domain.Common;
using ChurchFlow.Domain.ValueObjects;

namespace ChurchFlow.Domain.Entities;

public class Member : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Email Email { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public bool IsActive { get; private set; }

    public Member(
        string firstName,
        string lastName,
        string email,
        DateTime dateOfBirth,
        Guid tenantId)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.", nameof(lastName));

        if (dateOfBirth > DateTime.UtcNow)
            throw new ArgumentException("Invalid date of birth.", nameof(dateOfBirth));

        Id = Guid.NewGuid();
        TenantId = tenantId;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = Email.Create(email);
        DateOfBirth = dateOfBirth;

        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void UpdateEmail(string email)
    {
        Email = Email.Create(email);
        UpdatedAt = DateTime.UtcNow;
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}
