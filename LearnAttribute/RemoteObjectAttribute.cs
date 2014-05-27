using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnAttribute
{
    public enum RemoteServers
    {
        JEANVALJEAN,
        JAVERT,
        COSETTE
    }
    public class RemoteObjectAttribute : Attribute
    {
        public RemoteObjectAttribute(RemoteServers server)
        {
            this.server = server;
        }
        private RemoteServers server;

        public RemoteServers Server
        {
            get { return server; }
            set { server = value; }
        }
    }
}
