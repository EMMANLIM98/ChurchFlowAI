using ChurchFlow.Domain.ValueObjects;

namespace ChurchFlow.Application.Members.CreateMember;

public class CreateMemberCommand
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}