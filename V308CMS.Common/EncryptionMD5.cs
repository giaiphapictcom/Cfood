using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace V308CMS.Common
{
    public class EncryptionMD5
    {
        public static string Key = "fgame_longtoc";
        static public string ToMd5(string sinput)
        {
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] originalBytes = Encoding.Default.GetBytes(sinput);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).ToLower().Replace("-", "");
        }
        public static string HexEncrypt(string input, string key)
        {
            if (input == null || input.Trim() == "") return null;
            try
            {
                if (string.IsNullOrEmpty(key))
                    key = Key;
                byte[] keyBytes = Encoding.ASCII.GetBytes(key);
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                int a1, a2, a3;
                int j = 0;
                string b, s = "";

                for (int i = 0; i < inputBytes.Length; i++)
                {
                    if (j >= keyBytes.Length) j = 0;

                    a1 = keyBytes[j];
                    j = j + 1;
                    a2 = Convert.ToInt16(inputBytes[i]);
                    a3 = a1 ^ a2;
                    b = String.Format("{0:x2}", a3);
                    switch (b.Length)
                    {
                        case 1:
                            {
                                b = "000" + b;
                                break;
                            }
                        case 2:
                            {
                                b = "00" + b;
                                break;
                            }
                        case 3:
                            {
                                b = "0" + b;
                                break;
                            }
                    }
                    s += b;
                }
                return s;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        public static string HexDecrypt(string input, string key)
        {
            if (input == null || input.Trim() == "") return null;
            try
            {
                if (string.IsNullOrEmpty(key))
                    key = Key;
                byte[] keyBytes = Encoding.ASCII.GetBytes(key);
                byte[] outputBytes = new byte[input.Length / 4];
                long a1, a2, a3;
                int j = 0;
                string b, s = "";
                for (int i = 0; i < input.Length; i += 4)
                {
                    if (j >= keyBytes.Length) j = 0;
                    a1 = keyBytes[j];
                    j = j + 1;
                    b = input.Substring(i, 4);
                    a2 = Convert.ToUInt32(b, 16);
                    a3 = a1 ^ a2;
                    string stOutput = a3.ToString();
                    byte byteOutputs = Convert.ToByte(stOutput);
                    outputBytes[i / 4] = byteOutputs;
                }
                s = Encoding.UTF8.GetString(outputBytes);
                return s;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }
    }
}