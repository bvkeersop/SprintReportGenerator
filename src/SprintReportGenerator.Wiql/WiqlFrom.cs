namespace SprintReportGenerator.AzureDevops.WorkItemQueryLanguage;

internal class WiqlFrom
{
    private readonly string _name;

    public WiqlFrom(string name)
    {
        _name = name;
    }

    public string ToQuery() => $"FROM {_name}";
}
