using System;
using NativeDictionary.Abstract;
using NativeDictionary.Enums;
using System.Collections.Generic;

namespace NativeDictionary
{
    public class NativeDictionaryModel<T> : AbstractDictionary<T>
    {
        public override ICollection<T> GetItems()
        {
            if(_items == null) return new List<T>();
            var list = new List<T>();

            foreach (var item in _items)
            {
                if (item != null)
                {
                    if(!item.Equals(default(T))) list.Add(item);
                }
            }

            return list;
        }

        public override bool HasKey(string key)
        {
            if (_keys == null) return false;

            if (this.SearchKey(key) == -1) return false;

            return true;
        }

        public override T GetItem(string key)
        {
            var index = SearchKey(key);
            if (index > -1)
            {
                return _items[index];
            }

            return default;
        }

        public override void Update(string key, T item)
        {
            var index = SearchKey(key);
            if (index > -1)
            {
                _items[index] = item;
                _lastUpdateStatus = OperationCode.Ok;
                return;
            }

            _lastUpdateStatus = OperationCode.KeyNotFound;

        }

        public override AbstractDictionary<T> Initialize(int size)
        {
            _keys ??= new string[size];
            _items ??= new T[size];
            return this;
        }

        public override OperationCode LastPutStatus()
        {
            return _lastPutStatus;
        }

        public override OperationCode LastRemoveStatus()
        {
            return _lastRemoveStatus;
        }

        public override void Put(string key, T item)
        {
            if (_keys == null || _items == null)
            {
                _lastPutStatus = OperationCode.NotInitialized;
                return;
            }

            if (HasKey(key))
            {
                _lastPutStatus = OperationCode.KeyAlreadyExists;
                return;
            }

            if (_freeSlot == _keys.Length || _freeSlot == _items.Length)
            {
                // if free slot equals array len -> resize
            }

            _keys[_freeSlot] = key;
            _items[_freeSlot] = item;

            ToNextFreeSlot();
            _count++;

            _lastPutStatus = OperationCode.Ok;
        }

        public override void Remove(string key)
        {
            if (_keys == null || _items == null)
            {
                _lastPutStatus = OperationCode.NotInitialized;
                return;
            }

            var index = this.SearchKey(key);

            if (index > -1)
            {
                _keys[index] = default;
                _items[index] = default;
                _count--;
                _freeSlot = index;
                _lastRemoveStatus = OperationCode.Ok;
                // if count of values less than 50% of capacity => resize 
                var count = this.Size();
                if (count < _items.Length)
                {
                    Resize(_items.Length * 2 /3);
                }

            }
            else
            {
                _lastRemoveStatus = OperationCode.KeyNotFound;
            }

        }

        public override int Size()
        {
            return _count;
        }

        public override OperationCode LastUpdateStatus()
        {
            return _lastUpdateStatus;
        }

        // private 

        private int SearchKey(string key)
        {
            for (int index = 0; index < _keys.Length; index++)
            {
                if (_keys[index] == key)
                {
                    return index;
                }
            }

            return -1;
        }

        private void ToNextFreeSlot()
        {
            while (_freeSlot < _items.Length)
            {
                if (_items[_freeSlot] == null)
                {
                    return;
                }
                else
                {
                    if(_items[_freeSlot].Equals(default(T))) return;
                }

                _freeSlot++;
            }

            // free slot == items.Length => must be resize
            this.Resize(2 * _items.Length);
        }

        private void Resize(int new_capacity)
        {
            var itemsNew = new T[new_capacity];
            var keysNew = new string[new_capacity];

            if (new_capacity > _items.Length)
            {
                Array.Copy(_items, itemsNew, _items.Length);
                Array.Copy(_keys, keysNew, _keys.Length);
            }
            else
            {
                Array.Copy(_items, itemsNew, itemsNew.Length);
                Array.Copy(_keys, keysNew, keysNew.Length);
            }

            _items = itemsNew;
            _keys = keysNew;

        }

        private string[] _keys;
        private T[] _items;
        private int _freeSlot;

        private OperationCode _lastPutStatus;
        private OperationCode _lastRemoveStatus;
        private OperationCode _lastUpdateStatus;

        private int _count;
    }
}
