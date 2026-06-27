using ChurchFlow.Application.Abstractions;
using ChurchFlow.Application.Common;
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

    public async Task<Result> Handle(CreateMemberCommand request, CancellationToken cancellationToken = default)
    {
        var existing = await _memberRepository
            .GetByEmailAsync(_currentTenant.TenantId, request.Email, cancellationToken);

        if (existing != null)
            return Result.Failure("Member already exists.");

        var member = new Member(
            request.FirstName,
            request.LastName,
            request.Email,
            request.DateOfBirth,
            _currentTenant.TenantId
        );

        await _memberRepository.AddAsync(member, cancellationToken);

        return Result.Success();
    }
}
