namespace WHC.OrderWater.Commons.Threading
{
    using System;
    using System.Threading;

    [Serializable]
    public abstract class DisposableObject : IDisposable
    {
        private DisposeState disposeState_0 = DisposeState.None;
        private EventHandler eventHandler_0;

        public event EventHandler Disposed
        {
            add
            {
                EventHandler handler2;
                EventHandler handler = this.eventHandler_0;
                do
                {
                    handler2 = handler;
                    EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler handler = this.eventHandler_0;
                do
                {
                    handler2 = handler;
                    EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_0, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        protected DisposableObject()
        {
        }

        protected void CheckDisposed()
        {
            if (this.disposeState_0 == DisposeState.Disposed)
            {
                throw new ObjectDisposedException(base.GetType().Name);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposeState_0 == DisposeState.None)
            {
                this.disposeState_0 = DisposeState.Disposing;
                try
                {
                    if (disposing)
                    {
                        this.DisposeManagedResources();
                        this.DisposeUnmanagedResources();
                        this.disposeState_0 = DisposeState.Disposed;
                        this.OnDisposed();
                        GC.SuppressFinalize(this);
                    }
                    else
                    {
                        this.DisposeUnmanagedResources();
                        this.disposeState_0 = DisposeState.Disposed;
                    }
                }
                catch
                {
                    this.disposeState_0 = DisposeState.None;
                    throw;
                }
            }
        }

        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }

        ~DisposableObject()
        {
            this.Dispose(false);
        }

        protected virtual void OnDisposed()
        {
            if (this.eventHandler_0 != null)
            {
                this.eventHandler_0(this, EventArgs.Empty);
            }
        }

        public bool IsDisposed
        {
            get
            {
                return (this.disposeState_0 == DisposeState.Disposed);
            }
        }

        public bool IsDisposing
        {
            get
            {
                return (this.disposeState_0 == DisposeState.Disposing);
            }
        }
    }
}

