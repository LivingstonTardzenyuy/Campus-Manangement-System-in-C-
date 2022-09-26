using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cms.ConFolder
{
    public class EncryptPassword
    {
        public static string GetPassword(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] passwordHash = Encoding.UTF8.GetBytes(password);

            return Encoding.UTF8.GetString(md5.ComputeHash(passwordHash));
        }

        internal static object GetPassword()
        {
            throw new NotImplementedException();
        }
    }
}
