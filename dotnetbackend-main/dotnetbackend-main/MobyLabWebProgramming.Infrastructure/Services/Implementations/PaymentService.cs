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
public class PaymentService : IPaymentService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public PaymentService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<PaymentDTO>> GetPayment(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new PaymentProjectionSpec(id), cancellationToken); // Get a raion using a specification on the repository.

        return result != null ?
            ServiceResponse<PaymentDTO>.ForSuccess(result) :
            ServiceResponse<PaymentDTO>.FromError(CommonErrors.PaymentFailGet); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<PaymentDTO>>> GetPayments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new PaymentProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        var toBeAdded = await _repository.ListAsync(new PaymentProjectionSpec(pagination.Search), cancellationToken);

        if (toBeAdded != null)
        {
            result.Data = toBeAdded;
        };

        return result != null ? ServiceResponse<PagedResponse<PaymentDTO>>.ForSuccess(result) :
                                ServiceResponse<PagedResponse<PaymentDTO>>.FromError(CommonErrors.PaymentFailGet);
    }

    public async Task<ServiceResponse<int>> GetPaymentCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Product>(cancellationToken)); // Get the count of all raion entities in the database.

    public async Task<ServiceResponse> AddPayment(AddPaymentDTO payment, CancellationToken cancellationToken = default)
    {
        var result = await _repository.AddAsync(new Payment
        {
            OrderId = payment.OrderId,
            TotalPrice = payment.TotalPrice,
            PaymentMethod = payment.PaymentMethod
        }, cancellationToken);          // A new entity is created and persisted in the database.

        return result != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.PaymentFailAdd);
    }

    public async Task<ServiceResponse> UpdatePayment(PaymentDTO payment, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new PaymentSpec(payment.Id), cancellationToken);

        if (entity != null)
        {
            entity.OrderId = payment.OrderId;
            entity.TotalPrice = payment.TotalPrice;
            entity.PaymentMethod = payment.PaymentMethod;
            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.PaymentFailUpdate);
    }

    public async Task<ServiceResponse> DeletePayment(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new PaymentSpec(id), cancellationToken);

        if (entity != null) // Verify if the raion is not found, you cannot update an non-existing entity.
        {
            await _repository.DeleteAsync<Payment>(entity.Id, cancellationToken); // Delete the entity.
        }

        return entity != null ? ServiceResponse.ForSuccess() :
                                ServiceResponse.FromError(CommonErrors.PaymentFailDelete);
    }
}
