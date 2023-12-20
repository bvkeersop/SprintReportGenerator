namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Exceptions;

public enum GenerateSprintReportErrorCode
{
    Unknown,
    MultipleParentsFound,
    NoParentFound,
    NoFeatureCouldBeFound,
    NoEpicCouldBeFound,
    MultipleActiveSprintsFound,
    InvalidPullRequestUrl
}
