using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

namespace SprintReportGenerator.AzureDevops.CreateSprintReport.Model;

public class ListOfDevOpsItems
{
    public IEnumerable<Epic> Epics { get; set; } = Enumerable.Empty<Epic>();

    public static ListOfDevOpsItems CreateFrom(SprintInformation sprintInformation) => new()
    {
        Epics = sprintInformation.Epics.Select(e => Epic.CreateFrom(e.Value))
    };
}
