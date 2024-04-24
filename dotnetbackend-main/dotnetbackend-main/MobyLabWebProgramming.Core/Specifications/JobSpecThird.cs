using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
using System.Linq.Expressions;
using MobyLabWebProgramming.Core.DataTransferObjects;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class JobSpecThird : BaseSpec<JobSpecThird, Job, DeleteJobDTO>
{
    protected override Expression<Func<Job, DeleteJobDTO>> Spec => e => new()
    {
        id = e.Id,
        Title = e.Title
    };

    public JobSpecThird(Guid id) : base(id)
    {
    }

    public JobSpecThird(string title)
    {
        Query.Where(e => e.Title == title);
    }
}
