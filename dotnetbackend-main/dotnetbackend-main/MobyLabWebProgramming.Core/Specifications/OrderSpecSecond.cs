using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class OrderSpecSecond : BaseSpec<OrderSpecSecond, Order, OrderDTO>
{
    protected override Expression<Func<Order, OrderDTO>> Spec => e => new()
    {
        Id = e.Id,
        ClientId = e.ClientId
    };

    public OrderSpecSecond(Guid id) : base(id)
    {
    }
}
