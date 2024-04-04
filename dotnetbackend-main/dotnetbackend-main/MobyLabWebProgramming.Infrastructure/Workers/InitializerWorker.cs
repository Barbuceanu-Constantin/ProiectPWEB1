using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            await using var scope1 = _serviceProvider.CreateAsyncScope(); // Here a new scope is created, this is useful to get new scoped instances.
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
                _logger.LogInformation("Couldn't add null job!");

                return;
            }

            if (raionService == null)
            {
                _logger.LogInformation("Couldn't add null raion!");

                return;
            }

            if (providerService == null)
            {
                _logger.LogInformation("Couldn't add null provider!");

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
                    Id = new Guid("00000001-0000-0000-0000-000000000000".ToString()),
                    Title = "JobVid",
                    Sal_min = 0,
                    Sal_max = 0
                }
                );

                await jobService.AddJob(new()
                {
                    Id = new Guid("00000002-0000-0000-0000-000000000000".ToString()),
                    Title = "Admin",
                    Sal_min = 10000,
                    Sal_max = 20000
                }
                );

                await jobService.AddJob(new()
                {
                    Id = new Guid("00000003-0000-0000-0000-000000000000".ToString()),
                    Title = "ManagerRaion",
                    Sal_min = 5000,
                    Sal_max = 150000
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
                    JobId = new Guid("00000001-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await userService.AddStaffUser(new()
                {
                    Email = "manager1@default.com",
                    Name = "manager1",
                    Role = UserRoleEnum.ManagerRaion,
                    Password = PasswordUtils.HashPassword("manager1"),
                    JobId = new Guid("00000002-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await userService.AddStaffUser(new()
                {
                    Email = "manager2@default.com",
                    Name = "manager2",
                    Role = UserRoleEnum.ManagerRaion,
                    Password = PasswordUtils.HashPassword("manager2"),
                    JobId = new Guid("00000002-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await userService.AddStaffUser(new()
                {
                    Email = "manager3@default.com",
                    Name = "manager3",
                    Role = UserRoleEnum.ManagerRaion,
                    Password = PasswordUtils.HashPassword("manager3"),
                    JobId = new Guid("00000002-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await userService.AddStaffUser(new()
                {
                    Email = "manager4@default.com",
                    Name = "manager4",
                    Role = UserRoleEnum.ManagerRaion,
                    Password = PasswordUtils.HashPassword("manager4"),
                    JobId = new Guid("00000002-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await userService.AddStaffUser(new()
                {
                    Email = "manager5@default.com",
                    Name = "manager5",
                    Role = UserRoleEnum.ManagerRaion,
                    Password = PasswordUtils.HashPassword("manager5"),
                    JobId = new Guid("00000002-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);
                /////////////////////////////////////////////

                /////////////////////////////////////////////
                await raionService.AddRaionInit(new()
                {
                    Id = new Guid("00000001-0000-0000-0000-000000000000".ToString()),
                    Name = "Gradina",
                    SefRaionId = new Guid("00000002-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await raionService.AddRaionInit(new()
                {
                    Id = new Guid("00000002-0000-0000-0000-000000000000".ToString()),
                    Name = "Bucatarie",
                    SefRaionId = new Guid("00000003-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await raionService.AddRaionInit(new()
                {
                    Id = new Guid("00000003-0000-0000-0000-000000000000".ToString()),
                    Name = "Electrocasnice",
                    SefRaionId = new Guid("00000004-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await raionService.AddRaionInit(new()
                {
                    Id = new Guid("00000004-0000-0000-0000-000000000000".ToString()),
                    Name = "Scule si unelte",
                    SefRaionId = new Guid("00000005-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);

                await raionService.AddRaionInit(new()
                {
                    Id = new Guid("00000005-0000-0000-0000-000000000000".ToString()),
                    Name = "Auto",
                    SefRaionId = new Guid("00000006-0000-0000-0000-000000000000".ToString())
                }, cancellationToken: cancellationToken);
                /////////////////////////////////////////////

                /////////////////////////////////////////////
                await providerService.AddProviderInit(new()
                {
                    Id = new Guid("00000001-0000-0000-0000-000000000000".ToString()),
                    Name = "Gardena",
                    CountryOfOrigin = "Romania"
                }, cancellationToken: cancellationToken);

                await providerService.AddProviderInit(new()
                {
                    Id = new Guid("00000002-0000-0000-0000-000000000000".ToString()),
                    Name = "Husqvarna",
                    CountryOfOrigin = "Suedia"
                }, cancellationToken: cancellationToken);

                await providerService.AddProviderInit(new()
                {
                    Id = new Guid("00000003-0000-0000-0000-000000000000".ToString()),
                    Name = "Phillips",
                    CountryOfOrigin = "Olanda"
                }, cancellationToken: cancellationToken);

                await providerService.AddProviderInit(new()
                {
                    Id = new Guid("00000004-0000-0000-0000-000000000000".ToString()),
                    Name = "Kotarbau",
                    CountryOfOrigin = "Suedia"
                }, cancellationToken: cancellationToken);

                await providerService.AddProviderInit(new()
                {
                    Id = new Guid("00000005-0000-0000-0000-000000000000".ToString()),
                    Name = "Zanussi",
                    CountryOfOrigin = "Italia"
                }, cancellationToken: cancellationToken);
                /////////////////////////////////////////////
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing database!");
        }
    }
}