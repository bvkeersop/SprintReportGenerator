namespace SprintReportGenerator.AzureDevops.WorkItemQueryLanguage;

internal class WiqlWhere
{
    public string Name { get; }
    public Operator Operator { get; }
    public string Value { get; }

    public WiqlWhere(string name, Operator @operator, string value)
    {
        Name = name;
        Operator = @operator;
        Value = value;
    }

    public string ToQuery() => $"[{Name}] {Operator.Translate()} {Value}";
}
