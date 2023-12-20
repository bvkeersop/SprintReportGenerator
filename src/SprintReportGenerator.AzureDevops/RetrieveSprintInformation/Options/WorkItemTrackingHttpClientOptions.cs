using Microsoft.VisualStudio.Services.Common;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Options;

public class WorkItemTrackingHttpClientOptions
{
    public Uri AzureDevOpsBaseUri { get; }
    public VssBasicCredential PersonalAccessToken { get; set; }

    public WorkItemTrackingHttpClientOptions(string azureDevOpsBaseUri, string personalAccessToken)
    {
        AzureDevOpsBaseUri = new Uri(azureDevOpsBaseUri);
        PersonalAccessToken = new VssBasicCredential(string.Empty, personalAccessToken);
    }

    // TODO: Niet inchecken!
    public static WorkItemTrackingHttpClientOptions CreateDefault()
        => new("https://dev.azure.com/bartvankeersop", "mahq2w6bhl4wq6awf7hnzbskh54v7v7bctqal4ogy4ghpz3b4p6q");
}
