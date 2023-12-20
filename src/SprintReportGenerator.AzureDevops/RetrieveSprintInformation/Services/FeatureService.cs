using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Exceptions;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

public interface IFeatureService
{
    Task<Feature> GetFeatureAsync(int id);
}

public class FeatureService : WorkItemService, IFeatureService
{
    public FeatureService(IWorkItemClient workItemClient) : base(workItemClient) { }

    public async Task<Feature> GetFeatureAsync(int id)
    {
        var feature = await GetParentWorkItemAsync(id);

        if (feature == null)
        {
            var message = $"No feature could be found with id {id}";
            throw new GenerateSprintReportException(GenerateSprintReportErrorCode.NoFeatureCouldBeFound, message);
        }

        return new Feature(feature);
    }
}
