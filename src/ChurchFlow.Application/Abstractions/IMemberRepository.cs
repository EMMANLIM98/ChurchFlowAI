using ChurchFlow.Domain.Entities;

namespace ChurchFlow.Application.Abstractions;

public interface IMemberRepository
{
    Task AddAsync(Member member, CancellationToken cancellationToken = default);

    Task<Member?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default);

    Task<Member?> GetByEmailAsync(Guid tenantId, string email, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Member>> ListAsync(
        Guid tenantId,
        int skip,
        int take,
        string sortBy,
        bool sortDescending,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
