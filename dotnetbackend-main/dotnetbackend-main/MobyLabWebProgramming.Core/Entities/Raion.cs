using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Raion : BaseEntity
{
    public string Name { get; set; } = default!;
    public Guid SefRaionId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Provider> Providers { get; set; } = default!;
    public ICollection<Product> Products { get; set; } = default!;
}
