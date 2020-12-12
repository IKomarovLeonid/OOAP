using System.Collections.Generic;
using Queue.Src.Abstract;
using Queue.Src.Enums;

namespace Queue.Src
{
    public class QueueModel<T> : AbstractQueue<T>
    {
        // data
        private LinkedList<T> _list;

        // 
        private OperationStatus _enqueueStatus;
        private OperationStatus _dequeueStatus;
        private OperationStatus _sizeStatus;
        private OperationStatus _peekStatus;


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

        public override ICollection<T> GetItems()
        {
            return _list ?? new LinkedList<T>();
        }
    }
}
