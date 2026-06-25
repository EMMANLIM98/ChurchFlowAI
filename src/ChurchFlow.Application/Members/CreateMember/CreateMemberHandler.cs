using ChurchFlow.Application.Abstractions;
using ChurchFlow.Domain.Entities;

namespace ChurchFlow.Application.Members.CreateMember;

public class CreateMemberHandler
{
    private readonly IMemberRepository _memberRepository;
    private readonly ICurrentTenant _currentTenant;

    public CreateMemberHandler(
        IMemberRepository memberRepository,
        ICurrentTenant currentTenant)
    {
        _memberRepository = memberRepository;
        _currentTenant = currentTenant;
    }

    public async Task<Result> Handle(CreateMemberCommand request)
    {
        var existing = await _memberRepository
            .GetByEmailAsync(request.Email);

        if (existing != null)
            return Result.Failure("Member already exists.");

        var member = new Member(
            request.FirstName,
            request.LastName,
            request.Email,
            request.DateOfBirth,
            _currentTenant.TenantId
        );

        await _memberRepository.AddAsync(member);

        return Result.Success();
    }
}