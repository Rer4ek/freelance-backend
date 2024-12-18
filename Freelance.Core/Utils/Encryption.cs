﻿using System.Security.Cryptography;
using System.Text;

namespace Freelance.Core.Utils
{
    public static class Encryption
    {

        private static byte[] GetHash(string value)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] hash = SHA256.HashData(valueBytes);

            return hash;
        }

        public static string Encrypt(string value) => Convert.ToHexString(GetHash(value));

        public static bool Compare(string? hash, string? value)
        {
            if (hash == null || value == null)
            {
                return false;
            }

            byte[] hashBytes = Convert.FromHexString(hash);
            byte[] valueHashBytes = GetHash(value);

            return hashBytes.SequenceEqual(valueHashBytes);
        }


    }
}
