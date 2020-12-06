﻿namespace LinkedList.Src.Abstract
{
    abstract class AbstractLinkedList <T>
    {
        // Condition: list is not empty
        public abstract void CursorToHead();
        // Condition: list is not empty
        public abstract void CursorToTail();
        // Condition: next node exists
        public abstract void MoveCursorNext();
        // Condition: list is not empty
        public abstract T GetItem();

        public abstract void PutNext(T item);

        public abstract void PutLeft(T item);

        // Condition: cursor is set
        // Post condition: cursor becomes next item if exists
        // otherwise: cursor becomes previous node if exists
        public abstract void RemoveItem();

        public abstract void Clear();

        public abstract void Size();

        // useful operations

        public abstract void AddInTail(T item);

        // condition: cursor is set
        public abstract void ReplaceItem(T item);

        // condition: cursor is set
        public abstract void SetToNextSameItem(T item);

        // condition: cursor is set
        public abstract void RemoveAllSameItems(T item);

        public abstract bool IsCursorInHead();

        public abstract bool IsCursorInTail();

        public abstract bool IsCursorSet();
    }
}
