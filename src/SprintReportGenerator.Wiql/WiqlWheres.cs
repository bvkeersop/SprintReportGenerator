using System.Text;

namespace SprintReportGenerator.AzureDevops.WorkItemQueryLanguage;

internal class WiqlWheres
{
    private IEnumerable<WiqlWhere> _wheres;

    public WiqlWheres()
    {
        _wheres = Enumerable.Empty<WiqlWhere>();
    }

    public void AddWhere(WiqlWhere where)
    {
        _wheres = _wheres.Append(where);
    }

    public string ToQuery()
    {
        var sb = new StringBuilder();

        var startWith = "WHERE";

        foreach (var where in _wheres)
        {
            sb.Append(startWith)
                .Append(' ')
                .Append(where.ToQuery())
                .Append(' ');
        }

        return sb.ToString().Trim();
    }
}
