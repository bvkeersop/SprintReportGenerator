using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation;

public interface IConstructWorkItemGraphs
{
    public Task<WorkItemGraph> ConstructWorkItemGraph(IEnumerable<UserStory> userStories);
}

public class WorkItemGraphConstructor : IConstructWorkItemGraphs
{
    private readonly IPullRequestService _pullRequestService;
    private readonly ITaskService _taskService;
    private readonly IFeatureService _featureService;
    private readonly IEpicService _epicService;

    public WorkItemGraphConstructor(
        IPullRequestService pullRequestService,
        ITaskService taskService,
        IFeatureService featureService,
        IEpicService epicService)
    {
        _pullRequestService = pullRequestService;
        _taskService = taskService;
        _featureService = featureService;
        _epicService = epicService;
    }

    public async Task<WorkItemGraph> ConstructWorkItemGraph(IEnumerable<UserStory> userStories)
    {
        var enrichedUserStories = await CreateUserStoriesWithTasksAsync(userStories);
        var features = await CreateGraphForFeaturesAsync(enrichedUserStories);
        var epics = await CreateGraphForEpicsAsync(features);
        return new WorkItemGraph(epics);
    }

    public async Task<List<UserStory>> CreateUserStoriesWithTasksAsync(IEnumerable<UserStory> userStories)
    {
        var enrichedUserStories = new List<UserStory>();

        foreach (var userStory in userStories)
        {
            var tasks = await _taskService.GetTasksAsync(userStory.GetChildIds());
            userStory.AddTasks(tasks);
            enrichedUserStories.Add(userStory);
        }

        return enrichedUserStories;
    }

    public async ValueTask<WorkItemSet<Feature>> CreateGraphForFeaturesAsync(IEnumerable<UserStory> userStories)
    {
        WorkItemSet<Feature> features = new();

        foreach (var userStory in userStories)
        {
            if (features.TryGet(userStory.GetParentId(), out var feature))
            {
                feature.AddUserStory(userStory);
                continue;
            }

            var retrievedFeature = await _featureService.GetFeatureAsync(userStory.GetParentId());
            retrievedFeature.AddUserStory(userStory);
            features.Add(retrievedFeature);
        }

        return features;
    }

    public async ValueTask<WorkItemSet<Epic>> CreateGraphForEpicsAsync(WorkItemSet<Feature> features)
    {
        WorkItemSet<Epic> epics = new();

        foreach (var feature in features)
        {
            if (epics.TryGet(feature.Value.GetParentId(), out var epic))
            {
                epic.AddFeature(feature.Value);
                continue;
            }

            var retrievedEpic = await _epicService.GetEpicAsync(feature.Value.GetParentId());
            retrievedEpic.AddFeature(feature.Value);
            epics.Add(retrievedEpic);
        }

        return epics;
    }
}
