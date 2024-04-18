using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to transfer information about a user within the application and to client application.
/// Note that it doesn't contain a password property and that is why you should use DTO rather than entities to use only the data that you need or protect sensible information.
/// </summary>
public class UserDetailsDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string HireDate { get; set; } = default!;
    public float Salary { get; set; } = default!;
    public float Commission { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string? JobTitle { get; set; } = default!;
    public float SalMin { get; set; } = default!;
    public float SalMax { get; set; } = default!;
}
