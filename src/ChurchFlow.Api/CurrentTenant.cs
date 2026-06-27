using ChurchFlow.Application.Abstractions;

namespace ChurchFlow.Api;

public class CurrentTenant : ICurrentTenant
{
    private static readonly Guid DevelopmentTenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentTenant(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid TenantId
    {
        get
        {
            var tenantHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].FirstOrDefault();

            return Guid.TryParse(tenantHeader, out var tenantId)
                ? tenantId
                : DevelopmentTenantId;
        }
    }
}
