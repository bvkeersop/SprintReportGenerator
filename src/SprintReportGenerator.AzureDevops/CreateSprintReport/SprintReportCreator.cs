using MarkdownDocumentBuilder.Model.Document;
using SprintReportGenerator.AzureDevops.CreateSprintReport.Model;
using SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;
using System.Text;

namespace SprintReportGenerator.AzureDevops.CreateSprintReport;

public interface ICreateSprintReports
{
    void CreateSprintReport(SprintInformation sprintInformation);
}

internal class SprintReportCreator : ICreateSprintReports
{
    public void CreateSprintReport(SprintInformation sprintInformation)
    {
        using var memoryStream = new MemoryStream();
        var listOfDevOpsItems = ListOfDevOpsItems.CreateFrom(sprintInformation);

        MarkdownDocument.Build(document =>
        {
            document.Content(content =>
            {
                content.AddHeader1("SprintReport");
                content.AddOrderedList(listOfDevOpsItems.Epics);
            });

        }).SaveAsync(memoryStream);

        var result = ReadAsString(memoryStream);
    }

    public static string ReadAsString(Stream stream)
    {
        stream.Position = 0;
        using StreamReader reader = new(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
}
