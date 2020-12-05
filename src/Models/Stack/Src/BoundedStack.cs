using System;
using System.Collections.Generic;
using System.Linq;
using Stack.Abstract;
using Stack.Enums;

namespace Stack
{
    public class BoundedStack<T> : AbstractBoundedStack<T>
    {
        // data
        private readonly List<T> _boundedStack;
        private OpResult _popStatus = OpResult.NIL;
        private OpResult _peekStatus = OpResult.NIL;


        public BoundedStack(int capacity = 32)
        {
            if(capacity < 1) throw new ArgumentException();
            // initialize
            _boundedStack = new List<T>(capacity);
        }


        // stack operations 
        public override void Push(T item)
        {
            if (this._boundedStack.Count != this._boundedStack.Capacity)
            {
                _boundedStack.Add(item);
            }
        }

        public override void Pop()
        {
            if (this._boundedStack.Count != 0)
            {
                _boundedStack.RemoveAt(0);
                _popStatus = OpResult.OK;
            }
            else
            {
                _popStatus = OpResult.ERROR;
            }
        }

        public override T Peek()
        {
            if (this._boundedStack.Count != 0)
            {
                _peekStatus = OpResult.OK;
                return this._boundedStack.First();
            }

            _peekStatus = OpResult.ERROR;
            return default;
        }

        public override void Clear()
        {
            _boundedStack.Clear();
        }

        public override int Size()
        {
            return _boundedStack.Count;
        }

        public override OpResult GetPopStatus()
        {
            return _popStatus;
        }

        public override OpResult GetPeekStatus()
        {
            return _peekStatus;
        }
    }
}
