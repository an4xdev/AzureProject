namespace Project.Client.Services;

public class EventAggregator
{
    private readonly Dictionary<string, List<Func<Task>>> _eventSubscriptions = new();

    public void Subscribe(string eventName, Func<Task> callback)
    {
        if (!_eventSubscriptions.TryGetValue(eventName, out var value))
        {
            value = [];
            _eventSubscriptions[eventName] = value;
        }

        value.Add(callback);
    }

    public void Publish(string eventName)
    {
        if (!_eventSubscriptions.TryGetValue(eventName, out var value)) return;

        var callbacks = value.ToList();
        foreach (var callback in callbacks)
        {
            callback.Invoke();
        }
    }
}