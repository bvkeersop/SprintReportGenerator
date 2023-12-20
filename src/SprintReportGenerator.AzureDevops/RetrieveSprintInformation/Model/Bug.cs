namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model
{
    public class Bug : WorkItem
    {
        IEnumerable<PullRequest> PullRequests { get; } = Enumerable.Empty<PullRequest>();
        public Bug(Tfs.WorkItem tfsWorkItem) : base(tfsWorkItem) { }
    }
}
