using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProviderService
{
    public Task<ServiceResponse<ProviderDTO>> GetProvider(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<ProviderDTO>>> GetProviders(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddProvider(AddProviderDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateProvider(UpdateProviderDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateProviderRaioaneList(UpdateRaionProvidersListDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteProvider(Guid id, CancellationToken cancellationToken = default);
}
