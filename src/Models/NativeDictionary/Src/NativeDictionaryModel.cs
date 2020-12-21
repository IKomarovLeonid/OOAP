using NativeDictionary.Abstract;
using NativeDictionary.Enums;
using System.Collections.Generic;

namespace NativeDictionary
{
    public class NativeDictionaryModel<T> : AbstractDictionary<T>
    {
        public override ICollection<T> GetItems()
        {
            throw new System.NotImplementedException();
        }

        public override bool HasKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public override AbstractDictionary<T> Initialize(int size)
        {
            throw new System.NotImplementedException();
        }

        public override OperationCode LastPutStatus()
        {
            throw new System.NotImplementedException();
        }

        public override OperationCode LastRemoveStatus()
        {
            throw new System.NotImplementedException();
        }

        public override void Put(string key, T item)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public override int Size()
        {
            throw new System.NotImplementedException();
        }
    }
}
