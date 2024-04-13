using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class OrderProjectionSpec : BaseSpec<OrderProjectionSpec, Order, OrderDTO>
{
    protected override Expression<Func<Order, OrderDTO>> Spec => e => new()
    {
        Id = e.Id,
        ClientId = e.ClientId
    };

    public OrderProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public OrderProjectionSpec(Guid clientId)
    {
        Query.Where(e => EF.Functions.ILike(e.ClientId.ToString(), clientId.ToString()));
    }

    public OrderProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        var searchExpr = search != null ? $"%{search.Replace(" ", "%")}%" : "%"; // This will match all titles if search is null or empty

        Query.Where(e => EF.Functions.ILike(e.Id.ToString(), searchExpr)); // This is an example on how database specific expressions can be used via C# expressions.
                                                                           // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
