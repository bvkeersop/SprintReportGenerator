namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;

internal static class RelationExtensions
{
    internal static string ToWiql(this Model.Relation relation) => relation switch
    {
        Model.Relation.Parent => "System.LinkTypes.Hierarchy-Reverse",
        Model.Relation.Child => "System.LinkTypes.Hierarchy-Forward",
        Model.Relation.Artifact => "ArtifactLink",
        _ => throw new NotImplementedException($"{relation} is not implemented")
    };
}
