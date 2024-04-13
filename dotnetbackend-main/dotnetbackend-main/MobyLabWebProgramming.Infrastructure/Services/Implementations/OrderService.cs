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
using System.Xml.Linq;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange provider information.
/// As most routes and business logic will need to know what job is currently using the backend this service will be the most used.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public OrderService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<OrderDTO>> GetOrder(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new OrderSpecSecond(id), cancellationToken); // Get a raion using a specification on the repository.

        return result != null ?
            ServiceResponse<OrderDTO>.ForSuccess(result) :
            ServiceResponse<OrderDTO>.FromError(CommonErrors.OrderFailGet); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrdersForAdmin(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new OrderProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new OrderProjectionSpec(pagination.Search), cancellationToken);

        if (toBeAdded != null)
        {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<OrderDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<OrderDTO>>.FromError(CommonErrors.OrderFailGet);
    }

    public async Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrdersForClient(PaginationSearchQueryParams pagination, Guid clientId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new OrderProjectionSpec(clientId), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new OrderProjectionSpec(clientId), cancellationToken);

        if (toBeAdded != null)
        {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<OrderDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<OrderDTO>>.FromError(CommonErrors.OrderFailGet);
    }

    public async Task<ServiceResponse<int>> GetOrderCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Product>(cancellationToken)); // Get the count of all raion entities in the database.

    public async Task<ServiceResponse> AddOrder(AddOrderDTO order, CancellationToken cancellationToken = default)
    {
        var result = await _repository.AddAsync(new Order
        {
           ClientId = order.ClientId
        }, cancellationToken);          // A new entity is created and persisted in the database.

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.OrderFailAdd);
    }

    public async Task<ServiceResponse> UpdateOrder(OrderDTO order, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new OrderSpec(order.Id), cancellationToken);

        if (entity != null)
        {
            entity.ClientId = order.ClientId;
            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.OrderFailUpdate);
    }

    public async Task<ServiceResponse> DeleteOrder(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new OrderSpec(id), cancellationToken);

        if (entity != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            await _repository.DeleteAsync<Order>(entity.Id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.OrderFailDelete);
    }
}

