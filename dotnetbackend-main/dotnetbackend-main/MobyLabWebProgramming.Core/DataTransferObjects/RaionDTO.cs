using MobyLabWebProgramming.Core.Entities;
using System.Collections.ObjectModel;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to transfer information about a user within the application and to client application.
/// Note that it doesn't contain a password property and that is why you should use DTO rather than entities to use only the data that you need or protect sensible information.
/// </summary>
public class RaionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid SefRaionId { get; set; }
}
