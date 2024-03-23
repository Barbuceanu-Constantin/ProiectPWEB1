using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Warranty { get; set; } = default!;
    public float Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    
    //provider_id foreign_key
    public Guid ProviderId { get; set; }
    public Provider Provider { get; set; } = default!;
    //command_id foreign_key
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;

    public ICollection<Transaction> Transactions { get; set; } = default!;
}

