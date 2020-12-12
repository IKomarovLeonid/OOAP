using System.Collections.Generic;
using Queue.Src.Enums;

namespace Queue.Src.Abstract
{
    public abstract class AbstractQueue <T>
    {
        // condition after: queue initialized 
        public abstract AbstractQueue<T> Initialize();

        // condition: queue is initialized
        public abstract void Enqueue(T item);

        // condition: queue is initialized 
        //            queue is not empty
        // condition after: element has been removed. size reduces
        public abstract void Dequeue();

        // condition: queue is initialized
        //          : queue is not empty
        // condition after: get T item which is first for delete

        public abstract T PeekHeadItem();

        // condition: queue is initialized
        public abstract int Size();

        // condition: queue is initialized
        public abstract ICollection<T> GetItems();

        // statuses

        // Not initialized if queue is not created
        // Ok if enqueue was successful 

        public abstract OperationStatus LastEnqueueStatus();
        // Not initialized if queue is not created
        // Ok if enqueue was successful 
        // Error if queue size is 0

        public abstract OperationStatus LastDequeueStatus();
        // Not initialized if queue is not created
        // Ok if enqueue was successful 
        // Error if queue size is 0

        public abstract OperationStatus LastPeekStatus();

        // Not initialized if queue is not created
        public abstract OperationStatus LastGetSizeStatus();
    }
}
