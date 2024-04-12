using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Provider : BaseEntity
{
    public string Name { get; set; } = default!;
    public string CountryOfOrigin { get; set; } = default!;
    public ICollection<Raion> Raioane { get; set; }
    public ICollection<Product> Products { get; set; }
}
