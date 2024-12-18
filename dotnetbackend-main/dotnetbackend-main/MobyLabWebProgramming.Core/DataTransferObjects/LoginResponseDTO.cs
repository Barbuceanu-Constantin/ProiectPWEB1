﻿namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to respond to a login with the JWT token and user information.
/// </summary>
public class LoginResponseDTO
{
    public string Token { get; set; } = default!;
    public UserLoginDTO User { get; set; } = default!;  //Modificat de mine. Era UserDTO
}
