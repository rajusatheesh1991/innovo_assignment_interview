using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Unicode;

namespace InnovoAssignment.Application.Security
{
    public interface IEncryptDecryptManager
    {
       

        string Encrypt(string password);

        bool Verify(string password,string savedHash);

    }

    public class Pbkdf2EncryptDecryptManager : IEncryptDecryptManager
    {
        private const int SALT_INDEX = 0;
        private const int HASH_INDEX = 1;
        private const string SEPARATOR = "_";

        public bool Verify(string password, string savedHash)
        {

            var split = savedHash.Split(SEPARATOR);

            byte[] saltBytes = Convert.FromBase64String(split[SALT_INDEX]);


            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return split[HASH_INDEX].Equals(hash) ? true : false;
        }

        public string Encrypt(string password)
        {

            byte[] salt = Random.GenerateRandomNumber(32);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hash= Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return Convert.ToBase64String(salt) + SEPARATOR + hash;
        }

        

       
    }
}
