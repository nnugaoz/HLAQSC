namespace Web.MyLib
{
    public class StringOption
    {
        /// <summary>
        /// 自定义替换
        /// </summary>
        /// <param name="str_old">原始字符串</param>
        /// <param name="target">需要替换的目标</param>
        /// <param name="str">替换后的字符串</param>
        /// <param name="left">向左扩大位数</param>
        /// <param name="right">向右扩大位数</param>
        public static void Replace_Def(ref string str_old, string target, string str, int left = 0, int right = 0)
        {
            string str_new = "";

            int index = str_old.IndexOf(target);
            if (index > 0)
            {
                str_new += str_old.Substring(0, index - left);
                str_new += str;
                str_new += str_old.Substring(index + target.Length + right);
            }

            str_old = str_new;
        }
    }
}