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
public class ProductService : IProductService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public ProductService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ProductProjectionSpec(id), cancellationToken); // Get a raion using a specification on the repository.

        return result != null ?
            ServiceResponse<ProductDTO>.ForSuccess(result) :
            ServiceResponse<ProductDTO>.FromError(CommonErrors.ProductFailGet); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProductProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new ProductProjectionSpec(pagination.Search), cancellationToken);

        if (toBeAdded != null)
        {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<ProductDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<ProductDTO>>.FromError(CommonErrors.ProductFailGet);
    }

    public async Task<ServiceResponse<int>> GetProductCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Product>(cancellationToken)); // Get the count of all raion entities in the database.

    public async Task<ServiceResponse> AddProduct(AddProductDTO product, CancellationToken cancellationToken = default)
    {
        var result = await _repository.AddAsync(new Product
        {
            Name = product.Name,
            Description = product.Description,
            Warranty = product.Warranty,
            Price = product.Price,
            Quantity = product.Quantity,
            RaionId = product.RaionId,
            ProviderId = product.ProviderId,
        }, cancellationToken);          // A new entity is created and persisted in the database.

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.ProductFailAdd);
    }

    public async Task<ServiceResponse> UpdateProduct(UpdateProductDTO product, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new ProductSpec(product.OldName), cancellationToken);

        if (entity != null)
        {
            entity.Name = product.NewName ?? entity.Name;
            entity.Description = product.Description ?? entity.Description;
            entity.Warranty = product.Warranty ?? entity.Warranty;
            entity.Price = product.Price != 0 ? product.Price : entity.Price;
            entity.Quantity = product.Quantity != 0 ? product.Quantity : entity.Quantity;
            entity.RaionId = product.RaionId != entity.RaionId ? product.RaionId : entity.RaionId;
            entity.ProviderId = product.ProviderId != entity.ProviderId ? product.ProviderId : entity.RaionId;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.ProductFailUpdate);
    }

    public async Task<ServiceResponse> DeleteProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new ProductSpec(id), cancellationToken);

        if (entity != null)
        {
            await _repository.DeleteAsync<Order>(entity.Id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.ProductFailDelete);
    }

    public async Task<ServiceResponse> DeleteProduct(string name, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new ProductSpec(name), cancellationToken);

        if (entity != null)
        {
            await _repository.DeleteAsync<Product>(entity.Id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.ProductFailDelete);
    }
}
