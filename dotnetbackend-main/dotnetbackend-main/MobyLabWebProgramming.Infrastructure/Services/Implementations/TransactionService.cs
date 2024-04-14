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
public class TransactionService : ITransactionService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public TransactionService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<TransactionDTO>> GetTransaction(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new TransactionSpecSecond(id), cancellationToken); // Get a raion using a specification on the repository.

        return result != null ?
            ServiceResponse<TransactionDTO>.ForSuccess(result) :
            ServiceResponse<TransactionDTO>.FromError(CommonErrors.TransactionFailGet); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<TransactionDTO>>> GetTransactions(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new TransactionProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new TransactionProjectionSpec(pagination.Search), cancellationToken);

        if (toBeAdded != null)
        {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<TransactionDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<TransactionDTO>>.FromError(CommonErrors.TransactionFailGet);
    }

    public async Task<ServiceResponse<PagedResponse<TransactionDTO>>> GetTransactionsForOrder(PaginationSearchQueryParams pagination,
                                                                                              Guid orderId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new TransactionProjectionSpec(orderId), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new TransactionProjectionSpec(orderId), cancellationToken);

        if (toBeAdded != null)
        {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<TransactionDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<TransactionDTO>>.FromError(CommonErrors.TransactionFailGet);
    }

    public async Task<ServiceResponse<int>> GetTransactionCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Transaction>(cancellationToken)); // Get the count of all raion entities in the database.

    public async Task<ServiceResponse> AddTransaction(AddTransactionDTO transaction, CancellationToken cancellationToken = default)
    {
        var result = await _repository.AddAsync(new Transaction
        {
            OrderId = transaction.OrderId,
            ProductId = transaction.ProductId,
            Quantity = transaction.Quantity,
            TotalPrice = transaction.TotalPrice
        }, cancellationToken);

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.TransactionFailAdd);
    }

    public async Task<ServiceResponse> UpdateTransaction(TransactionDTO transaction, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new TransactionSpec(transaction.Id), cancellationToken);

        if (entity != null)
        {
            entity.Quantity = transaction.Quantity;
            entity.TotalPrice = transaction.TotalPrice;
            entity.ProductId = transaction.ProductId;
            entity.OrderId = transaction.OrderId;
            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.TransactionFailUpdate);
    }

    public async Task<ServiceResponse> DeleteTransaction(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new TransactionSpec(id), cancellationToken);

        if (entity != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            await _repository.DeleteAsync<Transaction>(entity.Id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.TransactionFailDelete);
    }
}

