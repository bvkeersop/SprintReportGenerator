using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SprintReportGenerator.AzureDevops.RetrieveSprintInformation.Model;

public class WorkItemSet<TWorkItem> : IEnumerable<KeyValuePair<int, TWorkItem>> where TWorkItem : WorkItem
{
    private readonly Dictionary<int, TWorkItem> _value = new();

    public bool Contains(int id)
    {
        return _value.ContainsKey(id);
    }

    public void Add(TWorkItem workItem)
    {
        if (workItem == null)
        {
            return;
        }

        _value.Add(workItem.Id, workItem);
    }

    public bool TryAdd(TWorkItem workItem)
    {
        if (workItem == null)
        {
            return false;
        }

        return _value.TryAdd(workItem.Id, workItem);
    }

    public void Add(IEnumerable<TWorkItem> workItems)
    {
        foreach (var workItem in workItems)
        {
            Add(workItem);
        }
    }


    public bool TryGet(int id, [NotNullWhen(true)] out TWorkItem? workItem) => _value.TryGetValue(id, out workItem);

    public WorkItem Get(int id) => _value[id];

    public IEnumerator<KeyValuePair<int, TWorkItem>> GetEnumerator()
    {
        foreach (var item in _value)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
