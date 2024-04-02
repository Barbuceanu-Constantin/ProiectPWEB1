using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange raion information.
/// As most routes and business logic will need to know what job is currently using the backend this service will be the most used.
/// </summary>
public class RaionService : IRaionService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public RaionService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<JobDTO>> GetRaion(Guid id, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse<PagedResponse<RaionDTO>>> GetRaioane(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse> AddRaion(RaionDTO raion, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Raion
        {
            Id = raion.Id,
            Name = raion.Name,
            SefRaionId = raion.SefRaionId
        }, cancellationToken);          // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateRaion(RaionDTO raion, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse> DeleteRaion(Guid id, CancellationToken cancellationToken = default)
    {
        return null;
    }
}
