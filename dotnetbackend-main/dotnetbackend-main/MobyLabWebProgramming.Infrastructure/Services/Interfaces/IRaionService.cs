using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IRaionService
{
    public Task<ServiceResponse<RaionDTO>> GetRaion(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<RaionDTO>>> GetRaioane(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddRaion(AddRaionDTO raion, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateRaion(UpdateRaionDTO raion, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateRaionProvidersList(UpdateRaionProvidersListDTO raion, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteRaion(Guid id, CancellationToken cancellationToken = default);
}

