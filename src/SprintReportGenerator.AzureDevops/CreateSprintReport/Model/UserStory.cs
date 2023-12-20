namespace SprintReportGenerator.AzureDevops.CreateSprintReport.Model;

public class UserStory
{
    public string? Title { get; set; } = "Unknown";
    public IEnumerable<PullRequest> PullRequests { get; set; } = Enumerable.Empty<PullRequest>();

    public static UserStory CreateFrom(RetrieveSprintInformation.Model.UserStory userstory) => new()
    {
        Title = userstory.Title,
        PullRequests = userstory.Tasks
            .SelectMany(task => task.PullRequests, (task, pr) => PullRequest.CreateFrom(pr))
    };
}
