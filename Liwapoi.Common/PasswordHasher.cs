using System;
using System.Security.Cryptography;

namespace Liwapoi.Common
{
    public sealed class PasswordHasher
    {
        #region Private fields

        private const byte Version = 1;
        private const int Pbkdf2IterCount = 50000;
        private const int Pbkdf22SubkeyLength = 32;
        private const int SaltSize = 16;
        private readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        #endregion

        #region Public methods

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            using var rfc289DriverBytes =
                new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2IterCount, _hashAlgorithmName);

            var salt = rfc289DriverBytes.Salt;
            var bytes = rfc289DriverBytes.GetBytes(Pbkdf22SubkeyLength);

            var inArray = new byte[1 + SaltSize + Pbkdf22SubkeyLength];
            inArray[0] = Version;

            Buffer.BlockCopy(salt, 0, inArray, 1, SaltSize);
            Buffer.BlockCopy(bytes, 0, inArray, SaltSize + 1, Pbkdf22SubkeyLength);

            return Convert.ToBase64String(inArray);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            if (string.IsNullOrEmpty(hashedPassword))
            {
                return PasswordVerificationResult.Failed;
            }

            var numArray = Convert.FromBase64String(hashedPassword);
            if (numArray.Length < 1)
            {
                return PasswordVerificationResult.Failed;
            }

            var version = numArray[0];
            if (version > Version)
            {
                return PasswordVerificationResult.Failed;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(numArray, 1, salt, 0, SaltSize);

            var a = new byte[Pbkdf22SubkeyLength];
            Buffer.BlockCopy(numArray, SaltSize + 1, a, 0, Pbkdf22SubkeyLength);

            using var rfc2898DriveBytes = new Rfc2898DeriveBytes(password, salt, Pbkdf2IterCount, _hashAlgorithmName);
            var bytes = rfc2898DriveBytes.GetBytes(Pbkdf22SubkeyLength);

            return CryptographicOperations.FixedTimeEquals(a, bytes) ?
                PasswordVerificationResult.Success :
                PasswordVerificationResult.Failed;
        }
        #endregion
    }
    public enum PasswordVerificationResult
    {
        Failed,
        Success
    }
}
