﻿using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IJobService
{
    public Task<ServiceResponse<JobDTO>> GetJob(string title, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<JobDTO>> GetJob(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<JobDTO>>> GetJobs(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddJob(AddJobDTO job, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateJob(AddJobDTO job, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteJobByTitle(DeleteJobDTO title, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteJobById(Guid id, CancellationToken cancellationToken = default);
}
