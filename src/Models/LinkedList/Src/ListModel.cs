using System.Collections.Generic;
using ListModel.Abstract;

namespace ListModel
{
    public class ListModel<T> : AbstractModelList<T>
    {
        // data
        private readonly LinkedList<T> _list;
        private LinkedListNode<T> _cursor;

        public ListModel()
        {
            _list = new LinkedList<T>();
        }

        public override void AddInTail(T item)
        {
            _list.AddLast(item);
        }

        public override void Clear()
        {
            _list.Clear();
        }

        public override void CursorToHead()
        {
            if(Size() != 0) _cursor = _list.First;
        }

        public override void CursorToTail()
        {
            if (Size() != 0) _cursor = _list.Last;
        }

        public override T GetItem()
        {
            if (IsCursorSet())
            {
                return _cursor.Value;
            }

            return default;
        }

        public override bool IsCursorInHead()
        {
            if(IsCursorSet()) return _cursor == _list.First;
            return false;
        }

        public override bool IsCursorInTail()
        {
            if (IsCursorSet()) return _cursor == _list.Last;
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
            }
        }

        public override void PutLeft(T item)
        {
            throw new System.NotImplementedException();
        }

        public override void PutNext(T item)
        {
            MoveCursorNext();
            if (IsCursorSet())
            {
                var current = GetItem();
                ReplaceItem(item);

                while (IsCursorSet())
                {
                    MoveCursorNext();
                    var loopValue = GetItem();
                    ReplaceItem(current);
                    current = loopValue;
                }

                CursorToTail();
                AddInTail(current);
            }
        }

        public override void RemoveAllSameItems(T item)
        {
            if (IsCursorSet())
            {
                while (IsCursorSet())
                {
                    if(GetItem().Equals(item)) RemoveItem();
                    MoveCursorNext();
                }
            }
        }

        public override void RemoveItem()
        {
            throw new System.NotImplementedException();
        }

        public override void ReplaceItem(T item)
        {
            if (IsCursorSet()) _cursor.Value = item;
        }

        public override void SetToNextSameItem(T item)
        {
            while (_cursor != null)
            {
                if (GetItem().Equals(item)) return;
                MoveCursorNext();
            }
        }

        public override int Size()
        {
            return _list.Count;
        }
    }
}
