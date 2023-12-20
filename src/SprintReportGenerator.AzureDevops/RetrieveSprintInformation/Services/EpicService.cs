using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Exceptions;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

public interface IEpicService
{
    Task<Epic> GetEpicAsync(int id);
}

public class EpicService : WorkItemService, IEpicService
{
    public EpicService(IWorkItemClient workItemClient) : base(workItemClient) { }

    public async Task<Epic> GetEpicAsync(int id)
    {
        var epic = await GetParentWorkItemAsync(id);

        if (epic == null)
        {
            var message = $"No epic could be found with id {id}";
            throw new GenerateSprintReportException(GenerateSprintReportErrorCode.NoFeatureCouldBeFound, message);
        }

        return new Epic(epic);
    }
}
