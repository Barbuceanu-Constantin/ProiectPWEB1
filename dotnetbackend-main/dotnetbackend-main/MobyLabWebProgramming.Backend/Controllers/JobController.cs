using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// This is a controller example for CRUD operations on users.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class JobController : AuthorizedController // Here we use the AuthorizedController as the base class because it derives ControllerBase and also has useful methods to retrieve user information.
{
    private readonly IJobService _jobService;
    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public JobController(IJobService jobService, IUserService userService) : base(userService) // Also, you may pass constructor parameters to a base class constructor and call as specific constructor from the base class.
    {
        _jobService = jobService;
    }


    [Authorize]// You need to use this attribute to protect the route access, it will return a Forbidden status code if the JWT is not present or invalid, and also it will decode the JWT token.
    [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/Job/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<JobDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser =  await GetCurrentUser();

        if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin) { 
            var task = _jobService.GetJob(id).Result;

            return task.Result != null ? this.FromServiceResponse(await _jobService.GetJob(id)) :
                                         this.ErrorMessageResult<JobDTO>(task.Error);
        } else {
            return this.ErrorMessageResult<JobDTO>();
        }
    }


    [Authorize]
    [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/Job/GetPage.
    public async Task<ActionResult<RequestResponse<PagedResponse<JobDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
        {
            return currentUser.Result != null ? this.FromServiceResponse(await _jobService.GetJobs(pagination)) :
                                                this.ErrorMessageResult<PagedResponse<JobDTO>>(currentUser.Error);
        }
        else
        {
            return this.ErrorMessageResult<PagedResponse<JobDTO>>();
        }
    }

    [Authorize] ///Aici am facut modificarea cand am vorbit cu el. Acum am refacut cum era.
    [HttpPost] // This attribute will make the controller respond to a HTTP POST request on the route /api/Job/Add.
    public async Task<ActionResult<RequestResponse>> Add([FromBody] AddJobDTO job)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
        {
            return currentUser.Result != null ? this.FromServiceResponse(await _jobService.AddJob(job)) :
                                                this.ErrorMessageResult();
        }
        else {
            return this.ErrorMessageResult();
        }
    }

    /// <summary>
    /// This method implements the Update operation (U from CRUD) on a job. 
    /// </summary>
    [Authorize]
    [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/Job/Update.
    public async Task<ActionResult<RequestResponse>> Update([FromBody] AddJobDTO job) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
        {
            return currentUser.Result != null ? this.FromServiceResponse(await _jobService.UpdateJob(job)) :
                                                this.ErrorMessageResult();
        }
        else
        {
            return this.ErrorMessageResult();
        }
    }

    /// <summary>
    /// Note that in the HTTP RFC you cannot have a body for DELETE operations.
    /// </summary>
    [Authorize]
    [HttpDelete] // This attribute will make the controller respond to a HTTP DELETE request on the route /api/Job/Delete/<some_guid>.
    public async Task<ActionResult<RequestResponse>> DeleteByTitle([FromBody] DeleteJobDTO title) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
        {
            return currentUser.Result != null ? this.FromServiceResponse(await _jobService.DeleteJobByTitle(title)) :
                                                this.ErrorMessageResult(currentUser.Error);
        }
        else
        {
            return this.ErrorMessageResult();
        }
    }

    /// <summary>
    /// Note that in the HTTP RFC you cannot have a body for DELETE operations.
    /// </summary>
    [Authorize]
    [HttpDelete("{id:guid}")] // This attribute will make the controller respond to a HTTP DELETE request on the route /api/Job/Delete/<some_guid>.
    public async Task<ActionResult<RequestResponse>> DeleteById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
        {
            return currentUser.Result != null ? this.FromServiceResponse(await _jobService.DeleteJobById(id)) :
                                                this.ErrorMessageResult(currentUser.Error);
        }
        else
        {
            return this.ErrorMessageResult();
        }
    }
}

