/*******************************************************************************
 * 作者：星星    
 * 描述：MD5加密   
 * 修改记录： 
*********************************************************************************/
using System;
using System.Text;
using System.Security.Cryptography;

namespace Panda.Code
{
    public class Md5Encrypt
    {
        /// <summary>
        /// MD5加密码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code">32/16</param>
        /// <returns>返回小写加密码串</returns>
        public static string Md5(string str, int code)
        {
            MD5 md5 = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] md5Data = md5.ComputeHash(data);
            string strResult = BitConverter.ToString(md5Data);
            md5.Clear();

            string strEncrypt = string.Empty;
            if (code == 32)
            {
                strEncrypt = strResult.Replace("-", "").ToLower();
            }
            else if (code == 16)
            {
                strEncrypt = strResult.Replace("-", "").Substring(8, 16).ToLower();
            }
            return strEncrypt;
        }
    }
}
