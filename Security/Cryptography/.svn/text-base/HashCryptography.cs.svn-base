
/*===========================================================
 * 
 * Origami (Object Relational Gateway Microarchitecture)
 * 
 * Lightweight Enterprise Application Framework
 *
 * Version  : 3.0
 * Author   : Ariyanto
 * E-Mail   : neonerdy@yahoo.com
 *  
 * 
 * © 2009, Under Apache Licence 
 * 
 *==========================================================
 */

using System;
using System.Text;
using System.Security.Cryptography;

namespace Origami.Security.Cryptography
{
    public class HashCryptography
    {
        private string hashAlgorithm;
        private HashAlgorithm hashCryptoService;
        private string key;
        private string salt;

        public HashCryptography(HashProvider hashProvider,string key,string salt)
        {
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(key,
               Encoding.ASCII.GetBytes(salt));

            switch (hashProvider)
            {
                case HashProvider.HMACSHA1:
                    hashCryptoService = new HMACSHA1(rfc.GetBytes(29));
                    break;
                case HashProvider.MACTripleDES:
                    hashCryptoService = new MACTripleDES(rfc.GetBytes(16));
                    break;
                case HashProvider.MD5:
                    hashCryptoService = new MD5CryptoServiceProvider();
                    break;
                case HashProvider.RIPEMD160:
                    hashCryptoService = new RIPEMD160Managed();
                    break;
                case HashProvider.SHA1:
                    hashCryptoService = new SHA1CryptoServiceProvider();
                    break;
                case HashProvider.SHA256:
                    hashCryptoService = new SHA256Managed();
                    break;
                case HashProvider.SHA384:
                    hashCryptoService = new SHA384Managed();
                    break;
                case HashProvider.SHA512:
                    hashCryptoService = new SHA512Managed();
                    break;
            }
        }

     
        public HashCryptography()
        {

        }

        private HashAlgorithm SelectAlgorithm()
        {
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(this.key,
              Encoding.ASCII.GetBytes(this.salt));

            if (hashAlgorithm.Equals(HashProvider.HMACSHA1.ToString()))
            {
                hashCryptoService = new HMACSHA1(rfc.GetBytes(29));
            }
            else if (hashAlgorithm.Equals(HashProvider.MACTripleDES.ToString()))
            {
                hashCryptoService = new MACTripleDES(rfc.GetBytes(16));
            }
            else if (hashAlgorithm.Equals(HashProvider.MD5.ToString()))
            {
                hashCryptoService = new MD5CryptoServiceProvider();
            }
            else if (hashAlgorithm.Equals(HashProvider.RIPEMD160.ToString()))
            {
                hashCryptoService = new RIPEMD160Managed();
            }
            else if (hashAlgorithm.Equals(HashProvider.SHA1.ToString()))
            {
                hashCryptoService = new SHA1CryptoServiceProvider();
            }
            else if (hashAlgorithm.Equals(HashProvider.SHA256.ToString()))
            {
                hashCryptoService = new SHA256Managed();
            }
            else if (hashAlgorithm.Equals(HashProvider.SHA384.ToString()))
            {
                hashCryptoService = new SHA384Managed();
            }
            else if (hashAlgorithm.Equals(HashProvider.SHA512.ToString()))
            {
                hashCryptoService = new SHA512Managed();
            }
            return hashCryptoService;
        }

        public string Algorithm
        {
            get { return hashAlgorithm; }
            set
            {
                hashAlgorithm = value;
                hashCryptoService = SelectAlgorithm();
            }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public string Salt
        {
            get { return salt; }
            set { salt = value; }
        }

        public string Encrypt(string source)
        {
           
            byte[] input = Encoding.ASCII.GetBytes(source);
            hashCryptoService.ComputeHash(input);

            return Convert.ToBase64String(hashCryptoService.Hash);

        }     

    }
}
