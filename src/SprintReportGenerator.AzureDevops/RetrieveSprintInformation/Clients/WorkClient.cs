using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Clients;

internal interface IWorkClient
{
    Task<List<TeamSettingsIteration>> GetSprintsByProjectName(string projectName);
}

internal class WorkClient : IWorkClient
{
    private readonly WorkHttpClient _client;

    public WorkClient(WorkHttpClient client)
    {
        _client = client;
    }

    public async Task<List<TeamSettingsIteration>> GetSprintsByProjectName(string projectName)
    {
        var teamContext = new TeamContext(projectName);
        return await _client.GetTeamIterationsAsync(teamContext, "current", null, default);
    }
}
