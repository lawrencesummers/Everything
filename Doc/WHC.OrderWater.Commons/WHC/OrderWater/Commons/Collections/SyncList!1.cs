namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;

    [Serializable]
    public class SyncList<T> : CList<T>
    {
        protected CList<T> list;

        public SyncList()
        {
            this.list = new CList<T>();
        }

        public SyncList(CList<T> list)
        {
            this.list = new CList<T>();
            this.list = list;
        }

        public void Add(T item)
        {
            lock (this.list)
            {
                this.list.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            lock (this.list)
            {
                this.list.InsertRange(this.Count, collection);
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            lock (this.list)
            {
                return new ReadOnlyCollection<T>(this.list);
            }
        }

        public void Clear()
        {
            lock (this.list)
            {
                this.list.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (this.list)
            {
                return this.list.Contains(item);
            }
        }

        public CList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            lock (this.list)
            {
                return new CList<TOutput>(this.list.ConvertAll<TOutput>(converter));
            }
        }

        public void CopyTo(T[] array)
        {
            lock (this.list)
            {
                this.list.CopyTo(array, 0);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this.list)
            {
                this.list.CopyTo(array, arrayIndex);
            }
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            lock (this.list)
            {
                this.list.CopyTo(index, array, arrayIndex, count);
            }
        }

        public bool Exists(Predicate<T> match)
        {
            return (this.FindIndex(match) != -1);
        }

        public T Find(Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.Find(match);
            }
        }

        public CList<T> FindAll(Predicate<T> match)
        {
            lock (this.list)
            {
                return new CList<T>(this.list.FindAll(match));
            }
        }

        public int FindIndex(Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.FindIndex(match);
            }
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.FindIndex(startIndex, match);
            }
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.FindIndex(startIndex, count, match);
            }
        }

        public T FindLast(Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.FindLast(match);
            }
        }

        public int FindLastIndex(Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.FindLastIndex(match);
            }
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.FindLastIndex(startIndex, match);
            }
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.FindLastIndex(startIndex, count, match);
            }
        }

        public void ForEach(Action<T> action)
        {
            lock (this.list)
            {
                this.list.ForEach(action);
            }
        }

        public List<T>.Enumerator GetEnumerator()
        {
            lock (this.list)
            {
                return this.list.GetEnumerator();
            }
        }

        public CList<T> GetRange(int index, int count)
        {
            lock (this.list)
            {
                return new CList<T>(this.list.GetRange(index, count));
            }
        }

        public int IndexOf(T item)
        {
            lock (this.list)
            {
                return this.list.IndexOf(item);
            }
        }

        public int IndexOf(T item, int index)
        {
            lock (this.list)
            {
                return this.list.IndexOf(item, index);
            }
        }

        public int IndexOf(T item, int index, int count)
        {
            lock (this.list)
            {
                return this.list.IndexOf(item, index, count);
            }
        }

        public void Insert(int index, T item)
        {
            lock (this.list)
            {
                this.list.Insert(index, item);
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            lock (this.list)
            {
                this.list.InsertRange(index, collection);
            }
        }

        public int LastIndexOf(T item)
        {
            lock (this.list)
            {
                return this.list.LastIndexOf(item);
            }
        }

        public int LastIndexOf(T item, int index)
        {
            lock (this.list)
            {
                return this.list.LastIndexOf(item, index);
            }
        }

        public int LastIndexOf(T item, int index, int count)
        {
            lock (this.list)
            {
                return this.list.LastIndexOf(item, index, count);
            }
        }

        public bool Remove(T item)
        {
            lock (this.list)
            {
                return this.list.Remove(item);
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.RemoveAll(match);
            }
        }

        public void RemoveAt(int index)
        {
            lock (this.list)
            {
                this.list.RemoveAt(index);
            }
        }

        public void RemoveRange(int index, int count)
        {
            lock (this.list)
            {
                this.list.RemoveRange(index, count);
            }
        }

        public void Reverse()
        {
            lock (this.list)
            {
                this.list.Reverse();
            }
        }

        public void Reverse(int index, int count)
        {
            lock (this.list)
            {
                this.list.Reverse(index, count);
            }
        }

        public void Sort()
        {
            lock (this.list)
            {
                this.list.Sort();
            }
        }

        public void Sort(IComparer<T> comparer)
        {
            lock (this.list)
            {
                this.list.Sort(comparer);
            }
        }

        public void Sort(Comparison<T> comparison)
        {
            lock (this.list)
            {
                this.list.Sort(comparison);
            }
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            lock (this.list)
            {
                this.list.Sort(index, count, comparer);
            }
        }

        public T[] ToArray()
        {
            T[] localArray = new T[this.Count];
            lock (this.list)
            {
                return this.list.ToArray();
            }
        }

        public void TrimExcess()
        {
            lock (this.list)
            {
                this.list.TrimExcess();
            }
        }

        public bool TrueForAll(Predicate<T> match)
        {
            lock (this.list)
            {
                return this.list.TrueForAll(match);
            }
        }

        public int Capacity
        {
            get
            {
                lock (this.list)
                {
                    return this.list.Capacity;
                }
            }
            set
            {
                lock (this.list)
                {
                    this.list.Capacity = value;
                }
            }
        }

        public int Count
        {
            get
            {
                lock (this.list)
                {
                    return this.list.Count;
                }
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        public T this[int index]
        {
            get
            {
                lock (this.list)
                {
                    return this.list[index];
                }
            }
            set
            {
                lock (this.list)
                {
                    this.list[index] = value;
                }
            }
        }
    }
}

