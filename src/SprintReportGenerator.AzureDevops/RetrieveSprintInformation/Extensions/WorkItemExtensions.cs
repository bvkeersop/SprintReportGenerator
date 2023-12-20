using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Exceptions;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;
using System;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;

internal static class WorkItemExtensions
{
    public static int GetParentId(this Tfs.WorkItem tfsWorkItem)
        => tfsWorkItem.GetSingleParentRelation().Url.GetIdFromUrl();

    public static IEnumerable<int> GetChildIds(this Tfs.WorkItem tfsWorkItem)
        => tfsWorkItem.Relations.Where(w => w.Rel == Relation.Child.ToWiql()).Select(c => c.Url.GetIdFromUrl());

    public static WorkItemRelation GetSingleParentRelation(this Tfs.WorkItem tfsWorkItem)
    {
        var parents = tfsWorkItem.Relations.Where(w => w.Rel == Relation.Parent.ToWiql());

        if (parents.IsNullOrEmpty())
        {
            throw new GenerateSprintReportException(GenerateSprintReportErrorCode.NoParentFound);
        }

        if (parents.Count() > 1)
        {
            throw new GenerateSprintReportException(GenerateSprintReportErrorCode.MultipleParentsFound);
        }

        return parents.First();
    }

    private static int GetIdFromUrl(this string url)
    {
        var lastSlashIndex = url.LastIndexOf('/');
        return Convert.ToInt32(url[(lastSlashIndex + 1)..]);
    }

    public static TaskType GetTaskType(this Tfs.WorkItem workItem) => workItem.Fields["System.WorkItemType"] switch
    {
        "Task" => TaskType.Task,
        "Bug" => TaskType.Bug,
        _ => TaskType.Other,
    };
}
