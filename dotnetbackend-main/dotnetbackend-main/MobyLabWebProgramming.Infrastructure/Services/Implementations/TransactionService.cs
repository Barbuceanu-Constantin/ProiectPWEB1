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
        return null;
    }

    public async Task<ServiceResponse<PagedResponse<TransactionDTO>>> GetTransactions(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse<int>> GetTransactionCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Transaction>(cancellationToken)); // Get the count of all raion entities in the database.

    public async Task<ServiceResponse> AddTransaction(TransactionDTO payment, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse> UpdateTransaction(TransactionDTO payment, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<ServiceResponse> DeleteTransaction(Guid id, CancellationToken cancellationToken = default)
    {
        return null;
    }
}

