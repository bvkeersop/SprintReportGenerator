using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

public abstract class WorkItemService
{
    private readonly IWorkItemClient _workItemClient;

    public WorkItemService(IWorkItemClient workItemClient)
    {
        _workItemClient = workItemClient;
    }

    public async ValueTask<Tfs.WorkItem?> GetParentWorkItemAsync(int parentId) =>
        await _workItemClient.GetParentWorkItemAsync(parentId);

    public async Task<IEnumerable<Tfs.WorkItem>> GetChildWorkItemsAsync(IEnumerable<int> childIds)
        => await _workItemClient.GetChildWorkItems(childIds);
}
