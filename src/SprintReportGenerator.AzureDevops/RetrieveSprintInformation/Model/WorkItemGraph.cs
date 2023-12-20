namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model
{
    public class WorkItemGraph
    {
        public WorkItemSet<Epic> TopLevelWorkItems { get; } = new();

        public WorkItemGraph(WorkItemSet<Epic> topLevelWorkItems)
        {
            TopLevelWorkItems = topLevelWorkItems;
        }
    }
}
