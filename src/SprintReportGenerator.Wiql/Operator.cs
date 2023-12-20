namespace SprintReportGenerator.AzureDevops.WorkItemQueryLanguage;

public enum Operator
{
    Equals,
}

public static class OperatorExtensions
{
    public static string Translate(this Operator @operator) => @operator switch
    {
        Operator.Equals => "=",
        _ => throw new NotImplementedException($"Operator {@operator} is not yet supported")
    };
}