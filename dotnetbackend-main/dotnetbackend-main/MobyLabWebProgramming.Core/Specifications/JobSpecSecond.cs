using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
using System.Linq.Expressions;
using MobyLabWebProgramming.Core.DataTransferObjects;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class JobSpecSecond : BaseSpec<JobSpecSecond, Job, JobDTO>
{
    protected override Expression<Func<Job, JobDTO>> Spec => e => new()
    {
        Id = e.Id,
        Title = e.Title,
        Sal_min = e.Sal_min,
        Sal_max = e.Sal_max
    };

    public JobSpecSecond(Guid id) : base(id)
    {
    }

    public JobSpecSecond(string title)
    {
        Query.Where(e => e.Title == title);
    }
}
