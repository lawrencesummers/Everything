namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MyFileSystemWatcher : FileSystemWatcher, IDisposable
    {
        private Dictionary<string, DateTime> aRwYlIyUm3;
        [CompilerGenerated]
        private bool bool_0;
        private FileSystemEventHandler fileSystemEventHandler_0;
        private FileSystemEventHandler fileSystemEventHandler_1;
        private FileSystemEventHandler fileSystemEventHandler_2;
        private int int_0;
        private RenamedEventHandler renamedEventHandler_0;
        private TimeSpan timeSpan_0;

        public event FileSystemEventHandler Changed
        {
            add
            {
                FileSystemEventHandler handler2;
                FileSystemEventHandler handler = this.fileSystemEventHandler_0;
                do
                {
                    handler2 = handler;
                    FileSystemEventHandler handler3 = (FileSystemEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.fileSystemEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                FileSystemEventHandler handler2;
                FileSystemEventHandler handler = this.fileSystemEventHandler_0;
                do
                {
                    handler2 = handler;
                    FileSystemEventHandler handler3 = (FileSystemEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.fileSystemEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public event FileSystemEventHandler Created
        {
            add
            {
                FileSystemEventHandler handler2;
                FileSystemEventHandler handler = this.fileSystemEventHandler_1;
                do
                {
                    handler2 = handler;
                    FileSystemEventHandler handler3 = (FileSystemEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.fileSystemEventHandler_1, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                FileSystemEventHandler handler2;
                FileSystemEventHandler handler = this.fileSystemEventHandler_1;
                do
                {
                    handler2 = handler;
                    FileSystemEventHandler handler3 = (FileSystemEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.fileSystemEventHandler_1, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public event FileSystemEventHandler Deleted
        {
            add
            {
                FileSystemEventHandler handler2;
                FileSystemEventHandler handler = this.fileSystemEventHandler_2;
                do
                {
                    handler2 = handler;
                    FileSystemEventHandler handler3 = (FileSystemEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.fileSystemEventHandler_2, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                FileSystemEventHandler handler2;
                FileSystemEventHandler handler = this.fileSystemEventHandler_2;
                do
                {
                    handler2 = handler;
                    FileSystemEventHandler handler3 = (FileSystemEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.fileSystemEventHandler_2, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public event RenamedEventHandler Renamed
        {
            add
            {
                RenamedEventHandler handler2;
                RenamedEventHandler handler = this.renamedEventHandler_0;
                do
                {
                    handler2 = handler;
                    RenamedEventHandler handler3 = (RenamedEventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<RenamedEventHandler>(ref this.renamedEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                RenamedEventHandler handler2;
                RenamedEventHandler handler = this.renamedEventHandler_0;
                do
                {
                    handler2 = handler;
                    RenamedEventHandler handler3 = (RenamedEventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<RenamedEventHandler>(ref this.renamedEventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public MyFileSystemWatcher()
        {
            this.method_0();
        }

        public MyFileSystemWatcher(string Path) : base(Path)
        {
            this.method_0();
        }

        public MyFileSystemWatcher(string Path, string Filter) : base(Path, Filter)
        {
            this.method_0();
        }

        public void Dispose()
        {
            base.Dispose();
        }

        private void method_0()
        {
            this.Interval = 100;
            this.FilterRecentEvents = true;
            this.aRwYlIyUm3 = new Dictionary<string, DateTime>();
            base.Created += new FileSystemEventHandler(this.MyFileSystemWatcher_Created);
            base.Changed += new FileSystemEventHandler(this.MyFileSystemWatcher_Changed);
            base.Deleted += new FileSystemEventHandler(this.MyFileSystemWatcher_Deleted);
            base.Renamed += new RenamedEventHandler(this.MyFileSystemWatcher_Renamed);
        }

        private bool method_1(string string_0)
        {
            bool flag = false;
            if (!this.FilterRecentEvents)
            {
                return flag;
            }
            if (this.aRwYlIyUm3.ContainsKey(string_0))
            {
                DateTime time = this.aRwYlIyUm3[string_0];
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - time);
                flag = span < this.timeSpan_0;
                this.aRwYlIyUm3[string_0] = now;
                return flag;
            }
            this.aRwYlIyUm3.Add(string_0, DateTime.Now);
            return false;
        }

        private void MyFileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!this.method_1(e.FullPath))
            {
                this.OnChanged(e);
            }
        }

        private void MyFileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (!this.method_1(e.FullPath))
            {
                this.OnCreated(e);
            }
        }

        private void MyFileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (!this.method_1(e.FullPath))
            {
                this.OnDeleted(e);
            }
        }

        private void MyFileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (!this.method_1(e.OldFullPath))
            {
                this.OnRenamed(e);
            }
        }

        protected virtual void OnChanged(FileSystemEventArgs e)
        {
            if (this.fileSystemEventHandler_0 != null)
            {
                this.fileSystemEventHandler_0(this, e);
            }
        }

        protected virtual void OnCreated(FileSystemEventArgs e)
        {
            if (this.fileSystemEventHandler_1 != null)
            {
                this.fileSystemEventHandler_1(this, e);
            }
        }

        protected virtual void OnDeleted(FileSystemEventArgs e)
        {
            if (this.fileSystemEventHandler_2 != null)
            {
                this.fileSystemEventHandler_2(this, e);
            }
        }

        protected virtual void OnRenamed(RenamedEventArgs e)
        {
            if (this.renamedEventHandler_0 != null)
            {
                this.renamedEventHandler_0(this, e);
            }
        }

        public bool FilterRecentEvents
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public int Interval
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
                this.timeSpan_0 = new TimeSpan(0, 0, 0, 0, value);
            }
        }
    }
}

