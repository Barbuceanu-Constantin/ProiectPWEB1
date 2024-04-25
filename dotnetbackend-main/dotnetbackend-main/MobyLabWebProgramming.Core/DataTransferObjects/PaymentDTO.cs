﻿/// <summary>
/// This DTO is used to transfer information about a user within the application and to client application.
/// Note that it doesn't contain a password property and that is why you should use DTO rather than entities to use only the data that you need or protect sensible information.
/// </summary>
public class PaymentDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string PaymentMethod { get; set; } = default!;
}
