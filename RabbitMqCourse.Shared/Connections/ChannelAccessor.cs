using RabbitMQ.Client;

namespace RabbitMqCourse.Shared.Connections;

internal sealed class ChannelAccessor
{
    private static readonly ThreadLocal<ChannelHolder> _holder = new();

    public IModel Channnel
    {
        get
        {
            return _holder.Value?.Context;
        }
        set
        {
            var holder = _holder.Value;

            if (holder is not null)
            {
                holder.Context = null;
            }

            if (value is not null)
            {
                _holder.Value = new ChannelHolder { Context = value };
            }
        }
    }

    private class ChannelHolder
    {
        public IModel Context;
    }
}

/*

ThreadLocal<T> - is a class that allows you to store data that is unique to each thread. 
It's particularly useful in multi-threaded programming scenarios 
where you need to maintain separate instances of data for different threads.

Here's a basic overview of how ThreadLocal<T> works:

Initialization: 
    You create a ThreadLocal<T> object by specifying the type T of the data you want to store.

Accessing Data: 
    Each thread accessing the ThreadLocal<T> instance gets its own separate instance of T. 
    This ensures that changes made by one thread do not affect the data seen by other threads.

Default Value: 
    You can optionally specify a default value for the type T. 
    This default value will be returned when a thread accesses the ThreadLocal<T> for the first time.

Thread Safety: 
    ThreadLocal<T> ensures thread safety by managing the storage of T instances internally.

Clean-up: 
    ThreadLocal<T> automatically cleans up the data associated with each thread when the thread finishes executing. 
    This helps prevent memory leaks.
 
 */