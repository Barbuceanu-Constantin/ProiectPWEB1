using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange raion information.
/// As most routes and business logic will need to know what job is currently using the backend this service will be the most used.
/// </summary>
public class RaionService : IRaionService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private static int counter = 0;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public RaionService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<RaionDTO>> GetRaion(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new RaionProjectionSpec(id), cancellationToken); // Get a raion using a specification on the repository.

        return result != null ?
            ServiceResponse<RaionDTO>.ForSuccess(result) :
            ServiceResponse<RaionDTO>.FromError(CommonErrors.RaionFailGet); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<RaionDTO>>> GetRaioane(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new RaionProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return result != null ? ServiceResponse<PagedResponse<RaionDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<RaionDTO>>.FromError(CommonErrors.RaionFailGet);
    }

    public async Task<ServiceResponse<int>> GetRaionCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Raion>(cancellationToken)); // Get the count of all raion entities in the database.

    public async Task<ServiceResponse> AddRaionInit(RaionDTO raion, CancellationToken cancellationToken = default)
    {
        var result = await _repository.AddAsync(new Raion
        {
            Id = raion.Id,
            Name = raion.Name,
            SefRaionId = raion.SefRaionId,
            Providers = new Collection<Provider>()
        }, cancellationToken);          // A new entity is created and persisted in the database.

        if (result != null) counter++;

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.RaionFailAdd);
    }

    public async Task<ServiceResponse> AddRaion(AddRaionDTO raion, CancellationToken cancellationToken = default)
    {
        ////////////////////////////////////////////////////////////////////////
        // Convert the integer to a byte array
        byte[] bytes = BitConverter.GetBytes(counter + 1);

        // Pad the byte array to ensure it's 16 bytes long (required for GUID)
        byte[] paddedBytes = new byte[16];
        Array.Copy(bytes, paddedBytes, Math.Min(bytes.Length, 16));

        // Create a new GUID using the byte array
        Guid guidValue = new Guid(paddedBytes);
        ////////////////////////////////////////////////////////////////////////

        var result = await _repository.AddAsync(new Raion
        {
            Id = guidValue,
            Name = raion.Name,
            SefRaionId = raion.SefRaionId,
            Providers = new Collection<Provider>()
        }, cancellationToken);          // A new entity is created and persisted in the database.

        if ( result != null ) counter++;

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.RaionFailAdd);
    }

    public async Task<ServiceResponse> UpdateRaion(UpdateRaionDTO raion, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new RaionSpec(raion.OldName), cancellationToken);

        if (entity != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            entity.Name = raion.NewName ?? entity.Name;
            entity.SefRaionId = raion.SefRaionId;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateRaionProvidersList(UpdateRaionProvidersListDTO raion, CancellationToken cancellationToken = default)
    {
        var entityRaion = await _repository.GetAsync(new RaionSpec(raion.raion), cancellationToken);
        var entityProvider = await _repository.GetAsync(new ProviderSpec(raion.provider), cancellationToken);

        if (entityRaion != null && entityProvider != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            if (entityRaion.Providers == null)
            {
                entityRaion.Providers = new Collection<Provider>(); // Initialize the Providers collection if it's null
            }

            entityRaion.Providers.Add(entityProvider);

            await _repository.UpdateAsync(entityRaion, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteRaion(string name, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new RaionSpec(name), cancellationToken);

        if (entity != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            await _repository.DeleteAsync<Raion>(entity.Id, cancellationToken); // Delete the entity.
        }

        return ServiceResponse.ForSuccess();
    }
}
