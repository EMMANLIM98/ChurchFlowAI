namespace ChurchFlow.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public string Email { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool IsActive { get; private set; }

    private Member() { } // For EF Core

    public Member(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        if (dateOfBirth > DateTime.UtcNow)
            throw new ArgumentException("Invalid date of birth");

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email.ToLowerInvariant();
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
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");

        Email = email.ToLowerInvariant();
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}