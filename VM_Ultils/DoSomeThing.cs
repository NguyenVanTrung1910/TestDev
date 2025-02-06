namespace Cl.DataAccess.EF.common
{
    public static class clsUtils
    {
        public static string GetLookUpTitle(this string Value)
        {
            if (Value == null)
                return "";
            return string.Join(", ", Value.Split('|').Where((c, i) => i % 2 != 0));

        }
        public static void ThrowIfNull<T>(this T obj, string parameterName)
               where T : class
        {
            if (obj == null) throw new ArgumentNullException(parameterName);
        }
        public static List<int> GetIDsFormString(string Ids)
        {
            List<int> lstValues = new List<int>();
            if (!string.IsNullOrEmpty(Ids))
            {
                string[] temp = Ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                lstValues = temp.Select(x => Convert.ToInt32(x)).ToList();
            }
            return lstValues;
        }
        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
        public static string GetVietTat(string _Title)
        {
            if (!string.IsNullOrEmpty(_Title))
            {
                return string.Join("", _Title.Split(' ').ToList().Where(y => !string.IsNullOrEmpty(y)).Select(x => x[0])).ToUpper();
            }
            else return "";

        }

        public static T ConvertEntityToItem<T>(object key) where T : new()
        {
            T oT = new T();
            string temp = Newtonsoft.Json.JsonConvert.SerializeObject(key);
            oT = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(temp);
            return oT;
        }


    }
}
