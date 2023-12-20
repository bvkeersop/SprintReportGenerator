namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

public class Feature : WorkItem
{
    public IEnumerable<UserStory> UserStories { get; private set; } = Enumerable.Empty<UserStory>();

    public Feature(Tfs.WorkItem workItem) : base(workItem) { }

    public void AddUserStory(UserStory userStory)
    {
        UserStories = UserStories.Append(userStory);
    }
}
