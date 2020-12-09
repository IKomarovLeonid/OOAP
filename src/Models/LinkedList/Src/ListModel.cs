using System;
using System.Collections.Generic;
using ListModel.Abstract;
using ListModel.Statuses;

namespace ListModel
{
    public class ListModel<T> : AbstractModelList<T>
    {
        // data
        private readonly LinkedList<T> _list;
        private LinkedListNode<T> _cursor;

        // status
        private OperationStatus _operationStatus;

        public ListModel()
        {
            _list = new LinkedList<T>();
            _operationStatus = OperationStatus.NoAnyOperation;
        }

        public override void AddInTail(T item)
        {
            _list.AddLast(item);
            _operationStatus = OperationStatus.Ok;
        }

        public override void Clear()
        {
            _list.Clear();
            _operationStatus = OperationStatus.Ok;
        }

        public override IReadOnlyCollection<T> GetItems()
        {
            return _list;
        }

        public override void CursorToHead()
        {
            if (Size() != 0)
            {
                _cursor = _list.First;
                _operationStatus = OperationStatus.Ok;
            }
            else _operationStatus = OperationStatus.CursorNotSet;
        }

        public override void CursorToTail()
        {
            if (Size() != 0)
            {
                _cursor = _list.Last;
                _operationStatus = OperationStatus.Ok;
            }
            else _operationStatus = OperationStatus.CursorNotSet;
        }

        public override T GetItem()
        {
            if (IsCursorSet())
            {
                _operationStatus = OperationStatus.Ok;
                return _cursor.Value;
            }

            _operationStatus = OperationStatus.CursorNotSet;
            return default;
        }

        public override bool IsCursorInHead()
        {
            if (IsCursorSet())
            {
                _operationStatus = OperationStatus.Ok;
                return _cursor == _list.First;
            }
            _operationStatus = OperationStatus.CursorNotSet;
            return false;
        }

        public override bool IsCursorInTail()
        {
            if (IsCursorSet())
            {
                _operationStatus = OperationStatus.Ok;
                return _cursor == _list.Last;
            }
            _operationStatus = OperationStatus.CursorNotSet;
            return false;
        }

        public override bool IsCursorSet()
        {
            return _cursor != null;
        }

        public override void MoveCursorNext()
        {
            if (IsCursorSet())
            {
                if (_cursor.Next != null) _cursor = _cursor.Next;
                _operationStatus = OperationStatus.Ok;
                return;
            }
            _operationStatus = OperationStatus.CursorNotSet;
        }

        public override void MoveCursorPrevious()
        {
            if (IsCursorSet())
            {
                if (_cursor.Previous != null) _cursor = _cursor.Previous;
                _operationStatus = OperationStatus.Ok;
                return;
            }
            _operationStatus = OperationStatus.CursorNotSet;
        }


        public override void PutBefore(T item)
        {
            if (IsCursorSet())
            {
                if (IsCursorInHead())
                {
                    _list.AddFirst(item);
                    _operationStatus = OperationStatus.Ok;
                    return;
                }

                _list.AddBefore(_cursor, new LinkedListNode<T>(item));
                _operationStatus = OperationStatus.Ok;
            }
        }

        public override void PutNext(T item)
        {
            if (IsCursorSet())
            {
                if (IsCursorInTail())
                {
                    _list.AddLast(new LinkedListNode<T>(item));
                    _operationStatus = OperationStatus.Ok;
                    return;
                }

                _list.AddAfter(_cursor, new LinkedListNode<T>(item));
                _operationStatus = OperationStatus.Ok;
            }
        }

        public override void RemoveAllSameItems(T item)
        {
            if (!IsCursorSet())
            {
                _operationStatus = OperationStatus.CursorNotSet;
                return;
            }

            while (IsCursorSet())
            {
                if(!GetItem().Equals(item) && IsCursorInTail()) break;
                if (GetItem().Equals(item)) RemoveItem();
                SetToNextSameItem(item);
            }
            _operationStatus = OperationStatus.Ok;
        }

        public override void RemoveItem()
        {
            if (IsCursorSet())
            {
                if (_cursor.Next != null)
                {
                    MoveCursorNext();
                    _list.Remove(_cursor.Previous);
                    _operationStatus = OperationStatus.Ok;
                    return;
                }

                if (_cursor.Previous != null)
                {
                    MoveCursorPrevious();
                    _list.Remove(_cursor.Next);
                    _operationStatus = OperationStatus.Ok;
                    return;
                }

                _list.Remove(_cursor);
                _cursor = null;
                _operationStatus = OperationStatus.Ok;
                return;
            }
            _operationStatus = OperationStatus.CursorNotSet;
        }

        public override void ReplaceItem(T item)
        {
            if (IsCursorSet())
            {
                _cursor.Value = item;
                _operationStatus = OperationStatus.Ok;
            }
            else
            {
                _operationStatus = OperationStatus.CursorNotSet;
            }
        }

        public override void SetToNextSameItem(T item)
        {
            if (IsCursorSet())
            {
                while (_cursor != null)
                {
                    if (GetItem().Equals(item)) return;
                    // if in tail - break
                    if (IsCursorInTail()) return;
                    MoveCursorNext();
                }

                _operationStatus = OperationStatus.Ok;
            }
            else _operationStatus = OperationStatus.CursorNotSet;

        }

        public override int Size()
        {
            _operationStatus = OperationStatus.Ok;
            return _list.Count;
        }

        public override OperationStatus LastOperationStatus()
        {
            return _operationStatus;
        }
    }
}
