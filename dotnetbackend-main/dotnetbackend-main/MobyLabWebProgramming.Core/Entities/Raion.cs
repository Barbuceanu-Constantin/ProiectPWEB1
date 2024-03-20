using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Raion : BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<User> Users { get; set; } = default!;
    public Guid SefRaionId { get; set; }
    public User user { get; set; } = default!;
}
