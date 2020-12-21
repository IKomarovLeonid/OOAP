using HashTable.Enums;

namespace HashTable.Abstract
{
    public abstract class AbstractHashTable <T>
    {
        // initialization
        // Condition after: hashtable created
        public abstract AbstractHashTable<T> Initialize(int size);

        // calculate free slot's index
        // condition: hashtable initialized 
        public abstract void HashFunc(T item);


        // checks if item are in slots
        // condition: hashtable is initialized 
        // condition: 
        public abstract OperationCode Find(T item);

        // condition: slot is calculated
        // condition: can place item
        public abstract void Put(T item);

        // condition: hashtable is initialized
        public abstract int Size();

        // condition: hashtable is initialized
        public abstract void SetToNextFreeSlot();

        public abstract OperationCode CanPlaceItem();

        public abstract OperationCode LastInsertStatus();

        public abstract OperationCode LastHashStatus();
    }
}
