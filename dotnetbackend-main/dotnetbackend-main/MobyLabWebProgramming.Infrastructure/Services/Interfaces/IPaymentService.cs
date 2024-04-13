using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IPaymentService
{
    public Task<ServiceResponse<PaymentDTO>> GetPayment(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<PaymentDTO>>> GetPayments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddPayment(AddPaymentDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdatePayment(PaymentDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeletePayment(Guid id, CancellationToken cancellationToken = default);
}
