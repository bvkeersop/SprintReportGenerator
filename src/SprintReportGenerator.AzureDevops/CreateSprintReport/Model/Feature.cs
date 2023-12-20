namespace SprintReportGenerator.AzureDevops.CreateSprintReport.Model;

public class Feature
{
    public string? Title { get; set; }
    public IEnumerable<UserStory> Userstories { get; set; } = Enumerable.Empty<UserStory>();

    public static Feature CreateFrom(RetrieveSprintInformation.Model.Feature feature) => new()
    {
        Title = feature.Title,
        Userstories = feature.UserStories.Select(u => UserStory.CreateFrom(u))
    };
}
