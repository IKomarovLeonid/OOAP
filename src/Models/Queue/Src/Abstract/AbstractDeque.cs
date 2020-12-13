using Queues.Enums;

namespace Queues.Abstract
{
    public abstract class AbstractDeque <T> : AbstractQueue<T>
    {
        // extensions for deque

        // condition: deque is not empty and initialized
        public abstract T PeekLastItem();

        // condition: deque is initialized
        public abstract void EnqueueFirst(T item);

        // condition: deque is not empty and initialized
        public abstract void DequeueLast();

        // statuses
        public abstract OperationStatus LastEnqueueFirstStatus();

        public abstract OperationStatus DequeueLastStatus();

    }
}
