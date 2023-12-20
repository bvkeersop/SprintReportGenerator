using System.Text;

namespace SprintReportGenerator.AzureDevops.WorkItemQueryLanguage;

internal class WiqlSelect
{
    private const string _allSelector = "*";

    private readonly bool _selectAll;
    private readonly IEnumerable<string> _values = Enumerable.Empty<string>();

    private WiqlSelect(bool selectAll, IEnumerable<string> values)
    {
        _selectAll = selectAll;
        _values = values;
    }

    public static WiqlSelect All()
    {
        return new WiqlSelect(selectAll: true, Enumerable.Empty<string>());
    }

    public static WiqlSelect Values(params string[] values)
    {
        var valueEnumerable = Enumerable.Empty<string>();

        foreach (var value in values)
        {
            valueEnumerable = valueEnumerable.Append(value);
        }

        return new WiqlSelect(selectAll: false, values: valueEnumerable);
    }

    public string ToQuery()
    {
        if (_selectAll)
        {
            return $"SELECT {_allSelector}";
        }

        var sb = new StringBuilder();

        foreach (var value in _values)
        {
            sb.Append("SELECT")
                .Append(' ')
                .Append('[')
                .Append(value)
                .Append(']')
                .Append(',')
                .Append(' ');
        }

        return sb.ToString().Trim();
    }
}
