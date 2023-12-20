namespace SprintReportGenerator.AzureDevops.CreateSprintReport.Model;

public class Epic
{
    public string? Title { get; set; }
    public IEnumerable<Feature> Features { get; set; } = Enumerable.Empty<Feature>();

    public static Epic CreateFrom(RetrieveSprintInformation.Model.Epic epic) => new()
    {
        Title = epic.Title,
        Features = epic.Features.Select(f => Feature.CreateFrom(f.Value))
    };
}
