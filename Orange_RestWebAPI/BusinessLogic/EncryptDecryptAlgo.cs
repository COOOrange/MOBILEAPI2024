using System;
using System.IO;
using System.Security.Cryptography;
using Orange_RestWebAPI.Connections;

namespace Orange_RestWebAPI.BusinessLogic
{
    public class EncryptDecryptAlgo
    {
        private byte[] KEY_192 = { 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 197 };
        private byte[] IV_192 = { 55, 103, 246, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 222 };

        LogHelper logHelper = new LogHelper();

        public string EncryptTripleDES(string value)
        {
            try
            {
                if (value != "")
                {
                    TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
                    StreamWriter sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();
                    ms.Flush();
                    return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
                }
            }
            catch (Exception ex)
            {
                logHelper.Error("EncryptTripleDES : " + ex.Message);
                throw;
            }
            return "";
        }


    }
}
