namespace RDIFramework.Utilities
{
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections;
    using System.Data;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public class SortLogic
    {
        public static string GetNextId(DataTable dataTable, string id)
        {
            return GetNextId(dataTable.DefaultView, id, BusinessLogic.FieldId);
        }

        public static string GetNextId(DataView dataView, string id)
        {
            return GetNextId(dataView, id, BusinessLogic.FieldId);
        }

        public static string GetNextId(DataTable dataTable, string id, string field)
        {
            return GetNextId(dataTable.DefaultView, id, field);
        }

        public static string GetNextId(DataView dataView, string id, string field)
        {
            string str = string.Empty;
            bool flag = false;
            using (IEnumerator enumerator = dataView.GetEnumerator())
            {
                DataRowView current;
                while (enumerator.MoveNext())
                {
                    current = (DataRowView) enumerator.Current;
                    if (flag)
                    {
                        goto Label_0047;
                    }
                    if (current[field].ToString().Equals(id))
                    {
                        flag = true;
                    }
                }
                return str;
            Label_0047:
                str = current[field].ToString();
            }
            return str;
        }

        public static string GetNextIdDyn([Dynamic] object lstT, string id)
        {
            return (string) GetNextIdDyn((dynamic) lstT, id, BusinessLogic.FieldId);
        }

        public static string GetNextIdDyn([Dynamic] object lstT, string id, string field)
        {
            string str = string.Empty;
            bool flag = false;
            using (IEnumerator enumerator = ((IEnumerable) lstT).GetEnumerator())
            {
                object current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (flag)
                    {
                        goto Label_01E6;
                    }
                    if (<GetNextIdDyn>o__SiteContainer3.<>p__Site8 == null)
                    {
                        <GetNextIdDyn>o__SiteContainer3.<>p__Site8 = CallSite<System.Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SortLogic), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                    }
                    if (<GetNextIdDyn>o__SiteContainer3.<>p__Site8.Target(<GetNextIdDyn>o__SiteContainer3.<>p__Site8, ReflectHelper.GetProperty((dynamic) current, field).ToString().Equals(id)))
                    {
                        flag = true;
                    }
                }
                return str;
            Label_01E6:
                if (<GetNextIdDyn>o__SiteContainer3.<>p__Site5 == null)
                {
                    <GetNextIdDyn>o__SiteContainer3.<>p__Site5 = CallSite<System.Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(SortLogic)));
                }
                return <GetNextIdDyn>o__SiteContainer3.<>p__Site5.Target(<GetNextIdDyn>o__SiteContainer3.<>p__Site5, ReflectHelper.GetProperty((dynamic) current, field).ToString());
            }
        }

        public static string GetPreviousId(DataTable dataTable, string id)
        {
            return GetPreviousId(dataTable.DefaultView, id, BusinessLogic.FieldId);
        }

        public static string GetPreviousId(DataView dataView, string id)
        {
            return GetPreviousId(dataView, id, BusinessLogic.FieldId);
        }

        public static string GetPreviousId(DataTable dataTable, string id, string field)
        {
            return GetPreviousId(dataTable.DefaultView, id, field);
        }

        public static string GetPreviousId(DataView dataView, string id, string field)
        {
            string str = string.Empty;
            foreach (DataRowView view in dataView)
            {
                if (view[field].ToString().Equals(id))
                {
                    return str;
                }
                str = view[field].ToString();
            }
            return str;
        }

        public static string GetPreviousIdDyn([Dynamic] object lstT, string id)
        {
            return (string) GetPreviousIdDyn((dynamic) lstT, id, BusinessLogic.FieldId);
        }

        public static string GetPreviousIdDyn([Dynamic] object lstT, string id, string field)
        {
            string str = string.Empty;
            foreach (object obj2 in (IEnumerable) lstT)
            {
                if (<GetPreviousIdDyn>o__SiteContainerf.<>p__Site11 == null)
                {
                    <GetPreviousIdDyn>o__SiteContainerf.<>p__Site11 = CallSite<System.Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SortLogic), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                }
                if (<GetPreviousIdDyn>o__SiteContainerf.<>p__Site11.Target(<GetPreviousIdDyn>o__SiteContainerf.<>p__Site11, ReflectHelper.GetProperty((dynamic) obj2, field).ToString().Equals(id)))
                {
                    return str;
                }
                str = (string) ReflectHelper.GetProperty((dynamic) obj2, field).ToString().ToString();
            }
            return str;
        }

        public static int Swap(DataTable dataTable, string id, string targetId)
        {
            string targetValue = BusinessLogic.GetProperty(dataTable, id, BusinessLogic.FieldSortCode);
            string str2 = BusinessLogic.GetProperty(dataTable, targetId, BusinessLogic.FieldSortCode);
            return (BusinessLogic.SetProperty(dataTable, id, BusinessLogic.FieldSortCode, str2) + BusinessLogic.SetProperty(dataTable, targetId, BusinessLogic.FieldSortCode, targetValue));
        }

        public static int SwapDyn([Dynamic] object lstT, string id, string targetId)
        {
            int num = 0;
            string str = (string) BusinessLogic.GetPropertyDyn((dynamic) lstT, id, BusinessLogic.FieldSortCode);
            string str2 = (string) BusinessLogic.GetPropertyDyn((dynamic) lstT, targetId, BusinessLogic.FieldSortCode);
            num = (int) BusinessLogic.SetPropertyDyn((dynamic) lstT, id, BusinessLogic.FieldSortCode, str2);
            if (<SwapDyn>o__SiteContainer19.<>p__Site21 == null)
            {
                <SwapDyn>o__SiteContainer19.<>p__Site21 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(SortLogic), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            }
            return (int) <SwapDyn>o__SiteContainer19.<>p__Site21.Target(<SwapDyn>o__SiteContainer19.<>p__Site21, num, BusinessLogic.SetPropertyDyn((dynamic) lstT, targetId, BusinessLogic.FieldSortCode, str));
        }
    }
}

