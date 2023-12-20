using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

public class Task : WorkItem
{
    public const string PullRequestUrlStartingPattern = "vstfs:///Git/PullRequestId/";

    public TaskType TaskType { get; }
    public IEnumerable<PullRequest> PullRequests { get; private set; } = Enumerable.Empty<PullRequest>();

    public Task(Tfs.WorkItem tfsWorkItem) : base(tfsWorkItem)
    {
        TaskType = tfsWorkItem.GetTaskType();

        // Note: this method just get's the PR information from the workitem.
        // If more information about the PR is needed, a call needs to be made using a GitHttpClient.
        // This is left out since for now I'm only interested in the PR id.
        PullRequests = GetPullRequests(tfsWorkItem);
    }

    private static IEnumerable<PullRequest> GetPullRequests(Tfs.WorkItem tfsWorkItem)
      => tfsWorkItem.Relations
        .Where(w => w.Rel == Relation.Artifact.ToWiql() && IsPullRequestUrl(w.Url))
        .Select(r => GetPullRequest(r));

    private static bool IsPullRequestUrl(string url)
        => url.StartsWith(PullRequestUrlStartingPattern);

    private static PullRequest GetPullRequest(WorkItemRelation workItemRelation)
        => PullRequest.CreateFromUrl(workItemRelation.Url);
}
