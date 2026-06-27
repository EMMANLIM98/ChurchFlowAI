namespace ChurchFlow.Application.Abstractions;

public interface ICurrentTenant
{
    Guid TenantId { get; }
}
