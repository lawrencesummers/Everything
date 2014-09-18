namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public static class AbortableThreadPool
    {
        private static readonly Dictionary<WorkItem, Thread> dictionary_0 = new Dictionary<WorkItem, Thread>();
        private static readonly LinkedList<WorkItem> linkedList_0 = new LinkedList<WorkItem>();

        public static WorkItemStatus Cancel(WorkItem item, bool allowAbort)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            lock (linkedList_0)
            {
                LinkedListNode<WorkItem> node = linkedList_0.Find(item);
                if (node != null)
                {
                    linkedList_0.Remove(node);
                    return WorkItemStatus.Queued;
                }
                if (dictionary_0.ContainsKey(item))
                {
                    if (allowAbort)
                    {
                        dictionary_0[item].Abort();
                        dictionary_0.Remove(item);
                        return WorkItemStatus.Aborted;
                    }
                    return WorkItemStatus.Executing;
                }
                return WorkItemStatus.Completed;
            }
        }

        public static void CancelAll(bool allowAbort)
        {
            lock (linkedList_0)
            {
                linkedList_0.Clear();
                if (allowAbort)
                {
                    foreach (Thread thread in dictionary_0.Values)
                    {
                        thread.Abort();
                    }
                    dictionary_0.Clear();
                }
            }
        }

        private static void eLytnGgIfg(object object_0)
        {
            ContextCallback callback = null;
            LinkedList<WorkItem> list;
            WorkItem item = null;
            try
            {
                lock ((list = linkedList_0))
                {
                    if (linkedList_0.Count > 0)
                    {
                        item = linkedList_0.First.Value;
                        linkedList_0.RemoveFirst();
                    }
                    if (item == null)
                    {
                        return;
                    }
                    dictionary_0.Add(item, Thread.CurrentThread);
                }
                if (callback == null)
                {
                    <>c__DisplayClass2 class2;
                    callback = new ContextCallback(class2.<HandleItem>b__0);
                }
                ExecutionContext.Run(item.Context, callback, null);
            }
            finally
            {
                lock ((list = linkedList_0))
                {
                    if (item != null)
                    {
                        dictionary_0.Remove(item);
                    }
                }
            }
        }

        public static WorkItemStatus GetStatus(WorkItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            lock (linkedList_0)
            {
                if (linkedList_0.Find(item) != null)
                {
                    return WorkItemStatus.Queued;
                }
                if (dictionary_0.ContainsKey(item))
                {
                    return WorkItemStatus.Executing;
                }
                return WorkItemStatus.Completed;
            }
        }

        public static void Join()
        {
            foreach (Thread thread in dictionary_0.Values)
            {
                thread.Join();
            }
        }

        public static bool Join(int millisecondsTimeout)
        {
            foreach (Thread thread in dictionary_0.Values)
            {
                if (!thread.Join(millisecondsTimeout))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Join(TimeSpan timeout)
        {
            foreach (Thread thread in dictionary_0.Values)
            {
                if (!thread.Join(timeout))
                {
                    return false;
                }
            }
            return true;
        }

        public static WorkItem QueueUserWorkItem(WaitCallback callback)
        {
            return QueueUserWorkItem(callback, null);
        }

        public static WorkItem QueueUserWorkItem(WaitCallback callback, object state)
        {
            WorkItem item = new WorkItem(callback, state, ExecutionContext.Capture());
            lock (linkedList_0)
            {
                linkedList_0.AddLast(item);
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(AbortableThreadPool.eLytnGgIfg));
            return item;
        }

        public static int QueueCount
        {
            get
            {
                lock (linkedList_0)
                {
                    return linkedList_0.Count;
                }
            }
        }

        public static int WorkingCount
        {
            get
            {
                lock (dictionary_0)
                {
                    return dictionary_0.Count;
                }
            }
        }
    }
}

