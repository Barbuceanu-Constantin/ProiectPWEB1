using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IJobService
{
    public Task<ServiceResponse<JobDTO>> GetJob(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<JobDTO>>> GetJobs(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddJob(AddJobDTO job, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateJob(JobDTO job, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteJob(Guid id, CancellationToken cancellationToken = default);
}
