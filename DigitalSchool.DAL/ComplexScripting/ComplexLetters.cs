using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace DS.DAL.ComplexScripting
{
    public static class ComplexLetters
    {
        public static byte[] bytes = Encoding.ASCII.GetBytes("w/WJkhrt");

        public static string getTangledLetters(string inputLine)
        {
            try
            {
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(ComplexLetters.bytes, ComplexLetters.bytes), CryptoStreamMode.Write);
                StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream);
                streamWriter.Write(inputLine);
                ((TextWriter)streamWriter).Flush();
                cryptoStream.FlushFinalBlock();
                ((TextWriter)streamWriter).Flush();
                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            catch
            {
                return "r";
            }
        }

        public static string getEntangledLetters(string inputline)
        {
            try
            {
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                return new StreamReader((Stream)new CryptoStream((Stream)new MemoryStream(Convert.FromBase64String(inputline)), cryptoServiceProvider.CreateDecryptor(ComplexLetters.bytes, ComplexLetters.bytes), CryptoStreamMode.Read)).ReadToEnd();
            }
            catch
            {
                return "r";
            }
        }
    }
}
