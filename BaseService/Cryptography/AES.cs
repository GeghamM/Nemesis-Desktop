using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BaseService
{
    public static class AES
    {
        private static readonly byte[] Key1 = System.Text.Encoding.UTF8.GetBytes("7063f75bec5f49cc92cc4629a3119fd3");
        private static readonly byte[] Key2 = System.Text.Encoding.UTF8.GetBytes("46fb2adefa80448c");

        public static byte[] Encrypt(string plainText)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key1, Key2);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }
        public static string Decrypt(byte[] cipherText)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key1, Key2);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        public static string ToBase36String(this IEnumerable<byte> toConvert, bool bigEndian = false)
        {
            //the "alphabet" for base-36 encoding is similar in theory to hexadecimal,
            //but uses all 26 English letters a-z instead of just a-f.
            var alphabet = new[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
                'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                'w', 'x', 'y', 'z'
            };

            //most .NET-produced byte arrays are "little-endian" (LSB first),
            //but MSB-first is more natural to read bitwise left-to-right;
            //here, we can handle either way.
            var bytes = bigEndian
            ? toConvert.Reverse().ToArray()
            : toConvert.ToArray();
            var builder = new StringBuilder();

            while (bytes.Any(b => b > 0))
            {
                ulong mod;
                bytes = bytes.DivideBy(36, out mod);
                builder.Insert(0, alphabet[mod]);
            }

            return builder.ToString();

        }
        public static byte[] DivideBy(this byte[] bytes, ulong divisor, out ulong mod, bool preserveSize = true)
        {
            //the byte array MUST be little-endian here or the operation will be totally fubared.
            var bitArray = new BitArray(bytes);

            ulong buffer = 0;
            byte quotientBuffer = 0;
            byte qBufferLen = 0;
            var quotient = new List<byte>();

            //the bitarray indexes its values in little-endian fashion;
            //as the index increases we move from LSB to MSB.
            for (var i = bitArray.Count - 1; i >= 0; --i)
            {
                //The basic idea is similar to decimal long division;
                //starting from the most significant bit, take enough bits
                //to form a number divisible by (greater than) the divisor.
                buffer = (buffer << 1) + (ulong)(bitArray[i] ? 1 : 0);
                if (buffer >= divisor)
                {
                    //Now divide; buffer will never be >= divisor * 2,
                    //so the quotient of buffer / divisor is always 1...
                    quotientBuffer = (byte)((quotientBuffer << 1) + 1);
                    //then subtract the divisor from the buffer, 
                    //to produce the remainder to be carried forward.
                    buffer -= divisor;
                }
                else
                    //to keep our place; if buffer < divisor,
                    //then by definition buffer / divisor == 0 R buffer.
                    quotientBuffer = (byte)(quotientBuffer << 1);

                qBufferLen++;

                if (qBufferLen == 8)
                {
                    //preserveSize forces the output array to be the same number of bytes as the input;
                    //otherwise, insert only if we're inserting a nonzero byte or have already done so,
                    //to truncate leading zeroes.
                    if (preserveSize || quotient.Count > 0 || quotientBuffer > 0)
                        quotient.Add(quotientBuffer);

                    //reset the buffer
                    quotientBuffer = 0;
                    qBufferLen = 0;
                }
            }
            //and when all is said and done what's left in our buffer is the remainder.
            mod = buffer;

            //The quotient list was built MSB-first, but we can't require
            //a little-endian array and then return a big-endian one.
            return quotient.AsEnumerable().Reverse().ToArray();
        }
    }
}