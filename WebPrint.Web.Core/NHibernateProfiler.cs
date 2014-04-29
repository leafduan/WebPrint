using System;
using System.Reflection;

namespace WebPrint.Web.Core
{
    public static class NHibernateProfiler
    {
        /* Dynamic load assembly so that there has no need to explicit to import references to project */

        public static void StartProfiler()
        {
            var assembly =
                Assembly.Load(
                    "HibernatingRhinos.Profiler.Appender, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0774796e73ebf640");
            var type = assembly.GetType("HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler");
            /* call overload method through method params type */
            var method = type.GetMethod("Initialize", new Type[] {});
            method.Invoke(null, null);

            //NHibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize()
        }
    }
}
