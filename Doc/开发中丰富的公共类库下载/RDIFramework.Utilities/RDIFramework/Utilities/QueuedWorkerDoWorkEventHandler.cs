namespace RDIFramework.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public delegate void QueuedWorkerDoWorkEventHandler(object sender, QueuedWorkerDoWorkEventArgs e);
}

