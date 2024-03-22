using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class Receipt : BaseEntity
{
    //provider_id foreign_key
    public Guid CashierId { get; set; }
    public User Cashier { get; set; } = default!;
}
