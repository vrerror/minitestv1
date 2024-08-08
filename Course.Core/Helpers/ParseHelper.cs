using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Course.Core.Helpers
{
    public static class ParseHelper
    {
        public static string ToUniqueFileName(this string value)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(value);
                string extension = Path.GetExtension(value);
                string res = $"{fileName}-{DateTime.Now.ToString("yyMMddHHmmss")}{extension}";
                return res;
            }
            catch
            {
            }
            return value;
        }
    }
}
