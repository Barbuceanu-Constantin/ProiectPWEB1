using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class RaionProjectionSpec : BaseSpec<RaionProjectionSpec, Raion, RaionDTO>
{
    protected override Expression<Func<Raion, RaionDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        SefRaionId = e.SefRaionId,
        //Providers = e.Providers
    };

    public RaionProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public RaionProjectionSpec(Guid id) : base(id)
    {
    }

    public RaionProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = search != null ? $"%{search.Replace(" ", "%")}%" : "%"; // Match all titles if search is null

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
                                                                   // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
