namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;

    public class ADHelper : IDisposable
    {
        private DirectoryEntry directoryEntry_0 = null;
        private DirectorySearcher directorySearcher_0 = null;
        private string string_0 = "";
        private string string_1 = "";
        private string string_2 = "";
        private string string_3 = "";
        private string sZuthwwDue = "";

        public ADHelper(string Query, string UserName, string Password, string Path)
        {
            this.directoryEntry_0 = new DirectoryEntry(Path, UserName, Password, AuthenticationTypes.Secure);
            this.Path = Path;
            this.UserName = UserName;
            this.Password = Password;
            this.Query = Query;
            this.directorySearcher_0 = new DirectorySearcher(this.directoryEntry_0);
            this.directorySearcher_0.Filter = Query;
            this.directorySearcher_0.PageSize = 0x3e8;
        }

        public virtual bool Authenticate()
        {
            try
            {
                if (!this.directoryEntry_0.Guid.ToString().ToLower().Trim().Equals(""))
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public virtual void Close()
        {
            this.directoryEntry_0.Close();
        }

        public void Dispose()
        {
            if (this.directoryEntry_0 != null)
            {
                this.directoryEntry_0.Close();
                this.directoryEntry_0.Dispose();
                this.directoryEntry_0 = null;
            }
            if (this.directorySearcher_0 != null)
            {
                this.directorySearcher_0.Dispose();
                this.directorySearcher_0 = null;
            }
        }

        public virtual List<Entry> FindActiveGroupMembers(string GroupName)
        {
            try
            {
                List<Entry> list = this.FindGroups("cn=" + GroupName, new object[0]);
                return ((list.Count < 1) ? new List<Entry>() : this.FindActiveUsersAndGroups("memberOf=" + list[0].DistinguishedName, new object[0]));
            }
            catch
            {
                return new List<Entry>();
            }
        }

        public virtual List<Entry> FindActiveGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))", Filter);
            return this.FindGroups(Filter, new object[0]);
        }

        public virtual List<Entry> FindActiveUsers(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))", Filter);
            return this.FindUsers(Filter, new object[0]);
        }

        public virtual List<Entry> FindActiveUsersAndGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))", Filter);
            return this.FindUsersAndGroups(Filter, new object[0]);
        }

        public virtual List<Entry> FindAll()
        {
            List<Entry> list = new List<Entry>();
            using (SearchResultCollection results = this.directorySearcher_0.FindAll())
            {
                foreach (SearchResult result in results)
                {
                    list.Add(new Entry(result.GetDirectoryEntry()));
                }
            }
            return list;
        }

        public virtual List<Entry> FindComputers(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(objectClass=computer)({0}))", Filter);
            this.directorySearcher_0.Filter = Filter;
            return this.FindAll();
        }

        public virtual List<Entry> FindGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(objectClass=Group)(objectCategory=Group)({0}))", Filter);
            this.directorySearcher_0.Filter = Filter;
            return this.FindAll();
        }

        public virtual Entry FindOne()
        {
            return new Entry(this.directorySearcher_0.FindOne().GetDirectoryEntry());
        }

        public virtual Entry FindUserByUserName(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                throw new ArgumentNullException("UserName");
            }
            List<Entry> list = this.FindUsers("samAccountName=" + UserName, new object[0]);
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public virtual List<Entry> FindUsers(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(objectClass=User)(objectCategory=Person)({0}))", Filter);
            this.directorySearcher_0.Filter = Filter;
            return this.FindAll();
        }

        public virtual List<Entry> FindUsersAndGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(|(&(objectClass=Group)(objectCategory=Group))(&(objectClass=User)(objectCategory=Person)))({0}))", Filter);
            this.directorySearcher_0.Filter = Filter;
            return this.FindAll();
        }

        public virtual string Password
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
                if (this.directoryEntry_0 != null)
                {
                    this.directoryEntry_0.Close();
                    this.directoryEntry_0.Dispose();
                    this.directoryEntry_0 = null;
                }
                if (this.directorySearcher_0 != null)
                {
                    this.directorySearcher_0.Dispose();
                    this.directorySearcher_0 = null;
                }
                this.directoryEntry_0 = new DirectoryEntry(this.string_0, this.string_1, this.string_2, AuthenticationTypes.Secure);
                this.directorySearcher_0 = new DirectorySearcher(this.directoryEntry_0);
                this.directorySearcher_0.Filter = this.Query;
                this.directorySearcher_0.PageSize = 0x3e8;
            }
        }

        public virtual string Path
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
                if (this.directoryEntry_0 != null)
                {
                    this.directoryEntry_0.Close();
                    this.directoryEntry_0.Dispose();
                    this.directoryEntry_0 = null;
                }
                if (this.directorySearcher_0 != null)
                {
                    this.directorySearcher_0.Dispose();
                    this.directorySearcher_0 = null;
                }
                this.directoryEntry_0 = new DirectoryEntry(this.string_0, this.string_1, this.string_2, AuthenticationTypes.Secure);
                this.directorySearcher_0 = new DirectorySearcher(this.directoryEntry_0);
                this.directorySearcher_0.Filter = this.Query;
                this.directorySearcher_0.PageSize = 0x3e8;
            }
        }

        public virtual string Query
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
                this.directorySearcher_0.Filter = this.string_3;
            }
        }

        public virtual string SortBy
        {
            get
            {
                return this.sZuthwwDue;
            }
            set
            {
                this.sZuthwwDue = value;
                this.directorySearcher_0.Sort.PropertyName = this.sZuthwwDue;
                this.directorySearcher_0.Sort.Direction = SortDirection.Ascending;
            }
        }

        public virtual string UserName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
                if (this.directoryEntry_0 != null)
                {
                    this.directoryEntry_0.Close();
                    this.directoryEntry_0.Dispose();
                    this.directoryEntry_0 = null;
                }
                if (this.directorySearcher_0 != null)
                {
                    this.directorySearcher_0.Dispose();
                    this.directorySearcher_0 = null;
                }
                this.directoryEntry_0 = new DirectoryEntry(this.string_0, this.string_1, this.string_2, AuthenticationTypes.Secure);
                this.directorySearcher_0 = new DirectorySearcher(this.directoryEntry_0);
                this.directorySearcher_0.Filter = this.Query;
                this.directorySearcher_0.PageSize = 0x3e8;
            }
        }
    }
}

