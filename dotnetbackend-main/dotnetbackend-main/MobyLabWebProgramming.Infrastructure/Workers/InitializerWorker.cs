using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Diagnostics;

namespace MobyLabWebProgramming.Infrastructure.Workers;

/// <summary>
/// This is an example of a worker service, this service is called on the applications start to do some asynchronous work.
/// </summary>
public class InitializerWorker : BackgroundService
{
    private readonly ILogger<InitializerWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public InitializerWorker(ILogger<InitializerWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger; // The logger instance is injected here.
        _serviceProvider = serviceProvider; // Here the service provider is injected to request other components on runtime at request.
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            await using var scope1 = _serviceProvider.CreateAsyncScope();        // Here a new scope is created, this is useful to get new scoped instances.
            var userService = scope1.ServiceProvider.GetService<IUserService>(); // Here an instance for a service is requested, it may fail if the component is not declared or
                                                                                 // an exception is thrown on it’s creation.

            //Eu
            await using var scope2 = _serviceProvider.CreateAsyncScope();
            var jobService = scope2.ServiceProvider.GetService<IJobService>();

            await using var scope3 = _serviceProvider.CreateAsyncScope();
            var providerService = scope3.ServiceProvider.GetService<IProviderService>();

            await using var scope4 = _serviceProvider.CreateAsyncScope();
            var raionService = scope4.ServiceProvider.GetService<IRaionService>();
            //

            if (userService == null)
            {
                _logger.LogInformation("Couldn't create the user service!");

                return;
            }

            //Adaugat de mine
            if (jobService == null)
            {
                _logger.LogInformation("Couldn't create the job service!");

                return;
            }

            if (raionService == null)
            {
                _logger.LogInformation("Couldn't create the raion service!");

                return;
            }

            if (providerService == null)
            {
                _logger.LogInformation("Couldn't create the provider service!");

                return;
            }
            //Cod adaugat de mine

            var count = await userService.GetUserCount(cancellationToken);

            if (count.Result == 0)
            {
                _logger.LogInformation("No user found, adding default user!");

                /////////////////////////////////////////////
                await jobService.AddJob(new()
                {
                    Title = "Admin",
                    Sal_min = 10000,
                    Sal_max = 20000
                }
                );
                /////////////////////////////////////////////

                /////////////////////////////////////////////
                await userService.AddStaffUser(new()
                {
                    Email = "admin@default.com",
                    Name = "Admin",
                    Role = UserRoleEnum.Admin,
                    Password = PasswordUtils.HashPassword("default"),
                    JobTitle = "Admin"
                }, cancellationToken: cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing database!");
        }
    }
}