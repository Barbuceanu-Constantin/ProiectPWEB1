using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AddJobDTO
{
    public string Title { get; set; }
    public float Sal_min { get; set; } = default!;
    public float Sal_max { get; set; } = default!;
}
