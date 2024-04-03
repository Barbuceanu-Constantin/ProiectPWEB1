using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Payment : BaseEntity
{
    public string PaymentMethod { get; set; } = default!;
    public float TotalPrice { get; set; } = default!;

    //order_id foreign_key
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
}

