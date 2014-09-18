namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class SyncDictionary<TKey, TValue> : CDictionary<TKey, TValue>
    {
        private CDictionary<TKey, TValue> cdictionary_0;

        public SyncDictionary()
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>();
        }

        public SyncDictionary(CDictionary<TKey, TValue> dictionary)
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>();
            this.cdictionary_0 = dictionary;
        }

        internal SyncDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.cdictionary_0 = new CDictionary<TKey, TValue>();
            this.cdictionary_0 = (CDictionary<TKey, TValue>) info.GetValue("ParentTable", typeof(CDictionary<TKey, TValue>));
            if (this.cdictionary_0 == null)
            {
                throw new SerializationException("Insufficient state to return the real object.");
            }
        }

        public void Add(TKey key, TValue value)
        {
            lock (this.cdictionary_0)
            {
                this.cdictionary_0.Add(key, value);
            }
        }

        public void Clear()
        {
            lock (this.cdictionary_0)
            {
                this.cdictionary_0.Clear();
            }
        }

        public bool ContainsKey(TKey key)
        {
            return this.cdictionary_0.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            lock (this.cdictionary_0)
            {
                return this.cdictionary_0.ContainsValue(value);
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            lock (this.cdictionary_0)
            {
                this.cdictionary_0.CopyTo(array, index);
            }
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            lock (this.cdictionary_0)
            {
                return this.cdictionary_0.GetEnumerator();
            }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("ParentTable", this.cdictionary_0, typeof(CDictionary<TKey, TValue>));
        }

        public override void OnDeserialization(object sender)
        {
        }

        public bool Remove(TKey key)
        {
            lock (this.cdictionary_0)
            {
                return this.cdictionary_0.Remove(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (this.cdictionary_0)
            {
                return this.cdictionary_0.TryGetValue(key, out value);
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                lock (this.cdictionary_0)
                {
                    return this.cdictionary_0.Comparer;
                }
            }
        }

        public virtual int Count
        {
            get
            {
                return this.cdictionary_0.Count;
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.cdictionary_0[key];
            }
            set
            {
                lock (this.cdictionary_0)
                {
                    this.cdictionary_0[key] = value;
                }
            }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get
            {
                lock (this.cdictionary_0)
                {
                    return new Dictionary<TKey, TValue>.KeyCollection(this.cdictionary_0);
                }
            }
        }

        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get
            {
                lock (this.cdictionary_0)
                {
                    return new Dictionary<TKey, TValue>.ValueCollection(this.cdictionary_0);
                }
            }
        }
    }
}

