using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Src.Abstract
{
    abstract class AbstractBoundedStack <T>
    {
        public abstract void Push(T item);

        public abstract void Pop();

        public abstract T Peek();

        public abstract int Size();

        public abstract void Clear();

        public abstract int GetPeekStatus();

        public abstract int GetPopStatus();
    }
}
