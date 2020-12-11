namespace ListModel.Abstract
{
    public abstract class AbstractModelLinkedList <T> : AbstractModelList<T>
    {
        // Condition: previous node exists
        public abstract void MoveCursorPrevious();
    }
}
