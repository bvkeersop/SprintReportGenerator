using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace SprintReportGenerator.AzureDevops.WorkItemQueryLanguage;

public class WiqlQueryBuilder
{
    private WiqlSelect? _select;
    private readonly WiqlWheres _where = new();
    private WiqlFrom? _from;

    public WiqlQueryBuilder Select(params string[] fields)
    {
        if (_select != null)
        {
            throw new InvalidOperationException($"Can only have one select per wiql query");
        }

        if (fields.Contains("*"))
        {
            _select = WiqlSelect.All();
            return this;
        }

        WiqlSelect.Values(fields);
        return this;
    }

    public WiqlQueryBuilder From(string name)
    {
        if (_from != null)
        {
            throw new InvalidOperationException($"Can only have one from per wiql query");
        }

        _from = new WiqlFrom(name);
        return this;
    }

    public WiqlQueryBuilder Where(string name, Operator @operator, string value)
    {
        var wiqlWhere = new WiqlWhere(name, @operator, value);
        _where.AddWhere(wiqlWhere);
        return this;
    }

    public Wiql Build()
    {
        if (_select == null)
        {
            throw new InvalidOperationException($"Wiql query needs exactly one select");
        }

        if (_from == null)
        {
            throw new InvalidOperationException($"Wiql query needs exactly one from");
        }

        return new Wiql
        {
            Query = $"{_select.ToQuery()} {_from.ToQuery()} {_where.ToQuery()}"
        };
    }
}
