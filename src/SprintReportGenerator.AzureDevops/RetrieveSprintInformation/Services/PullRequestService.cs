using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

public interface IPullRequestService
{
    Task<IEnumerable<PullRequest>> GetPullRequestsAsync(IEnumerable<int> pullRequestIds);
}

public class PullRequestService : WorkItemService, IPullRequestService
{
    public PullRequestService(IWorkItemClient workItemClient) : base(workItemClient) { }

    public async Task<IEnumerable<PullRequest>> GetPullRequestsAsync(IEnumerable<int> pullRequestIds)
    {
        throw new NotImplementedException();
        //if (pullRequestIds.IsNullOrEmpty())
        //{
        //    return Enumerable.Empty<PullRequest>();
        //}

        //var pullRequests = await GetChildWorkItemsAsync(pullRequestIds);
        //return pullRequests.Select(s => new PullRequest(s));
    }
}
