using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TransactionProjectionSpec : BaseSpec<TransactionProjectionSpec, Transaction, TransactionDTO>
{
    protected override Expression<Func<Transaction, TransactionDTO>> Spec => e => new()
    {
        Id = e.Id,
        Quantity = e.Quantity,
        TotalPrice = e.TotalPrice,
        ProductId = e.ProductId,
        OrderId = e.OrderId
    };

    public TransactionProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public TransactionProjectionSpec(Guid orderId)
    {
        Query.Where(e => EF.Functions.ILike(e.OrderId.ToString(), orderId.ToString()));
    }

    public TransactionProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        var searchExpr = search != null ? $"%{search.Replace(" ", "%")}%" : "%"; // This will match all titles if search is null or empty

        Query.Where(e => EF.Functions.ILike(e.Id.ToString(), searchExpr)); // This is an example on how database specific expressions can be used via C# expressions.
                                                                           // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
