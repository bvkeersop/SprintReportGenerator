namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;

internal static class WorkItemTypeExtensions
{
    internal static string ToWiql(this Model.WorkItemType workitemType) => workitemType switch
    {
        Model.WorkItemType.Task => "'Task'",
        Model.WorkItemType.UserStory => "'User Story'",
        Model.WorkItemType.Feature => "'Feature'",
        Model.WorkItemType.Epic => "'Epic'",
        _ => throw new NotImplementedException()
    };
}
