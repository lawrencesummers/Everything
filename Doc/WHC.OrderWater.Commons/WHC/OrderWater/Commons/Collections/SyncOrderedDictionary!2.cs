namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class SyncOrderedDictionary<TKey, TValue> : OrderedDictionary<TKey, TValue>
    {
        private OrderedDictionary<TKey, TValue> orderedDictionary_0;

        public SyncOrderedDictionary()
        {
            this.orderedDictionary_0 = new OrderedDictionary<TKey, TValue>();
        }

        public SyncOrderedDictionary(OrderedDictionary<TKey, TValue> dictionary)
        {
            this.orderedDictionary_0 = new OrderedDictionary<TKey, TValue>();
            this.orderedDictionary_0 = dictionary;
        }

        public override void Add(TKey key, TValue value)
        {
            lock (this.orderedDictionary_0)
            {
                this.orderedDictionary_0.Add(key, value);
            }
        }

        public override void Clear()
        {
            lock (this.orderedDictionary_0)
            {
                this.orderedDictionary_0.Clear();
            }
        }

        public override bool ContainsKey(TKey key)
        {
            return this.orderedDictionary_0.ContainsKey(key);
        }

        public override bool ContainsValue(TValue value)
        {
            lock (this.orderedDictionary_0)
            {
                return this.orderedDictionary_0.ContainsValue(value);
            }
        }

        public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            lock (this.orderedDictionary_0)
            {
                this.orderedDictionary_0.CopyTo(array, index);
            }
        }

        public override OrderedDictionaryEnumerator<TKey, TValue> GetEnumerator()
        {
            lock (this.orderedDictionary_0)
            {
                return this.orderedDictionary_0.GetEnumerator();
            }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("ParentTable", this.orderedDictionary_0, typeof(CDictionary<TKey, TValue>));
        }

        public override void OnDeserialization(object sender)
        {
        }

        public override bool Remove(TKey key)
        {
            lock (this.orderedDictionary_0)
            {
                return this.orderedDictionary_0.Remove(key);
            }
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            lock (this.orderedDictionary_0)
            {
                return this.orderedDictionary_0.TryGetValue(key, out value);
            }
        }

        public override IEqualityComparer<TKey> Comparer
        {
            get
            {
                lock (this.orderedDictionary_0)
                {
                    return this.orderedDictionary_0.Comparer;
                }
            }
        }

        public override int Count
        {
            get
            {
                return this.orderedDictionary_0.Count;
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        public override TValue this[TKey key]
        {
            get
            {
                return this.orderedDictionary_0[key];
            }
            set
            {
                lock (this.orderedDictionary_0)
                {
                    this.orderedDictionary_0[key] = value;
                }
            }
        }

        public override TValue this[int index]
        {
            get
            {
                return this.orderedDictionary_0[index];
            }
            set
            {
                lock (this.orderedDictionary_0)
                {
                    this.orderedDictionary_0[index] = value;
                }
            }
        }

        public override ICollection<TKey> Keys
        {
            get
            {
                lock (this.orderedDictionary_0)
                {
                    return this.orderedDictionary_0.Keys;
                }
            }
        }

        public override ICollection<TValue> Values
        {
            get
            {
                lock (this.orderedDictionary_0)
                {
                    return this.orderedDictionary_0.Values;
                }
            }
        }
    }
}

