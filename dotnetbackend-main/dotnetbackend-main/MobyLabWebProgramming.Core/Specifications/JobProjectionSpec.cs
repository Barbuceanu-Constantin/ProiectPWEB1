using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a specification to filter the user entities and map it to and UserDTO object via the constructors.
/// Note how the constructors call the base class's constructors. Also, this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class JobProjectionSpec : BaseSpec<JobProjectionSpec, Job, JobDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<Job, JobDTO>> Spec => e => new()
    {
        Id = e.Id,
        Title = e.Title,
        Sal_max = e.Sal_max,
        Sal_min = e.Sal_min
    };

    public JobProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public JobProjectionSpec(Guid id) : base(id)
    {
    }

    public JobProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = search != null ? $"%{search.Replace(" ", "%")}%" : "%"; // Match all titles if search is null

        Query.Where(e => EF.Functions.ILike(e.Title, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
                                                                  // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}

