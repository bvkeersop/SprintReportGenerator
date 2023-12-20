namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;

internal static class EnumerableExtensions
{
    internal static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable is null || !enumerable.Any();
}
