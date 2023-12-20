using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

public interface IUserStoryService
{
    Task<IEnumerable<UserStory>> GetUserStories();
}

public class UserStoryService : IUserStoryService
{
    private readonly IWorkItemClient _workItemClient;

    public UserStoryService(IWorkItemClient workItemClient)
    {
        _workItemClient = workItemClient;
    }

    public async Task<IEnumerable<UserStory>> GetUserStories()
    {
        var userStoriesTfs = await _workItemClient.GetWorkItemsAsync(WorkItemType.UserStory);
        return userStoriesTfs.Select(u => new UserStory(u));
    }
}
