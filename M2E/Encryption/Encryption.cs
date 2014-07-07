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
    }
}