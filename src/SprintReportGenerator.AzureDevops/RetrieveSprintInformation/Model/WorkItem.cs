using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

public class WorkItem
{
    public int Id { get; }
    public string? Title { get; }
    public string Description { get; }
    public Tfs.WorkItem TfsWorkItem { get; }

    public WorkItem(Tfs.WorkItem tfsWorkItem)
    {
        Id = tfsWorkItem.Id.Value;
        TfsWorkItem = tfsWorkItem;
        Title = tfsWorkItem.Fields["System.Title"] as string;
    }

    public int GetParentId() => TfsWorkItem.GetParentId();
    public IEnumerable<int> GetChildIds() => TfsWorkItem.GetChildIds();
}