using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProviderProjectionSpec : BaseSpec<ProviderProjectionSpec, Provider, ProviderDTO>
{
    protected override Expression<Func<Provider,ProviderDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        CountryOfOrigin = e.CountryOfOrigin,
        //Raioane = e.Raioane
    };

    public ProviderProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ProviderProjectionSpec(Guid id) : base(id)
    {
    }

    public ProviderProjectionSpec(string? search)
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
