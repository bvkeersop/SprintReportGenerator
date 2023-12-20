using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Exceptions;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

public class PullRequest
{
    public const string PullRequestUrlStartingPattern = "vstfs:///Git/PullRequestId/";
    public const string Base64EncodedForwardSlash = "%2F";

    public string Id { get; }
    public string RepositoryId { get; }
    public string ProjectId { get; }

    public PullRequest(string id, string repositoryId, string projectId)
    {
        Id = id;
        RepositoryId = repositoryId;
        ProjectId = projectId;
    }

    public static PullRequest CreateFromUrl(string url)
    {
        if (!IsPullRequestUrl(url))
        {
            throw new GenerateSprintReportException(GenerateSprintReportErrorCode.InvalidPullRequestUrl);
        }

        string encodedPart = url.Substring(PullRequestUrlStartingPattern.Length);
        string[] parts = encodedPart.Split(Base64EncodedForwardSlash);
        string pullRequestId = Uri.UnescapeDataString(parts[0]);
        string repositoryId = Uri.UnescapeDataString(parts[1]);
        string projectId = Uri.UnescapeDataString(parts[2]);

        return new PullRequest(pullRequestId, repositoryId, projectId);
    }

    private static bool IsPullRequestUrl(string url)
        => url.StartsWith(PullRequestUrlStartingPattern);
}
