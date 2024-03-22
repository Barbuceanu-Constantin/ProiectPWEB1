using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class JoinProviderRaion : BaseEntity
{
    //provider_id foreign_key
    public Guid ProviderId { get; set; }
    public Provider Provider { get; set; } = default!;
    //command_id foreign_key
    public Guid RaionId { get; set; }
    public Raion Raion { get; set; } = default!;
}

