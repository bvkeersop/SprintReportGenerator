namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Options
{
    public class SprintReportOptions
    {
        public string AzureDevOpsBaseUrl { get; set; }
        public string ProjectName { get; set; } = "TestProject";
        public string FullUrl => $"{AzureDevOpsBaseUrl}{ProjectName}";
        public string Token { get; set; }
    }
}