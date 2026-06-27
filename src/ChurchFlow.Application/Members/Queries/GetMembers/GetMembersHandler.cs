using ChurchFlow.Application.Abstractions;
using ChurchFlow.Application.Common;

namespace ChurchFlow.Application.Members.GetMembers;

public class GetMembersHandler
{
    private readonly IMemberRepository _memberRepository;
    private readonly ICurrentTenant _currentTenant;

    public GetMembersHandler(
        IMemberRepository memberRepository,
        ICurrentTenant currentTenant)
    {
        _memberRepository = memberRepository;
        _currentTenant = currentTenant;
    }

    public async Task<PagedResult<MemberDto>> Handle(
        GetMembersQuery query,
        CancellationToken cancellationToken = default)
    {
        var page = Math.Max(query.Page, 1);
        var pageSize = Math.Clamp(query.PageSize, 1, 100);
        var skip = (page - 1) * pageSize;
        var sortDescending = string.Equals(query.SortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        var members = await _memberRepository.ListAsync(
            _currentTenant.TenantId,
            skip,
            pageSize,
            query.SortBy,
            sortDescending,
            cancellationToken);

        var totalCount = await _memberRepository.CountAsync(_currentTenant.TenantId, cancellationToken);

        return new PagedResult<MemberDto>
        {
            Items = members
                .Select(member => new MemberDto
                {
                    Id = member.Id,
                    FullName = member.GetFullName(),
                    Email = member.Email.Value
                })
                .ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
