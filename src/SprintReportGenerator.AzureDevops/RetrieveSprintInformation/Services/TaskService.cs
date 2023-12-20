using Microsoft.TeamFoundation.Common;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

public interface ITaskService
{
    ValueTask<IEnumerable<Model.Task>> GetTasksAsync(IEnumerable<int> taskIds);
}

public class TaskService : WorkItemService, ITaskService
{
    public TaskService(IWorkItemClient workItemClient) : base(workItemClient) { }

    public async ValueTask<IEnumerable<Model.Task>> GetTasksAsync(IEnumerable<int> taskIds)
    {
        if (taskIds.IsNullOrEmpty())
        {
            return Enumerable.Empty<Model.Task>();
        }

        var tasks = await GetChildWorkItemsAsync(taskIds);
        return tasks.Select(s => new Model.Task(s));
    }
}
