namespace SprintReportGenerator.AzureDevops.CreateSprintReport.Model;

public class PullRequest
{
    public string? DisplayValue { get; set; }

    public static PullRequest CreateFrom(RetrieveSprintInformation.Model.PullRequest pullRequest) => new()
    {
        DisplayValue = $"!{pullRequest.ProjectId}"
    };
}
