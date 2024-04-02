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
/// This service will be uses to mange provider information.
/// As most routes and business logic will need to know what job is currently using the backend this service will be the most used.
/// </summary>
public class ProviderService : IProviderService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public ProviderService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ProviderDTO>> GetProvider(Guid id, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse<PagedResponse<ProviderDTO>>> GetProviders(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse> AddProvider(ProviderDTO provider, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Provider
        {
            Id = provider.Id,
            Name = provider.Name,
            CountryOfOrigin = provider.CountryOfOrigin
        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProvider(ProviderDTO provider, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse> DeleteProvider(Guid id, CancellationToken cancellationToken = default)
    {
        return null;
    }
}

