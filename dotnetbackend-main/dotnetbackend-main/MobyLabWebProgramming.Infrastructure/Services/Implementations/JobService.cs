using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using System.Diagnostics;

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
    public async Task<ServiceResponse<JobDTO>> GetJob(string title, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new JobSpecSecond(title), cancellationToken); // Get a job using a specification on the repository.

        return result != null ?
            ServiceResponse<JobDTO>.ForSuccess(result) :
            ServiceResponse<JobDTO>.FromError(CommonErrors.JobFailGet); // Pack the result or error into a ServiceResponse.
    }

    /// <summary>
    /// GetJob will provide the information about a job given its job Id.
    /// </summary>
    public async Task<ServiceResponse<JobDTO>> GetJob(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new JobProjectionSpec(id), cancellationToken); // Get a job using a specification on the repository.

        return result != null ?
            ServiceResponse<JobDTO>.ForSuccess(result) :
            ServiceResponse<JobDTO>.FromError(CommonErrors.JobFailGet); // Pack the result or error into a ServiceResponse.
    }
    /// <summary>
    /// GetJobs returns page with job information from the database.
    /// </summary>
    public async Task<ServiceResponse<PagedResponse<JobDTO>>> GetJobs(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new JobProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new JobProjectionSpec(pagination.Search), cancellationToken);

        if (toBeAdded != null)
        {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<JobDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<JobDTO>>.FromError(CommonErrors.JobFailGet);
    }

    /// <summary>
    /// AddJob adds an job
    /// </summary>
    public async Task<ServiceResponse> AddJob(AddJobDTO job, CancellationToken cancellationToken = default)
    {
        var smin = job.Sal_min;
        var smax = job.Sal_max;
        var result = await _repository.AddAsync(new Job
        {
            Title = job.Title,
            Sal_min = job.Title != "Client" ? job.Sal_min : 0,
            Sal_max = job.Title != "Client" ? job.Sal_max : 0
        }, cancellationToken); // A new entity is created and persisted in the database.

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.JobFailAdd);
    }
    /// <summary>
    /// UpdateJob updates a job
    /// </summary>
    public async Task<ServiceResponse> UpdateJob(AddJobDTO job, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new JobSpec(job.Title), cancellationToken);

        if (entity != null) // Verify if the job is not found, you cannot update an non-existing entity.
        {
            entity.Sal_max = job.Sal_max;
            entity.Sal_min = job.Sal_min;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.JobFailUpdate);
    }
    /// <summary>
    /// DeleteJob deletes a job
    /// </summary>
    public async Task<ServiceResponse> DeleteJobByTitle(DeleteJobDTO title, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new JobSpecThird(title.Title), cancellationToken);

        if (entity != null)
        {
            await _repository.DeleteAsync<Job>(entity.id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.JobFailDelete);
    }
    /// <summary>
    /// DeleteJob deletes a job
    /// </summary>
    public async Task<ServiceResponse> DeleteJobById(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new JobSpec(id), cancellationToken);

        if (entity != null)
        {
            await _repository.DeleteAsync<Job>(id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.JobFailDelete);
    }
}

