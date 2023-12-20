using Microsoft.Extensions.DependencyInjection;

namespace SprintReportGenerator.AzureDevops.CreateSprintReport;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSprintReportCreator(this IServiceCollection services)
        => services.AddScoped<ICreateSprintReports, SprintReportCreator>();
}
