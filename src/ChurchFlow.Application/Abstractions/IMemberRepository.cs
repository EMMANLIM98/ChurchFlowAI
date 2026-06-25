using ChurchFlow.Domain.Entities;

namespace ChurchFlow.Application.Abstractions;

public interface IMemberRepository
{
    Task AddAsync(Member member);

    Task<Member?> GetByIdAsync(Guid id);
}