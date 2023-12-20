// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SprintReportGenerator.AzureDevops.CreateSprintReport;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Options;
using System.Reflection;

using IHost host =
    Host.CreateDefaultBuilder(args)
        .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
        .ConfigureServices((hostContext, services) =>
        {
            services
            .AddRetrieveSprintInformationRetriever(WorkItemTrackingHttpClientOptions.CreateDefault())
            .AddSprintReportCreator();
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        })
        .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider serviceProvider = serviceScope.ServiceProvider;

var sprintInformation = await serviceProvider
    .GetRequiredService<IRetrieveSprintInformation>()
    .RetrieveSprintInformation()
    .ConfigureAwait(false);

serviceProvider
    .GetRequiredService<ICreateSprintReports>()
    .CreateSprintReport(sprintInformation);

Console.ReadLine();