using System;
using System.Linq;
using HashTable.Abstract;
using HashTable.Enums;

namespace HashTable
{
    public class HashTableModel<T> : AbstractHashTable<T>
    {
        private T[] _values;

        private int? _slot;

        private int _count;

        // statuses 

        private OperationCode _lastHashStatus;

        private OperationCode _canPlaceItemToSlot;

        private OperationCode _lastPutStatus;


        public override AbstractHashTable<T> Initialize(int size)
        {
            _values ??= new T[size];
            return this;
        }

        public override void HashFunc(T item)
        {
            if (_values == null)
            {
                _lastHashStatus = OperationCode.NotInitialized;
                return;
            }
            // calculate slot 
            var itemStr = item.ToString();
            int hash = 0;
            for (int i = 0; i < itemStr.Length; i++)
            {
                hash += itemStr[i];
                hash += (hash << 10);
                hash ^= (hash << 6);
            }

            _slot = Math.Abs(hash % (_values.Length + 1));
            _lastHashStatus = OperationCode.Ok;
            _canPlaceItemToSlot = OperationCode.CanBePlaced;

            if (_values[(int) _slot] == null)
            {
                return;
            }

            // check if free
            if (!_values[(int) _slot].Equals(default(T)))
            {
                _canPlaceItemToSlot = OperationCode.CanNotBePlaced;
            }
        }

        public override OperationCode Find(T item)
        {
            if (_values == null)
            {
                return OperationCode.NotInitialized;
            }

            if (_values.Contains(item))
            {
                return OperationCode.Exists;
            }

            return OperationCode.NotFound;
        }

        public override void Put(T item)
        {
            var code = Find(item);
            if (code != OperationCode.NotFound)
            {
                _lastPutStatus = OperationCode.Error;
                return;
            }

            if (_slot == null)
            {
                _lastPutStatus = OperationCode.SlotNotCalculate;
                return;
            }

            if (CanPlaceItem() != OperationCode.CanBePlaced)
            {
                _lastPutStatus = OperationCode.CanNotBePlaced;
                return;
            }

            _values[(int) _slot] = item;
            _count++;

        }

        public override OperationCode CanPlaceItem()
        {
            return _canPlaceItemToSlot;
        }

        public override OperationCode LastInsertStatus()
        {
            return _lastPutStatus;
        }

        public override OperationCode LastHashStatus()
        {
            return _lastHashStatus;
        }

        public override int Size()
        {
            return _count;
        }

        public override void SetToNextFreeSlot()
        {
            if (_slot == null)
            {
                return;
            }
            
            if (CanPlaceItem() != OperationCode.CanBePlaced)
            {
                var current = _slot;
                for (; current < _values.Length; current++)
                {
                    if (_values[(int)current] == null)
                    {
                        _slot = current;
                        return;
                    }
                }
            }
        }
    }
}
