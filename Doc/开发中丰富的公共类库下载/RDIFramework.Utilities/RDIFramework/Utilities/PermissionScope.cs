namespace RDIFramework.Utilities
{
    using System;

    public enum PermissionScope
    {
        All = -1,
        Detail = -7,
        None = 0,
        User = -6,
        UserCompany = -2,
        UserDepartment = -4,
        UserSubOrg = -3,
        UserWorkgroup = -5
    }
}

