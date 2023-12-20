namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

public class SprintInformation
{
    public WorkItemSet<Epic> Epics { get; }

    public SprintInformation(WorkItemSet<Epic> epics)
    {
        Epics = epics;
    }
}
