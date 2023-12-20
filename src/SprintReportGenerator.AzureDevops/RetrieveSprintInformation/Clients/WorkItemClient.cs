using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;
using SprintReportGenerator.AzureDevops.WorkItemQueryLanguage;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;

public interface IWorkItemClient
{
    Task<IEnumerable<Tfs.WorkItem>> GetWorkItemsAsync(Model.WorkItemType workItemType);
    Task<Tfs.WorkItem?> GetParentWorkItemAsync(int parentId);
    Task<IEnumerable<Tfs.WorkItem>> GetChildWorkItems(IEnumerable<int> childIds);
}

internal class WorkItemClient : IWorkItemClient
{
    private readonly WorkItemTrackingHttpClient _client;

    public WorkItemClient(WorkItemTrackingHttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Tfs.WorkItem>> GetWorkItemsAsync(Model.WorkItemType workItemType)
    {
        var wiqlQuery = new WiqlQueryBuilder()
            .Select(Constants.All)
            .From(Constants.Workitems)
            .Where(Constants.WorkItemType, Operator.Equals, workItemType.ToWiql())
            .Build();

        var result = await _client.QueryByWiqlAsync(wiqlQuery);
        var workItemIds = result.WorkItems.Select(w => w.Id);
        return await _client.GetWorkItemsAsync(workItemIds, fields: null, asOf: null, WorkItemExpand.Relations);
    }

    public async Task<Tfs.WorkItem?> GetParentWorkItemAsync(int parentId) =>
        await _client.GetWorkItemAsync(parentId, fields: null, asOf: null, WorkItemExpand.Relations);

    public async Task<IEnumerable<Tfs.WorkItem>> GetChildWorkItems(IEnumerable<int> childIds) =>
        await _client.GetWorkItemsAsync(childIds, fields: null, asOf: null, WorkItemExpand.Relations);
}
