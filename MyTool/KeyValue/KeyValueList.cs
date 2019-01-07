using System.Collections.Generic;

namespace MyTool.KeyValue
{
    public class KeyValueList
    {
        public KeyValueList(string type, string txt)
        {
            list = new List<KeyValue>();
            if (type == "json1")
            {
                string[] list1 = txt.Split(';');
                for (int i = 0; i < list1.Length; i++)
                {
                    if (list1[i] != "")
                    {
                        string[] list2 = list1[i].Split(',');
                        if (list2.Length == 2)
                        {
                            list.Add(new KeyValue(list2[0], list2[1]));
                        }
                    }
                }
            }
            if (type == "json2")
            {
                string[] list1 = txt.Split('&');
                for (int i = 0; i < list1.Length; i++)
                {
                    if (list1[i] != "")
                    {
                        string[] list2 = list1[i].Split('=');
                        if (list2.Length == 2)
                        {
                            list.Add(new KeyValue(list2[0], list2[1]));
                        }
                    }
                }
            }

            fun = GetValue("fun");
            id = GetValue("id");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>如果找不到对应值，则返回值为""</returns>
        public string GetValue(string key)
        {
            if (list != null && list.Count > 0)
            {
                KeyValue obj = list.Find(p => p.key.ToLower() == key.ToLower());
                if (obj != null)
                {
                    return obj.value;
                }
            }

            return "";
        }

        public string GetValue(string key, string rep)
        {
            key = rep.Replace("{0}", key);
            if (list != null && list.Count > 0)
            {
                KeyValue obj = list.Find(p => p.key.ToLower() == key.ToLower());
                if (obj != null)
                {
                    return obj.value;
                }
            }

            return "";
        }

        public void SetValue(string key, string value)
        {
            if (list != null && list.Count > 0)
            {
                KeyValue obj = list.Find(p => p.key.ToLower() == key.ToLower());
                if (obj != null)
                {
                    obj.value = value;
                }
            }
        }

        public void Add(KeyValue it)
        {
            string key = it.key;
            if (list != null && list.Count > 0)
            {
                KeyValue obj = list.Find(p => p.key == key);
                if (obj != null)
                {
                    SetValue(key, it.value);
                    return;
                }
            }

            this.list.Add(it);
        }

        public void Delete(KeyValue it)
        {
            this.list.Remove(it);
        }

        public List<KeyValue> list = new List<KeyValue>();
        public string fun = "";
        public string id = "";
    }
}
