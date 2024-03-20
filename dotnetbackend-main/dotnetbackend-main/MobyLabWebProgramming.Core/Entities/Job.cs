using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Job : BaseEntity
{
    public string Title { get; set; } = default!;
    public float Sal_min { get; set; } = default!;
    public float Sal_max { get; set; } = default!;
    public ICollection<User> Users { get; set; } = default!;
}


