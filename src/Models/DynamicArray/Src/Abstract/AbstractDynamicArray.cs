using DynamicArray.Enums;
using DynamicArray.Src.Enums;

namespace DynamicArray.Src.Abstract
{
    public abstract class AbstractDynamicArray <T>
    {
        public abstract AbstractDynamicArray<T> SetCapacity(int capacity);
        // add in tail -> resize
        public abstract void AddLast(T item);

        // clear 
        public abstract void Clear();

        // cursor 
        public abstract void SetCursor(int index);
        // required: cursor is set
        public abstract T GetItem();

        // required: cursor is set 
        public abstract void Remove();

        // required: not current last index
        public abstract void MoveCursorNext();

        // required: previous index greater than 0
        public abstract void MoveCursorPrevious();
        // required: cursor is set 
        public abstract void SetItem(T item);
        // required: cursor is set
        public abstract void MoveToNextSameItem(T item);

        public abstract bool IsCursorSet();

        // system
        public abstract int GetItemsCount();

        public abstract int GetCapacity();

        public abstract bool IsInitialized();

        // statuses 
        public abstract OperationStatus LastInsertStatus();

        public abstract OperationStatus LastSetCursorStatus();

        public abstract OperationStatus LastRemoveStatus();

        public abstract OperationStatus LastGetItemStatus();

        public abstract OperationStatus LastMoveCursorOperation();

        public abstract ResizeStatus ResizeStatus();

    }
}
