using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable.Src.Abstract
{
    public abstract class AbstractHashTable <T>
    {
        // initialization
        // Condition after: hashtable created
        public abstract AbstractHashTable<T> Initialize();
    }
}
