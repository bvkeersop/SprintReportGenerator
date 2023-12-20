namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Extensions;

internal static class DateTimeExtensions
{
    public static DateOnly? AsDateOnly(this DateTime? dateTime)
        => dateTime.HasValue ? DateOnly.FromDateTime(dateTime.Value) : null;
}
