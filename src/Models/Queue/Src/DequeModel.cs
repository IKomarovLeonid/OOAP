using System.Collections.Generic;
using Queues.Abstract;
using Queues.Enums;

namespace Queues
{
    public class DequeModel<T> : AbstractDeque<T>
    {
        // data
        private LinkedList<T> _list;

        // statuses 
        private OperationStatus _enqueueStatus;
        private OperationStatus _dequeueStatus;
        private OperationStatus _sizeStatus;
        private OperationStatus _peekStatus;
        private OperationStatus _enqueueFirstOperation;
        private OperationStatus _dequeLastOperation;


        // initialization
        public override AbstractQueue<T> Initialize()
        {
            _list ??= new LinkedList<T>();
            return this;
        }


        public override void Dequeue()
        {
            if (_list == null)
            {
                _dequeueStatus = OperationStatus.NotInitialized;
                return;
            }

            if (Size() == 0)
            {
                _dequeueStatus = OperationStatus.Error;
                return;
            }

            _dequeueStatus = OperationStatus.Ok;
            _list.RemoveFirst();
        }

        public override void Enqueue(T item)
        {
            if (_list == null)
            {
                _enqueueStatus = OperationStatus.NotInitialized;
                return;
            }
            // place new item
            _list.AddLast(item);
            _enqueueStatus = OperationStatus.Ok;
        }

        public override T PeekHeadItem()
        {
            if (_list == null)
            {
                _peekStatus = OperationStatus.NotInitialized;
                return default;
            }

            if (Size() == 0)
            {
                _peekStatus = OperationStatus.Error;
                return default;
            }

            _peekStatus = OperationStatus.Ok;
            return _list.First.Value;
        }

        public override int Size()
        {
            if (_list == null)
            {
                _sizeStatus = OperationStatus.NotInitialized;
                return -1;
            }

            return _list.Count;
        }

        // statuses 
        public override OperationStatus LastDequeueStatus()
        {
            return _dequeueStatus;
        }

        public override OperationStatus LastEnqueueStatus()
        {
            return _enqueueStatus;
        }

        public override OperationStatus LastPeekStatus()
        {
            return _peekStatus;
        }

        public override OperationStatus LastGetSizeStatus()
        {
            return _sizeStatus;
        }

        public override OperationStatus LastEnqueueFirstStatus()
        {
            return _enqueueFirstOperation;
        }

        public override OperationStatus DequeueLastStatus()
        {
            return _dequeLastOperation;
        }

        public override ICollection<T> GetItems()
        {
            return _list ?? new LinkedList<T>();
        }

        public override T PeekLastItem()
        {
            if (_list == null)
            {
                _peekStatus = OperationStatus.NotInitialized;
                return default;
            }

            if (Size() == 0)
            {
                _peekStatus = OperationStatus.Error;
                return default;
            }

            _peekStatus = OperationStatus.Ok;
            return _list.Last.Value;
        }

        public override void EnqueueFirst(T item)
        {
            if (_list == null)
            {
                _enqueueFirstOperation = OperationStatus.NotInitialized;
                return;
            }

            // place new item
            _list.AddFirst(item);
            _enqueueFirstOperation = OperationStatus.Ok;
        }

        public override void DequeueLast()
        {
            if (_list == null)
            {
                _dequeLastOperation = OperationStatus.NotInitialized;
                return;
            }

            if (Size() == 0)
            {
                _dequeLastOperation = OperationStatus.Error;
                return;
            }

            _dequeLastOperation = OperationStatus.Ok;
            _list.RemoveLast();
        }

        
    }
}
