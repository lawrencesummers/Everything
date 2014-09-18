namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.Runtime.CompilerServices;

    public class Entry : IDisposable
    {
        [CompilerGenerated]
        private System.DirectoryServices.DirectoryEntry directoryEntry_0;

        public Entry(System.DirectoryServices.DirectoryEntry DirectoryEntry)
        {
            this.DirectoryEntry = DirectoryEntry;
        }

        public void Dispose()
        {
            if (this.DirectoryEntry != null)
            {
                this.DirectoryEntry.Dispose();
                this.DirectoryEntry = null;
            }
        }

        public virtual object GetValue(string Property)
        {
            PropertyValueCollection values = this.DirectoryEntry.Properties[Property];
            return ((values != null) ? values.Value : null);
        }

        public virtual object GetValue(string Property, int Index)
        {
            PropertyValueCollection values = this.DirectoryEntry.Properties[Property];
            return ((values != null) ? values[Index] : null);
        }

        public virtual void Save()
        {
            if (this.DirectoryEntry == null)
            {
                throw new NullReferenceException("DirectoryEntry shouldn't be null");
            }
            this.DirectoryEntry.CommitChanges();
        }

        public virtual void SetValue(string Property, object Value)
        {
            PropertyValueCollection values = this.DirectoryEntry.Properties[Property];
            if (values != null)
            {
                values.Value = Value;
            }
        }

        public virtual void SetValue(string Property, int Index, object Value)
        {
            PropertyValueCollection values = this.DirectoryEntry.Properties[Property];
            if (values != null)
            {
                values[Index] = Value;
            }
        }

        public virtual string CN
        {
            get
            {
                return (string) this.GetValue("cn");
            }
            set
            {
                this.SetValue("cn", value);
            }
        }

        public virtual string Company
        {
            get
            {
                return (string) this.GetValue("company");
            }
            set
            {
                this.SetValue("company", value);
            }
        }

        public virtual string CountryCode
        {
            get
            {
                return (string) this.GetValue("countrycode");
            }
            set
            {
                this.SetValue("countrycode", value);
            }
        }

        public virtual System.DirectoryServices.DirectoryEntry DirectoryEntry
        {
            [CompilerGenerated]
            get
            {
                return this.directoryEntry_0;
            }
            [CompilerGenerated]
            set
            {
                this.directoryEntry_0 = value;
            }
        }

        public virtual string DisplayName
        {
            get
            {
                return (string) this.GetValue("displayname");
            }
            set
            {
                this.SetValue("displayname", value);
            }
        }

        public virtual string DistinguishedName
        {
            get
            {
                return (string) this.GetValue("distinguishedname");
            }
            set
            {
                this.SetValue("distinguishedname", value);
            }
        }

        public virtual string Email
        {
            get
            {
                return (string) this.GetValue("mail");
            }
            set
            {
                this.SetValue("mail", value);
            }
        }

        public virtual string GivenName
        {
            get
            {
                return (string) this.GetValue("givenname");
            }
            set
            {
                this.SetValue("givenname", value);
            }
        }

        public virtual string Initials
        {
            get
            {
                return (string) this.GetValue("initials");
            }
            set
            {
                this.SetValue("initials", value);
            }
        }

        public virtual List<string> MemberOf
        {
            get
            {
                List<string> list = new List<string>();
                PropertyValueCollection values = this.DirectoryEntry.Properties["memberof"];
                foreach (object obj2 in values)
                {
                    list.Add((string) obj2);
                }
                return list;
            }
        }

        public virtual string Name
        {
            get
            {
                return (string) this.GetValue("name");
            }
            set
            {
                this.SetValue("name", value);
            }
        }

        public virtual string Office
        {
            get
            {
                return (string) this.GetValue("physicaldeliveryofficename");
            }
            set
            {
                this.SetValue("physicaldeliveryofficename", value);
            }
        }

        public virtual string SamAccountName
        {
            get
            {
                return (string) this.GetValue("samaccountname");
            }
            set
            {
                this.SetValue("samaccountname", value);
            }
        }

        public virtual string TelephoneNumber
        {
            get
            {
                return (string) this.GetValue("telephonenumber");
            }
            set
            {
                this.SetValue("telephonenumber", value);
            }
        }

        public virtual string Title
        {
            get
            {
                return (string) this.GetValue("title");
            }
            set
            {
                this.SetValue("title", value);
            }
        }
    }
}

