using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
using System.Linq.Expressions;
using MobyLabWebProgramming.Core.DataTransferObjects;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class TransactionSpecSecond : BaseSpec<TransactionSpecSecond, Transaction, TransactionDTO>
{
    protected override Expression<Func<Transaction, TransactionDTO>> Spec => e => new()
    {
        Id = e.Id,
        ProductId = e.ProductId,
        OrderId = e.OrderId,
        Quantity = e.Quantity,
        TotalPrice = e.TotalPrice
    };

    public TransactionSpecSecond(Guid id) : base(id)
    {
    }
}
