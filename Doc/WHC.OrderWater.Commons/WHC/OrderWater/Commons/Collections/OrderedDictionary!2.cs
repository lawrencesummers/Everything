namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class OrderedDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, ICloneable<OrderedDictionary<TKey, TValue>>, IEnumerable<TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, ICloneable, IEnumerable
    {
        private CDictionary<TKey, TValue> cdictionary_0;
        private CList<KeyValuePair<TKey, TValue>> clist_0;

        public OrderedDictionary()
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>();
            this.clist_0 = new CList<KeyValuePair<TKey, TValue>>();
        }

        public OrderedDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>(dictionary);
            this.clist_0 = new CList<KeyValuePair<TKey, TValue>>();
        }

        public OrderedDictionary(IEqualityComparer<TKey> comparer)
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>(comparer);
            this.clist_0 = new CList<KeyValuePair<TKey, TValue>>();
        }

        public OrderedDictionary(int capacity)
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>(capacity);
            this.clist_0 = new CList<KeyValuePair<TKey, TValue>>();
        }

        public OrderedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>(dictionary, comparer);
            this.clist_0 = new CList<KeyValuePair<TKey, TValue>>();
        }

        public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>(capacity, comparer);
            this.clist_0 = new CList<KeyValuePair<TKey, TValue>>();
        }

        public virtual void Add(TKey key, TValue value)
        {
            this.cdictionary_0.Add(key, value);
            this.clist_0.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public virtual void Clear()
        {
            this.cdictionary_0.Clear();
            this.clist_0.Clear();
        }

        public OrderedDictionary<TKey, TValue> Clone()
        {
            return new OrderedDictionary<TKey, TValue>(this);
        }

        public virtual bool Contains(TKey key)
        {
            return this.ContainsKey(key);
        }

        public virtual bool ContainsKey(TKey key)
        {
            return this.cdictionary_0.ContainsKey(key);
        }

        public virtual bool ContainsValue(TValue value)
        {
            return this.cdictionary_0.ContainsValue(value);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.cdictionary_0.CopyTo(array, arrayIndex);
        }

        public virtual OrderedDictionaryEnumerator<TKey, TValue> GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.clist_0);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.cdictionary_0.GetObjectData(info, context);
        }

        public int IndexOf(TKey key)
        {
            for (int i = 0; i < this.clist_0.Count; i++)
            {
                KeyValuePair<TKey, TValue> pair = this.clist_0[i];
                if (pair.Key.Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(TValue value)
        {
            for (int i = 0; i < this.clist_0.Count; i++)
            {
                KeyValuePair<TKey, TValue> pair = this.clist_0[i];
                if (pair.Value.Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, TKey key, TValue value)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            this.cdictionary_0.Add(key, value);
            this.clist_0.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
        }

        public virtual void OnDeserialization(object sender)
        {
            this.cdictionary_0.OnDeserialization(sender);
        }

        public virtual bool Remove(TKey key)
        {
            bool flag = this.cdictionary_0.Remove(key);
            this.clist_0.RemoveAt(this.IndexOf(key));
            return flag;
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            KeyValuePair<TKey, TValue> pair = this.clist_0[index];
            this.cdictionary_0.Remove(pair.Key);
            this.clist_0.RemoveAt(index);
        }

        [HostProtection(SecurityAction.LinkDemand, Synchronization=true)]
        public static OrderedDictionary<TKey, TValue> Synchronized(OrderedDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return new SyncOrderedDictionary<TKey, TValue>(dictionary);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            this.Add(keyValuePair.Key, keyValuePair.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (this.ContainsKey(item.Key))
            {
                TValue local = this[item.Key];
                if (local.Equals(item.Value))
                {
                    return true;
                }
            }
            return false;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (this.ContainsKey(item.Key))
            {
                TValue local = this[item.Key];
                if (local.Equals(item.Value))
                {
                    return this.cdictionary_0.Remove(item.Key);
                }
            }
            return false;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.clist_0);
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.clist_0);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new OrderedDictionaryEnumerator<TKey, TValue>(this.clist_0);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            return this.cdictionary_0.TryGetValue(key, out value);
        }

        public virtual IEqualityComparer<TKey> Comparer
        {
            get
            {
                return this.cdictionary_0.Comparer;
            }
        }

        public virtual int Count
        {
            get
            {
                return this.clist_0.Count;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return this.cdictionary_0.IsSynchronized;
            }
        }

        public virtual TValue this[int index]
        {
            get
            {
                if ((index < 0) || (index >= this.Count))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                KeyValuePair<TKey, TValue> pair = this.clist_0[index];
                return pair.Value;
            }
            set
            {
                if ((index < 0) || (index >= this.Count))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                KeyValuePair<TKey, TValue> pair = this.clist_0[index];
                TKey key = pair.Key;
                if (this.cdictionary_0.ContainsKey(key))
                {
                    this.cdictionary_0[key] = value;
                    this.clist_0[index] = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                if (this.cdictionary_0.ContainsKey(key))
                {
                    return this.cdictionary_0[key];
                }
                return default(TValue);
            }
            set
            {
                if (this.cdictionary_0.ContainsKey(key))
                {
                    this.cdictionary_0[key] = value;
                    this.clist_0[this.IndexOf(key)] = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        public virtual ICollection<TKey> Keys
        {
            get
            {
                CList<TKey> list = new CList<TKey>();
                for (int i = 0; i < this.clist_0.Count; i++)
                {
                    KeyValuePair<TKey, TValue> pair = this.clist_0[i];
                    list.Add(pair.Key);
                }
                return list;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual ICollection<TValue> Values
        {
            get
            {
                CList<TValue> list = new CList<TValue>();
                for (int i = 0; i < this.clist_0.Count; i++)
                {
                    KeyValuePair<TKey, TValue> pair = this.clist_0[i];
                    list.Add(pair.Value);
                }
                return (ICollection<TValue>) list;
            }
        }
    }
}

