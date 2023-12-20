using Microsoft.Extensions.Logging;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Services;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation;

public interface IRetrieveSprintInformation
{
    Task<SprintInformation> RetrieveSprintInformation();
}

internal class SprintInformationRetriever : IRetrieveSprintInformation
{
    private readonly ILogger<SprintInformationRetriever> _logger;
    private readonly IUserStoryService _userStoryService;
    private readonly IConstructWorkItemGraphs _workItemGraphBuilder;

    public SprintInformationRetriever(
        ILogger<SprintInformationRetriever> logger,
        IUserStoryService userStoryService,
        IConstructWorkItemGraphs workItemGraphBuilder)
    {
        _logger = logger;
        _userStoryService = userStoryService;
        _workItemGraphBuilder = workItemGraphBuilder;
    }

    public async Task<SprintInformation> RetrieveSprintInformation()
    {
        var userStories = await _userStoryService.GetUserStories();
        var workItemGraph = await _workItemGraphBuilder.ConstructWorkItemGraph(userStories);
        var sprintInformation = new Model.SprintInformation(workItemGraph.TopLevelWorkItems);

        foreach (var epic in sprintInformation.Epics)
        {
            _logger.LogInformation(epic.Value.Title);

            foreach (var feature in epic.Value.Features)
            {
                _logger.LogInformation(feature.Value.Title);

                foreach (var us in feature.Value.UserStories)
                {
                    _logger.LogInformation(us.Title);

                    foreach (var task in us.Tasks)
                    {
                        _logger.LogInformation(task.Title);
                        foreach (var pr in task.PullRequests) ;
                    }
                }
            }
        }
        Console.WriteLine("hello");
        return sprintInformation;
    }
}
