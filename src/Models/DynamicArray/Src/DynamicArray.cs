using System;
using System.Collections.Generic;
using DynamicArray.Abstract;
using DynamicArray.Enums;
using DynamicArray.Src.Enums;

namespace DynamicArray
{
    public class DynamicArray<T> : AbstractDynamicArray<T>
    {
        private T[] _array;
        private int? _cursor;
        // data
        private int _count;

        // statuses
        private OperationStatus _insertStatus;
        private OperationStatus _removeStatus;
        private OperationStatus _getStatus;
        private OperationStatus _moveStatus;
        private OperationStatus _setCursorStatus;
        private ResizeStatus _resizeStatus;

        public override void AddLast(T item)
        {
           // checks array initialization 
           if (!IsInitialized())
           {
               _insertStatus = OperationStatus.ArrayNotInitialized;
               return;
           }
           // checks ability to insert 
           var count = GetItemsCount();
           var capacity = _array.Length;
           // remember current cursor
           var beforeStart = _cursor;

           if (count < capacity)
           {
               SetCursor(0);
                _moveStatus = OperationStatus.Ok;
                while (_moveStatus == OperationStatus.Ok)
                {
                    if (GetItem().Equals(default(T)))
                    {
                        SetItem(item);
                        _cursor = beforeStart;
                        return;
                    }
                    MoveCursorNext();
                }
           }
           else
           {
               // set cursor to last current index
               SetCursor(capacity - 1);
               // resize 
               var newCapacity = capacity * 2;
               ResizeArray(newCapacity);
               // move next
               MoveCursorNext();
               SetItem(item);
               // restore cursor 
               _cursor = beforeStart;
               return;
           }
        }

        // system
        private void ResizeArray(int newCapacity)
        {
            if (newCapacity > GetCapacity())
            {
                _resizeStatus = Enums.ResizeStatus.Increase;
                var newArray = new T[newCapacity];
                Array.Copy(_array, newArray, _array.Length);
                _array = newArray;
            }
            else
            {
                _resizeStatus = Enums.ResizeStatus.Decrease;
                _array = new T[newCapacity];
            }

           
        }

        public override void Clear()
        {
            if (_array == null)
            {
                _getStatus = OperationStatus.ArrayNotInitialized;
                return;
            }
            Array.Clear(_array, 0, _array.Length);
            _cursor = null;
            _count = 0;
        }

        public override int GetCapacity()
        {
            if (!IsInitialized())
            {
                _getStatus = OperationStatus.ArrayNotInitialized;
                return default;
            }

            return _array.Length;

        }

        public override int GetItemsCount()
        {
            _getStatus = OperationStatus.Ok;
            return _count;
        }

        // methods with cursor
        public override T GetItem()
        {
            if (!IsInitialized())
            {
                _getStatus = OperationStatus.ArrayNotInitialized;
                return default;
            }

            if (!IsCursorSet())
            {
                _getStatus = OperationStatus.CursorNotSet;
                return default;
            }

            return _array[(int)_cursor];
        }

        public override bool IsCursorSet()
        {
            return _cursor != null;
        }

        public override bool IsInitialized()
        {
            return _array != null;
        }

        public override OperationStatus LastGetItemStatus()
        {
            return _getStatus;
        }

        public override OperationStatus LastInsertStatus()
        {
            return _insertStatus;
        }

        public override OperationStatus LastMoveCursorOperation()
        {
            return _moveStatus;
        }

        public override OperationStatus LastRemoveStatus()
        {
            return _removeStatus;
        }

        public override OperationStatus LastSetCursorStatus()
        {
            return _setCursorStatus;
        }

        public override void MoveCursorNext()
        {
            if (!IsInitialized())
            {
                _moveStatus = OperationStatus.ArrayNotInitialized;
                return;
            }

            if (!IsCursorSet())
            {
                _moveStatus = OperationStatus.CursorNotSet;
                return;
            }

            if (_cursor == _array.Length - 1)
            {
                _moveStatus = OperationStatus.Error;
                return;
            }

            _cursor = _cursor + 1;
            _moveStatus = OperationStatus.Ok;
        }

        public override void MoveCursorPrevious()
        {
            if (!IsCursorSet())
            {
                _moveStatus = OperationStatus.CursorNotSet;
                return;
            }

            if (_cursor == 0)
            {
                _moveStatus = OperationStatus.Error;
                return;
            }

            _cursor = _cursor - 1;
            _moveStatus = OperationStatus.Ok;
        }

        public override void MoveToNextSameItem(T item)
        {
            if (!IsCursorSet())
            {
                _moveStatus = OperationStatus.CursorNotSet;
                return;
            }

            var status = OperationStatus.Ok;

            while (status != OperationStatus.Error)
            {
                if (GetItem().Equals(item))
                {
                    _moveStatus = OperationStatus.Ok;
                    return;
                }
                MoveCursorNext();
                status = LastMoveCursorOperation();
            }

            _moveStatus = OperationStatus.NotFound;
        }

        public override void Remove()
        {
            if (!IsInitialized())
            {
                _removeStatus = OperationStatus.ArrayNotInitialized;
                return;
            }

            if (!IsCursorSet())
            {
                _removeStatus = OperationStatus.CursorNotSet;
                return;
            }

            _array[(int)_cursor] = default;
            _count--;
            _removeStatus = OperationStatus.Ok;

            if (_count < GetCapacity() / 2)
            {
                _cursor = 0;
                var items = GetItemsFromArray();
                // resize
                var newCapacity = 2 * GetCapacity() / 3;
                ResizeArray(newCapacity);
                // place items 
                SetItemsToArray(items);
            }

            return;
        }

        private IEnumerable<T> GetItemsFromArray()
        {
            var list = new List<T>();
            if (IsInitialized())
            {
                for (int index = 0; index < _array.Length; index++)
                {
                    if (!_array[index].Equals(default(T)))
                    {
                        list.Add(_array[index]);
                    }
                }
            }

            return list;
        }

        private void SetItemsToArray(IEnumerable<T> items)
        {
            int index = 0;
            foreach (var item in items)
            {
                _array[index] = item;
                index++;
            }
        }


        public override ResizeStatus ResizeStatus()
        {
            return _resizeStatus;
        }

        public override AbstractDynamicArray<T> SetCapacity(int capacity = 16)
        {
            if (!IsInitialized() && capacity > -1)
            {
                _array = new T[capacity];
            }
            
            return this;
        }

        public override void SetCursor(int index)
        {
            if (!IsInitialized())
            {
                _setCursorStatus = OperationStatus.ArrayNotInitialized;
                return;
            }

            if (index < 0 || index > _array.Length - 1)
            {
                _setCursorStatus = OperationStatus.Error;
                return;
            }
            _cursor = index;
            _setCursorStatus = OperationStatus.Ok;
        }

        public override void SetItem(T item)
        {
            if (!IsCursorSet())
            {
                _insertStatus = OperationStatus.CursorNotSet;
                return;
            }

            _insertStatus = OperationStatus.Ok;
            _array[(int)_cursor] = item;
            _count++;
        }
    }
}
