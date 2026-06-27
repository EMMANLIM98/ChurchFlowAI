using ChurchFlow.Application.Abstractions;
using ChurchFlow.Domain.Entities;

namespace ChurchFlow.Infrastructure.Repositories;

public class InMemoryMemberRepository : IMemberRepository
{
    private static readonly List<Member> Members = [];
    private static readonly object Lock = new();

    public Task AddAsync(Member member, CancellationToken cancellationToken = default)
    {
        lock (Lock)
        {
            Members.Add(member);
        }

        return Task.CompletedTask;
    }

    public Task<Member?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default)
    {
        lock (Lock)
        {
            return Task.FromResult(Members.FirstOrDefault(member =>
                member.TenantId == tenantId &&
                member.Id == id &&
                !member.IsDeleted));
        }
    }

    public Task<Member?> GetByEmailAsync(Guid tenantId, string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        lock (Lock)
        {
            return Task.FromResult(Members.FirstOrDefault(member =>
                member.TenantId == tenantId &&
                member.Email.Value == normalizedEmail &&
                !member.IsDeleted));
        }
    }

    public Task<IReadOnlyList<Member>> ListAsync(
        Guid tenantId,
        int skip,
        int take,
        string sortBy,
        bool sortDescending,
        CancellationToken cancellationToken = default)
    {
        lock (Lock)
        {
            var query = Members
                .Where(member => member.TenantId == tenantId && !member.IsDeleted);

            query = sortBy.Trim().ToLowerInvariant() switch
            {
                "firstname" => sortDescending
                    ? query.OrderByDescending(member => member.FirstName)
                    : query.OrderBy(member => member.FirstName),
                "email" => sortDescending
                    ? query.OrderByDescending(member => member.Email.Value)
                    : query.OrderBy(member => member.Email.Value),
                "createdat" => sortDescending
                    ? query.OrderByDescending(member => member.CreatedAt)
                    : query.OrderBy(member => member.CreatedAt),
                _ => sortDescending
                    ? query.OrderByDescending(member => member.LastName)
                    : query.OrderBy(member => member.LastName)
            };

            return Task.FromResult<IReadOnlyList<Member>>(query
                .Skip(skip)
                .Take(take)
                .ToList());
        }
    }

    public Task<int> CountAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        lock (Lock)
        {
            return Task.FromResult(Members.Count(member =>
                member.TenantId == tenantId &&
                !member.IsDeleted));
        }
    }
}
