namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed class OrderedDictionaryEnumerator<TKey, TValue> : IEnumerator<TValue>, IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
    {
        private bool bool_0;
        private CList<KeyValuePair<TKey, TValue>> clist_0;
        private int int_0;

        internal OrderedDictionaryEnumerator(CList<KeyValuePair<TKey, TValue>> list)
        {
            this.bool_0 = false;
            this.int_0 = -1;
            this.clist_0 = list;
        }

        public void Dispose()
        {
            this.method_0(true);
        }

        ~OrderedDictionaryEnumerator()
        {
            this.method_0(false);
        }

        private void method_0(bool bool_1)
        {
            if (!this.bool_0)
            {
                if (bool_1)
                {
                    if (this.clist_0 != null)
                    {
                        this.clist_0 = null;
                    }
                    GC.SuppressFinalize(this);
                }
                this.bool_0 = true;
            }
        }

        private KeyValuePair<TKey, TValue> method_1()
        {
            if ((this.int_0 < 0) || (this.int_0 >= this.clist_0.Count))
            {
                throw new InvalidOperationException();
            }
            return this.clist_0[this.int_0];
        }

        public bool MoveNext()
        {
            this.int_0++;
            if (this.int_0 >= this.clist_0.Count)
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            this.int_0 = -1;
        }

        public TValue Current
        {
            get
            {
                return this.method_1().Value;
            }
        }

        public TKey Key
        {
            get
            {
                return this.method_1().Key;
            }
        }

        KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
        {
            get
            {
                return this.method_1();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.method_1().Value;
            }
        }

        public TValue Value
        {
            get
            {
                return this.method_1().Value;
            }
        }
    }
}

