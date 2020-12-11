using System.Runtime.Serialization.Formatters;
using DynamicArray.Src.Abstract;
using DynamicArray.Src.Enums;

namespace DynamicArray.Src
{
    public class DynamicArray<T> : AbstractDynamicArray<T>
    {
        private T[] _array;
        private int? _cursor;

        // statuses
        private OperationStatus _insertStatus;
        private OperationStatus _removeStatus;
        private OperationStatus _getStatus;
        private OperationStatus _moveStatus;
        private OperationStatus _setCursorStatus;
        private ResizeStatus _resizeStatus;

        public override void AddLast(T item)
        {
           
        }

        public override void Clear()
        {
            if (_array == null)
            {
                _getStatus = OperationStatus.ArrayNotInitialized;
                return;
            }

            _cursor = 0;
            while (_cursor < _array.Length)
            {
                if (!GetItem().Equals(default))
                {
                    Remove();
                }
                MoveCursorNext();
            }

            _cursor = null;
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

        public override void GetItemsCount()
        {
            throw new System.NotImplementedException();
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
            return _moveStatus;
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
        }

        public override ResizeStatus ResizeStatus()
        {
            throw new System.NotImplementedException();
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
        }
    }
}
