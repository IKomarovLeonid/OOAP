using Stack.Enums;

namespace Stack.Abstract
{
    public abstract class AbstractBoundedStack <T>
    {
        public abstract void Initialize(int capacity = 32);
        public abstract void Push(T item);

        public abstract void Pop();

        public abstract T Peek();

        public abstract int Size();

        public abstract void Clear();

        public abstract OpResult GetPeekStatus();

        public abstract OpResult GetPopStatus();

        public abstract OpResult GetPushStatus();
    }
}
