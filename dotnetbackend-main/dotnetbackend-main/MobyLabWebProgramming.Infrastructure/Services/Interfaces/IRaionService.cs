using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IRaionService
{
    public Task<ServiceResponse<JobDTO>> GetRaion(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<RaionDTO>>> GetRaioane(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddRaion(RaionDTO raion, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateRaion(RaionDTO raion, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteRaion(Guid id, CancellationToken cancellationToken = default);
}

