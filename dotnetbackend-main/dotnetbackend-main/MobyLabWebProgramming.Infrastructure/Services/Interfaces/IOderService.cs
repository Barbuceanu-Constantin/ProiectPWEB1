using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    public Task<ServiceResponse<OrderDTO>> GetOrder(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrdersForAdmin(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrdersForClient(PaginationSearchQueryParams pagination, Guid clientId, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddOrder(AddOrderDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateOrder(OrderDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteOrder(Guid id, CancellationToken cancellationToken = default);
}
