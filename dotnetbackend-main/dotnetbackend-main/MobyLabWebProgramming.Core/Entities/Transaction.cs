using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Transaction : BaseEntity
{
    public int Quantity { get; set; } = default!;
    public float TotalPrice { get; set; } = default!;

    //product_id foreign_key
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    //order_id foreign_key
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
}

