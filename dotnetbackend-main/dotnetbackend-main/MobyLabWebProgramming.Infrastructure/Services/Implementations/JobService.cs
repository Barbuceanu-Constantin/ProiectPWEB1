using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange job information.
/// As most routes and business logic will need to know what job is currently using the backend this service will be the most used.
/// </summary>
public class JobService : IJobService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public JobService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    /// <summary>
    /// GetJob will provide the information about a job given its job Id.
    /// </summary>
    public Task<ServiceResponse<JobDTO>> GetJob(Guid id, CancellationToken cancellationToken = default)
    {
        return null;
    }
    /// <summary>
    /// GetJobs returns page with job information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<JobDTO>>> GetJobs(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        return null;
    }

    /// <summary>
    /// AddJob adds an job
    /// </summary>
    public async Task<ServiceResponse> AddJob(JobDTO job, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Job
        {
            Id = job.Id,
            Title = job.Title,
            Sal_min = job.Sal_min,
            Sal_max = job.Sal_max,
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }
    /// <summary>
    /// UpdateJob updates a job
    /// </summary>
    public Task<ServiceResponse> UpdateJob(JobDTO job, CancellationToken cancellationToken = default)
    {
        return null;
    }
    /// <summary>
    /// DeleteJob deletes a job
    /// </summary>
    public Task<ServiceResponse> DeleteJob(Guid id, CancellationToken cancellationToken = default)
    {
        return null;
    }
}

