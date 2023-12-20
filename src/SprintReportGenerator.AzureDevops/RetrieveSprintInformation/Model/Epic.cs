namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model
{
    public class Epic : WorkItem
    {
        public WorkItemSet<Feature> Features { get; private set; } = new();

        public Epic(Tfs.WorkItem tfsWorkItem) : base(tfsWorkItem) { }

        public void AddFeature(Feature feature) => Features.TryAdd(feature);
    }
}
