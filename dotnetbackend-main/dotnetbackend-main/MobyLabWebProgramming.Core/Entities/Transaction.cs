using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Transaction : BaseEntity
{
    public int Quantity { get; set; } = default!;
    //provider_id foreign_key
    public Guid ReceiptId { get; set; }
    public Receipt Receipt { get; set; } = default!;
    //command_id foreign_key
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
}

