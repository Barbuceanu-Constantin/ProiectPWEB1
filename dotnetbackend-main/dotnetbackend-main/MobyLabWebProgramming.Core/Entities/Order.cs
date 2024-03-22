using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Order : BaseEntity
{

    //client(user)_id foreign_key
    public Guid ClientId { get; set; }
    public User Client { get; set; } = default!;
    public ICollection<Product> Products { get; set; } = default!;
}
