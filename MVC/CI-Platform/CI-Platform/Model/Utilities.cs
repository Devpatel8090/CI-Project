

namespace CI_Platform.Model
{
    public class Utilities
    {
        public string Encodepass(string str)
        {
            byte[] encData_byte = new byte[str.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(str);
            string encodedPassword = Convert.ToBase64String(encData_byte);
            return encodedPassword;
        }
    }
}
