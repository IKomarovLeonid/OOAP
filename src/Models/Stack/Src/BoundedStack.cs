using System;
using System.Collections.Generic;
using Stack.Src.Abstract;

namespace Stack.Src
{
    class BoundedStack <T> : AbstractBoundedStack<T>
    {
        // data
        private List<T> _boundedStack;
        private int _peek_status;
        private int _pop_status;

        // stack operations 

        public override void Push(T item)
        {
            throw new NotImplementedException();
        }

        public override void Pop()
        {
            throw new NotImplementedException();
        }

        public override T Peek()
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override int Size()
        {
            throw new NotImplementedException();
        }

        public override int GetPopStatus()
        {
            throw new NotImplementedException();
        }

        public override int GetPeekStatus()
        {
            throw new NotImplementedException();
        }
    }
}
