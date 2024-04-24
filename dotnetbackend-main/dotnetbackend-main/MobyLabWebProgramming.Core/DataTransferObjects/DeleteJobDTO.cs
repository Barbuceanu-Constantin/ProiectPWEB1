using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class DeleteJobDTO
{
    public Guid id = default!;
    public string Title { get; set; } = default!;
}
