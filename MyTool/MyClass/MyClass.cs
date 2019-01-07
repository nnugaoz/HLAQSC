using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MyTool.MyClass
{
    public class MyClass<T>
    {
        public MyClass(ref T _t, string _para)
        {
            t = _t;
            para = _para;

            t = Convert_1();
        }

        /// <summary>
        /// a=1&b=2&c=3
        /// </summary>
        /// <returns></returns>
        private T Convert_1()
        {
            Hashtable ht = new Hashtable();
            if (!String.IsNullOrEmpty(para))
            {
                string[] list = para.Split('&');
                for (int i = 0; i < list.Length; i++)
                {
                    string[] it = list[i].Split('=');
                    if (it.Length > 1)
                    {
                        List<string> _list = new List<string>();
                        ht.Add(it[0], it[1]);
                    }
                }
            }

            Type _type = t.GetType();
            PropertyInfo[] PropertyList = _type.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                if (ht[item.Name] == null)
                {
                    item.SetValue(t, "", null);
                }
                else
                {
                    item.SetValue(t, ht[item.Name].ToString(), null);
                }
            }

            return t;
        }

        private T t { get; set; }
        private string para { get; set; }
    }
}
