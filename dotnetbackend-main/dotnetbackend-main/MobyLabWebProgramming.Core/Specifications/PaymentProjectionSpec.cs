using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class PaymentProjectionSpec : BaseSpec<PaymentProjectionSpec, Payment, PaymentDTO>
{
    protected override Expression<Func<Payment, PaymentDTO>> Spec => e => new()
    {
        Id = e.Id,
        OrderId = e.OrderId,
        PaymentMethod = e.PaymentMethod
    };

    public PaymentProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public PaymentProjectionSpec(Guid id) : base(id)
    {
    }

    public PaymentProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = search != null ? $"%{search.Replace(" ", "%")}%" : "%"; // Match all titles if search is null

        Query.Where(e => EF.Functions.ILike(e.PaymentMethod, searchExpr));  // This is an example on who database specific expressions can be used via C# expressions.
                                                                            // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
