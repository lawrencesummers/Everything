namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Security.Permissions;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("list")]
    public class CList<T> : List<T>, ICloneable<CList<T>>, ICloneable
    {
        public CList()
        {
        }

        public CList(IEnumerable<T> collection) : base(collection)
        {
        }

        public CList(int capacity) : base(capacity)
        {
        }

        public CList<T> Clone()
        {
            return new CList<T>(this);
        }

        public static CList<V> Repeat<V>(V value, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("count", "Non-negative number required.");
            }
            CList<V> list = new CList<V>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(value);
            }
            return list;
        }

        [HostProtection(SecurityAction.LinkDemand, Synchronization=true)]
        public static CList<V> Synchronized<V>(CList<V> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return new SyncList<V>(list);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }
    }
}

