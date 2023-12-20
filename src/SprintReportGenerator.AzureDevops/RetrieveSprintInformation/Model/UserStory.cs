namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model
{
    public class UserStory : WorkItem
    {
        public List<Task> Tasks { get; private set; } = new List<Task>();

        public UserStory(Tfs.WorkItem tfsWorkItem) : base(tfsWorkItem) { }

        public void AddTasks(IEnumerable<Task> tasks) => Tasks.AddRange(tasks);
    }
}
