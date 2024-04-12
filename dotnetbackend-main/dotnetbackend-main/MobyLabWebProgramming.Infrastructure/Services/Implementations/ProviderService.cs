using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using System.Collections.ObjectModel;

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
        var result = await _repository.GetAsync(new ProviderProjectionSpec(id), cancellationToken); // Get a raion using a specification on the repository.

        return result != null ?
            ServiceResponse<ProviderDTO>.ForSuccess(result) :
            ServiceResponse<ProviderDTO>.FromError(CommonErrors.ProviderFailGet); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<ProviderDTO>>> GetProviders(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProviderProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new ProviderProjectionSpec(pagination.Search), cancellationToken);

        if (toBeAdded != null) {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<ProviderDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<ProviderDTO>>.FromError(CommonErrors.ProviderFailGet);
    }

    public async Task<ServiceResponse<int>> GetRaionCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Provider>(cancellationToken)); // Get the count of all raion entities in the database.

    public async Task<ServiceResponse> AddProvider(AddProviderDTO provider, CancellationToken cancellationToken = default)
    {
        var result = await _repository.AddAsync(new Provider
        {
            Name = provider.NameProvider,
            CountryOfOrigin = provider.CountryOfOrigin,
            Raioane = new Collection<Raion>()
        }, cancellationToken); // A new entity is created and persisted in the database.

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.ProviderFailAdd);
    }

    public async Task<ServiceResponse> UpdateProvider(UpdateProviderDTO provider, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new ProviderSpec(provider.OldName), cancellationToken);

        if (entity != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            entity.Name = provider.NewName ?? entity.Name;
            entity.CountryOfOrigin = provider.NewCountryOfOrigin ?? entity.CountryOfOrigin;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.ProviderFailUpdate);
    }

    public async Task<ServiceResponse> UpdateProviderRaioaneList(UpdateRaionProvidersListDTO provider, CancellationToken cancellationToken = default)
    {
        var entityProvider = await _repository.GetAsync(new ProviderSpec(provider.provider), cancellationToken);
        var entityRaion = await _repository.GetAsync(new RaionSpec(provider.raion), cancellationToken);

        if (entityProvider != null && entityRaion != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            if (entityProvider.Raioane == null)
            {
                entityProvider.Raioane = new Collection<Raion>(); // Initialize the Providers collection if it's null
            }

            entityProvider.Raioane.Add(entityRaion);

            await _repository.UpdateAsync(entityProvider, cancellationToken); // Update the entity and persist the changes.
        }

        return (entityProvider != null && entityRaion != null) ? ServiceResponse.ForSuccess() :
                                                                 ServiceResponse.FromError(CommonErrors.ProviderFailUpdate);
    }

    public async Task<ServiceResponse> DeleteProvider(string name, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new ProviderSpec(name), cancellationToken);

        if (entity != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            await _repository.DeleteAsync<Provider>(entity.Id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.ProviderFailUpdate);
    }
}

