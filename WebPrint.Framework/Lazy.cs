using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPrint.Framework
{
    //Adapter From: http://blog.zhaojie.me/2009/08/from-delegate-to-others.html
    //.Net 4.0 已有System.Lazy<T>
    /*
    public class Lazy<T>
    {
        public Lazy(Func<T> func)
        {
            this.m_initialized = false;
            this.m_func = func;
            this.m_mutex = new object();
        }

        private Func<T> m_func;

        private bool m_initialized;
        private object m_mutex;
        private T m_value;

        public T Value
        {
            get
            {
                if (!this.m_initialized)
                {
                    lock (this.m_mutex)
                    {
                        if (!this.m_initialized)
                        {
                            this.m_value = this.m_func();
                            this.m_func = null;
                            this.m_initialized = true;
                        }
                    }
                }

                return this.m_value;
            }
        }
    }
     * */
}
