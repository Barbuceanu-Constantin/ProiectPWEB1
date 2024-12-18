﻿using Microsoft.AspNetCore.Authorization;
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

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// This is a controller example for CRUD operations on users.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class PaymentController : AuthorizedController // Here we use the AuthorizedController as the base class because it derives ControllerBase and also has useful methods to retrieve user information.
{
    private readonly IPaymentService _paymentService;
    private readonly IOrderService _orderService;
    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public PaymentController(IPaymentService paymentService, IOrderService orderService,
                             IUserService userService) : base(userService) // Also, you may pass constructor parameters to a base class constructor and call as specific constructor from the base class.
    {
        _paymentService = paymentService;
        _orderService = orderService;
    }


    [Authorize]// You need to use this attribute to protect the route access, it will return a Forbidden status code if the JWT is not present or invalid, and also it will decode the JWT token.
    [HttpGet("{id:guid}")] // This attribute will make the controller respond to a HTTP GET request on the route /api/Job/GetById/<some_guid>.
    public async Task<ActionResult<RequestResponse<PaymentDTO>>> GetById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                var task = _paymentService.GetPayment(id).Result;

                return this.FromServiceResponse(await _paymentService.GetPayment(id));
            }
            else
            {
                var task = _paymentService.GetPayment(id).Result;

                if (task.Result != null)
                {
                    var order = _orderService.GetOrder(task.Result.OrderId).Result;


                    if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Client)
                        if (order.Result != null && currentUser.Result.Id == order.Result.ClientId)
                            return task.Result != null ? this.FromServiceResponse(await _paymentService.GetPayment(id)) :
                                                         this.ErrorMessageResult<PaymentDTO>(task.Error);
                }

                return this.ErrorMessageResult<PaymentDTO>(CommonErrors.PaymentFailGet);
            }
        } else
        {
            return this.ErrorMessageResult<PaymentDTO>();
        }
    }

    [Authorize]
    [HttpGet] // This attribute will make the controller respond to a HTTP GET request on the route /api/Job/GetPage.
    public async Task<ActionResult<RequestResponse<PagedResponse<PaymentDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return this.FromServiceResponse(await _paymentService.GetPayments(pagination));
            } else
            {
                return this.ErrorMessageResult<PagedResponse<PaymentDTO>>(CommonErrors.PaymentFailGet);
            }
        }
        else
        {
            return this.ErrorMessageResult<PagedResponse<PaymentDTO>>();
        }
    }

    [Authorize] ///Aici am facut modificarea cand am vorbit cu el. Acum am refacut cum era.
    [HttpPost] // This attribute will make the controller respond to a HTTP POST request on the route /api/Job/Add.
    public async Task<ActionResult<RequestResponse>> Add([FromBody] AddPaymentDTO payment)
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Client)
            {
                return this.FromServiceResponse(await _paymentService.AddPayment(payment));
            } else
            {
                return this.ErrorMessageResult(CommonErrors.PaymentFailAdd);
            }
        }
        else
        {
            return this.ErrorMessageResult();
        }
    }

    /// <summary>
    /// This method implements the Update operation (U from CRUD) on a job. 
    /// </summary>
    [Authorize]
    [HttpPut] // This attribute will make the controller respond to a HTTP PUT request on the route /api/Job/Update.
    public async Task<ActionResult<RequestResponse>> Update([FromBody] PaymentDTO payment) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
    {
        //Doar adminul are voie update pentru ca odata ce tranzactia
        //e facuta clientul nu mai poate modifica metoda de plata.
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return this.FromServiceResponse(await _paymentService.UpdatePayment(payment));
            } else
            {
                return this.ErrorMessageResult(CommonErrors.PaymentFailUpdate);
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
    public async Task<ActionResult<RequestResponse>> DeleteById([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        if (currentUser.Result != null)
        {
            if (currentUser.Result.Role == Core.Enums.UserRoleEnum.Admin)
            {
                return this.FromServiceResponse(await _paymentService.DeletePayment(id));
            } else
            {
                return this.ErrorMessageResult(CommonErrors.PaymentFailDelete);
            }
        }
        else
        {
            return this.ErrorMessageResult();
        }
    }
}
