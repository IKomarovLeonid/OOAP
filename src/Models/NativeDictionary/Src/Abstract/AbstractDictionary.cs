using System.Collections;
using System.Collections.Generic;
using NativeDictionary.Enums;

namespace NativeDictionary.Abstract
{
    public abstract class AbstractDictionary<T>
    {
        // condition after: dictionary is initialized
        public abstract AbstractDictionary<T> Initialize(int size);

        // condition: dictionary is initialized
        // task: key must be string 
        // condition after: item is placed or error
        public abstract void Put(string key, T item);
        
        // condition: dictionary is initialized
        // true if key is in dictionary
        // false if not
        public abstract bool HasKey(string key);

        // condition: key <-> value is in dictionary
        public abstract T GetItem(string key);

        // condition: key <-> value is in dictionary
        // condition after: item has new value
        public abstract void Update(string key, T item);

        // condition: key <-> value is in dictionary
        // condition after success: slot is free. Key are not exists
        public abstract void Remove(string key);

        // condition: dictionary is initialized
        public abstract int Size();

        // condition: dictionary is initialized
        public abstract ICollection<T> GetItems();


        // statuses
        public abstract OperationCode LastPutStatus();

        public abstract OperationCode LastRemoveStatus();

        public abstract OperationCode LastUpdateStatus();
    }
}
