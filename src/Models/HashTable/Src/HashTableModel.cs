using HashTable.Src.Abstract;

namespace HashTable.Src
{
    public class HashTableModel<T> : AbstractHashTable<T>
    {
        public override AbstractHashTable<T> Initialize()
        {
            return this;
        }
    }
}
