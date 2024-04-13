using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITransactionService
{
    public Task<ServiceResponse<TransactionDTO>> GetTransaction(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<TransactionDTO>>> GetTransactions(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddTransaction(TransactionDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateTransaction(TransactionDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteTransaction(Guid id, CancellationToken cancellationToken = default);
}
