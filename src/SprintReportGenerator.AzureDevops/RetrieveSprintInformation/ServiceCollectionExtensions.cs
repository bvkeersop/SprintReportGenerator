using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Options;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRetrieveSprintInformationRetriever(this IServiceCollection services, WorkItemTrackingHttpClientOptions options)
    {
        return services
           .AddSingleton(options)
           .AddScoped<IRetrieveSprintInformation, SprintInformationRetriever>()
           .AddScoped<IWorkItemClient, WorkItemClient>()
           .AddScoped<IPullRequestService, PullRequestService>()
           .AddScoped<ITaskService, TaskService>()
           .AddScoped<IUserStoryService, UserStoryService>()
           .AddScoped<IFeatureService, FeatureService>()
           .AddScoped<IEpicService, EpicService>()
           .AddScoped<IConstructWorkItemGraphs, WorkItemGraphConstructor>()
           .AddSingleton(new WorkItemTrackingHttpClient(options.AzureDevOpsBaseUri, options.PersonalAccessToken));
    }
}
