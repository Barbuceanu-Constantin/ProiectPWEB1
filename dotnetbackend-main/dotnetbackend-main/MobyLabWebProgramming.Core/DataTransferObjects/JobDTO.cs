using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class JobDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public float Sal_min { get; set; } = default!;
    public float Sal_max { get; set; } = default!;
}
