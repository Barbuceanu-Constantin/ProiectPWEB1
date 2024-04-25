using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using Org.BouncyCastle.Asn1.X509;

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// This is a controller example for CRUD operations on users.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class TransactionController : AuthorizedController // Here we use the AuthorizedController as the base class because it derives ControllerBase and also has useful methods to retrieve user information.
{
    private readonly ITransactionService _transactionService;
    private readonly IOrderService _orderService;
    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public TransactionController(ITransactionService transactionService, 
                                 IOrderService orderService, 
                                 IUserService userService) : base(userService) // Also, you may pass constructor parameters to a base class constructor and call as specific constructor from the base class.
    {
        _transactionService = transactionService;
        _orderService = orderService;
    }


    [Authorize]// You need to use this attribute to protect the route access, it will return a Forbidden status code if the JWT is not present or invalid, and also it will decode the JWT token.
    [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/Job/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<TransactionDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                var task = _transactionService.GetTransaction(id).Result;

                return task.Result != null ? this.FromServiceResponse(await _transactionService.GetTransaction(id)) :
                                             this.ErrorMessageResult<TransactionDTO>(task.Error);
            }
            else
            {
                var task = _transactionService.GetTransaction(id).Result;

                if (task.Result != null)
                {
                    var order = _orderService.GetOrder(task.Result.OrderId).Result;

                    if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Client)
                        if (order.Result != null)
                        {
                            if (currentUser.Result.Id == order.Result.ClientId)
                                return this.FromServiceResponse(await _transactionService.GetTransaction(id));
                            else
                            {
                                return this.ErrorMessageResult<TransactionDTO>(task.Error);
                            }
                        } else
                        {
                            return this.ErrorMessageResult<TransactionDTO>(CommonErrors.TransactionFailGet);
                        }
                    else
                    {
                        return this.ErrorMessageResult<TransactionDTO>(CommonErrors.TransactionFailGet);
                    }
                }
                else
                {
                    return this.ErrorMessageResult<TransactionDTO>(CommonErrors.TransactionFailGet);
                }
            }
        } else
        {
            return this.ErrorMessageResult<TransactionDTO>();
        }
    }

    [Authorize]
    [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/Job/GetPage.
    public async Task<ActionResult<RequestResponse<PagedResponse<TransactionDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return currentUser.Result != null ? this.FromServiceResponse(await _transactionService.GetTransactions(pagination)) :
                                                    this.ErrorMessageResult<PagedResponse<TransactionDTO>>(currentUser.Error);
            } else
            {
                return this.ErrorMessageResult<PagedResponse<TransactionDTO>>(CommonErrors.TransactionFailGet);
            }
        }
        else
        {
            return this.ErrorMessageResult<PagedResponse<TransactionDTO>>();
        }
    }

    [Authorize]
    [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/Job/GetPage.
    public async Task<ActionResult<RequestResponse<PagedResponse<TransactionDTO>>>> GetTransactionsForOrder([FromQuery] PaginationSearchQueryParams pagination, Guid orderId)
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return currentUser.Result != null ? this.FromServiceResponse(await _transactionService.GetTransactionsForOrder(pagination, orderId)) :
                                                    this.ErrorMessageResult<PagedResponse<TransactionDTO>>(currentUser.Error);
            }
            else
            {
                var order = _orderService.GetOrder(orderId).Result;

                if (order.Result != null)
                {
                    if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Client)
                        if (order.Result.ClientId == currentUser.Result.Id)
                            return this.FromServiceResponse(await _transactionService.GetTransactionsForOrder(pagination, orderId));
                        else
                            return this.ErrorMessageResult<PagedResponse<TransactionDTO>>(CommonErrors.TransactionFailGet);
                    else
                        return this.ErrorMessageResult<PagedResponse<TransactionDTO>>(CommonErrors.TransactionFailGet);
                } else
                {
                    return this.ErrorMessageResult<PagedResponse<TransactionDTO>>(CommonErrors.TransactionFailGet);
                }
            }
        } else
        {
            return this.ErrorMessageResult<PagedResponse<TransactionDTO>>();
        }
    }

    [Authorize] ///Aici am facut modificarea cand am vorbit cu el. Acum am refacut cum era.
    [HttpPost] // This attribute will make the controller respond to a HTTP POST request on the route /api/Job/Add.
    public async Task<ActionResult<RequestResponse>> Add([FromBody] AddTransactionDTO transaction)
    {
        var currentUser = await GetCurrentUser();
        var order = _orderService.GetOrder(transaction.OrderId).Result;

        if (currentUser.Result != null && order.Result != null)
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Client)
                if (order.Result.ClientId == currentUser.Result.Id)
                    return this.FromServiceResponse(await _transactionService.AddTransaction(transaction));

        return this.ErrorMessageResult();
    }

    /// <summary>
    /// This method implements the Update operation (U from CRUD) on a job. 
    /// </summary>
    [Authorize]
    [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/Job/Update.
    public async Task<ActionResult<RequestResponse>> Update([FromBody] TransactionDTO transaction) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return currentUser.Result != null ? this.FromServiceResponse(await _transactionService.UpdateTransaction(transaction)) :
                                                    this.ErrorMessageResult();
            } else
            {
                return this.ErrorMessageResult(CommonErrors.TransactionFailUpdate);
            }
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
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return currentUser.Result != null ? this.FromServiceResponse(await _transactionService.DeleteTransaction(id)) :
                                                    this.ErrorMessageResult(currentUser.Error);
            } else
            {
                return this.ErrorMessageResult(CommonErrors.TransactionFailDelete);
            }
        }
        else
        {
            return this.ErrorMessageResult();
        }
    }
}

