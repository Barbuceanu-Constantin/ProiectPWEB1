using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProductService
{
    public Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddProduct(AddProductDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateProduct(UpdateProductDTO provider, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteProduct(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteProduct(string name, CancellationToken cancellationToken = default);
}
