﻿using System.Security.Cryptography;
using System.Text;
namespace M2E.Encryption
{
    public class Encryption
    {
        public string GetEncryptionKey(string plainText, string key)
        {            
            return AES.Encrypt(plainText, key);
        }
        public string GetDecryptionValue(string cipherText, string key)
        {
            return AES.Decrypt(cipherText, key);
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}