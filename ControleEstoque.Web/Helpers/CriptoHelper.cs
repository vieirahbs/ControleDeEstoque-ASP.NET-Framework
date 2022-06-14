 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace ControleEstoque.Web.Helpers
{
    public static class CriptoHelper
    {
        public static string HashMD5(string val)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(val);
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(bytes);
            string retorno = string.Empty;

            for (int i = 0;i < hash.Length; i++)
            {
                retorno += hash[i].ToString("x2");
            }
            return retorno;
        }
    }
}