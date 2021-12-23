using CarAuction.Logic.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace CarAuction.Logic.Services
{
    public class HashingService : IHashingService
    {
        public (string hashed, byte[] salt) EncryptPassword(string password)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            var salt = new byte[128 / 8];     // to read salt: Convert.ToBase64String(salt)
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            var saltString = Convert.ToBase64String(salt); // for development purposes

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return (hashed, salt);
        }

        public bool VerifyPassword(string enteredPassword, byte[] salt, string storedPassword)
        {
            var encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return encryptedPassw == storedPassword;
        }
    }
}
