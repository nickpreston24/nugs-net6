using CodeMechanic.Async;

namespace nugsnet6.Experimental;

public interface IHighSpeedRepository
{
    public async Task<SerialQueue> QueueAsync<T, R>(params Func<T, Task<R>>[] actions)
    {
        var Q = new SerialQueue();
        var tasks = actions.Select(action => Q.Enqueue(async () => action));
        await Task.WhenAll(tasks);

        return Q;
    }

    // NestAsync<Func<R,T>, Func<R,T>>(f1, f2...);
}
