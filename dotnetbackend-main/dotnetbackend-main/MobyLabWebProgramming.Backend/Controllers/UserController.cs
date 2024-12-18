﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// This is a controller example for CRUD operations on users.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class UserController : AuthorizedController // Here we use the AuthorizedController as the base class because it derives ControllerBase and also has useful methods to retrieve user information.
{
    IJobService JobService;
    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public UserController(IJobService jobService, IUserService userService) : base(userService) // Also, you may pass constructor parameters to a base class constructor and call as specific constructor from the base class.
    {
        JobService = jobService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<UserDetailsDTO>>> GetCurrentUserDetails()
    {
        var currentUser = await GetCurrentUserEnhanced();
        var userDetails =  UserService.GetUserDetails(ExtractClaims());

        if (currentUser.Result != null)
        {
            if (userDetails.Result.Result != null && userDetails.Result.Result.JobTitle != null)
            {
                var jobDetails = JobService.GetJob(userDetails.Result.Result.JobTitle);

                if (jobDetails.Result.Result != null)
                {
                    userDetails.Result.Result.Salary = currentUser.Result.Salary;
                    userDetails.Result.Result.Commission = currentUser.Result.Commission;
                    if (currentUser.Result.Role != UserRoleEnum.Client)
                    {
                        userDetails.Result.Result.SalMin = jobDetails.Result.Result.Sal_min;
                        userDetails.Result.Result.SalMax = jobDetails.Result.Result.Sal_max;
                    }

                    userDetails.Result.Result.Role = userDetails.Result.Result.Role.Split(",")[0].Substring(9).Trim('\\').Trim('\"');

                    return this.FromServiceResponse(userDetails.Result);
                } else
                {
                    return this.ErrorMessageResult<UserDetailsDTO>(CommonErrors.UserFailGet);
                }
            } else
            {
                return this.ErrorMessageResult<UserDetailsDTO>(CommonErrors.UserFailGet);
            }
        } else
        {
            return this.ErrorMessageResult<UserDetailsDTO>(currentUser.Error);
        }
    }

    /// <summary>
    /// This method implements the Read operation (R from CRUD) on a user. 
    /// </summary>
    [Authorize] // You need to use this attribute to protect the route access, it will return a Forbidden status code if the JWT is not present or invalid, and also it will decode the JWT token.
    [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<UserDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin ||
                currentUser.Result.Id == id)
            {
                return this.FromServiceResponse(await UserService.GetUser(id));
            } else
            {
                return this.ErrorMessageResult<UserDTO>(CommonErrors.UserFailGet);
            }
        }

        return this.ErrorMessageResult<UserDTO>(currentUser.Error);
    }

    /// <summary>
    /// This method implements the Read operation (R from CRUD) on page of users.
    /// Generally, if you need to get multiple values from the database use pagination if there are many entries.
    /// It will improve performance and reduce resource consumption for both client and server.
    /// </summary>
    [Authorize]
    [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/User/GetPage.
    public async Task<ActionResult<RequestResponse<PagedResponse<UserDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination) // The FromQuery attribute will bind the parameters matching the names of
                                                                                                                                         // the PaginationSearchQueryParams properties to the object in the method parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return this.FromServiceResponse(await UserService.GetUsers(pagination));
            } else
            {
                return this.ErrorMessageResult<PagedResponse<UserDTO>>(CommonErrors.UserFailGet);
            }
        }

        return this.ErrorMessageResult<PagedResponse<UserDTO>>(currentUser.Error);
    }

    /// <summary>
    /// This method implements the Create operation (C from CRUD) of a user. 
    /// </summary>
    [Authorize] ///Aici am facut modificarea cand am vorbit cu el. Acum am refacut cum era.
    [HttpPost("AddStaff")] // This attribute will make the controller respond to a HTTP POST request on the route /api/User/Add.
    public async Task<ActionResult<RequestResponse>> Add([FromBody] UserAddStaffDTO user)
    {
        var currentUser = await GetCurrentUser();
        user.Password = PasswordUtils.HashPassword(user.Password);

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return this.FromServiceResponse(await UserService.AddStaffUser(user, null));

            } else
            {
                return this.ErrorMessageResult(CommonErrors.UserFailAdd);
            }
        }
        else
        {
            return this.ErrorMessageResult(currentUser.Error);
        }
    }

    /// <summary>
    /// This method implements the Create operation (C from CRUD) of a user. 
    /// </summary>
    //Aici am scos Authorize pentru ca un client trebuie sa se poata inregistra fara sa fie autorizat.
    [HttpPost("AddClient")] // This attribute will make the controller respond to a HTTP POST request on the route /api/User/Add.
    public async Task<ActionResult<RequestResponse>> Add([FromBody] UserAddClientDTO user)
    {
        var currentUser = await GetCurrentUser();
        user.Password = PasswordUtils.HashPassword(user.Password);

        return this.FromServiceResponse(await UserService.AddClientUser(user, null));

    }

    /// <summary>
    /// This method implements the Update operation (U from CRUD) on a user. 
    /// </summary>
    [Authorize]
    [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/User/Update.
    public async Task<ActionResult<RequestResponse>> Update([FromBody] UserUpdateDTO user) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin ||
                currentUser.Result.Id == user.Id)
            {
                return
                this.FromServiceResponse(await UserService.UpdateUser(user with
                {
                    Password = !string.IsNullOrWhiteSpace(user.Password) ? PasswordUtils.HashPassword(user.Password) : null
                }, currentUser.Result));
            } else
            {
                return this.ErrorMessageResult(CommonErrors.UserFailUpdate);
            }
        }
        else
        {
            return this.ErrorMessageResult(currentUser.Error);
        }
    }

    /// <summary>
    /// This method implements the Delete operation (D from CRUD) on a user.
    /// Note that in the HTTP RFC you cannot have a body for DELETE operations.
    /// </summary>
    [Authorize]
    [HttpDelete("{id:guid}")] // This attribute will make the controller respond to a HTTP DELETE request on the route /api/User/Delete/<some_guid>.
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin ||
                currentUser.Result.Id == id)
            {
                return this.FromServiceResponse(await UserService.DeleteUser(id));
            } else
            {
                return this.ErrorMessageResult(CommonErrors.UserFailDelete);
            }
        } else
        {
            return this.ErrorMessageResult(currentUser.Error);
        }
    }
}
