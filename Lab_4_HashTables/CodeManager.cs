using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab_4_HashTable
{
    public static class CodeManager
    {
        // Define the path to the file where the encryption key will be stored
        private const string KeyFilePath = "encryption_key.txt";

        // Generate a random encryption key and store it securely
        public static byte[] GenerateEncryptionKey()
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Ensure AES key size is set to 256 bits
                aesAlg.KeySize = 256;
                aesAlg.GenerateKey();
                aesAlg.GenerateIV();

                byte[] key = aesAlg.Key;

                // Store the encryption key securely
                File.WriteAllBytes(KeyFilePath, key);

                return key;
            }
        }

        // Retrieve the encryption key from storage
        private static byte[] RetrieveEncryptionKey()
        {
            // Check if the encryption key file exists
            if (File.Exists(KeyFilePath))
            {
                return File.ReadAllBytes(KeyFilePath);
            }
            else
            {
                // If the key file doesn't exist, generate a new encryption key
                return GenerateEncryptionKey();
            }
        }

        // Encrypt input string using AES encryption
        public static string EncryptAes(string input)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = RetrieveEncryptionKey();
                aes.Key = key;
                aes.IV = new byte[16]; // Set IV to all zeros for simplicity (not recommended for production)

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (var sw = new StreamWriter(cs))
                            {
                                sw.Write(input);
                            }
                        }

                        return Convert.ToBase64String(aes.IV.Concat(ms.ToArray()).ToArray());
                    }
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = RetrieveEncryptionKey();
                aesAlg.IV = new byte[16]; // Use a new IV for each decryption to ensure security

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static int FNV1aHash(string input)
        {
            // FNV1a hash implementation
            // https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
            unchecked
            {
                const int fnvPrime = 16777619;
                const int offsetBasis = unchecked((int)2166136261);
                int hash = offsetBasis;

                foreach (char c in input)
                {
                    hash ^= c;
                    hash *= fnvPrime;
                }

                return hash;
            }
        }

        public static int DJB2Hash(string input)
        {
            // DJB2 hash implementation
            // http://www.cse.yorku.ca/~oz/hash.html
            int hash = 5381;

            foreach (char c in input)
            {
                hash = ((hash << 5) + hash) + c; /* hash * 33 + c */
            }

            return hash;
        }
        
        public static void RemoveEntryFromJson(string fileName, string studentIdToRemove)
        {
            try
            {
                // Read the encrypted JSON file content
                string encryptedJsonContent = File.ReadAllText(fileName);

                // Decrypt the JSON content
                string decryptedJsonContent = DecryptString(encryptedJsonContent);

                // Find the index of the first occurrence of '{'
                int braceIndex = decryptedJsonContent.IndexOf('{');

                // Check if '{' is found
                if (braceIndex == -1)
                {
                    Console.WriteLine($"Error: Unable to find start of JSON content in {fileName}.");
                    return;
                }

                // Trim the JSON content from the first '{'
                string trimmedJsonContent = decryptedJsonContent.Substring(braceIndex);

                // Deserialize the trimmed JSON content
                dynamic jsonObject = JsonConvert.DeserializeObject(trimmedJsonContent);

                // Check if the jsonObject is null
                if (jsonObject == null)
                {
                    Console.WriteLine($"Error: Failed to deserialize JSON content from {fileName}.");
                    return;
                }

                // Debugging: Print the trimmed and decrypted JSON content
                Console.WriteLine($"Trimmed and Decrypted JSON content from {fileName}: {trimmedJsonContent}");

                // Find the "Pairs" array or other key where the student entry might be stored
                JToken pairsToken = null;
                if (jsonObject["Pairs"] != null)
                {
                    pairsToken = jsonObject["Pairs"];
                }

                // Check if the "Pairs" token is found
                if (pairsToken == null)
                {
                    Console.WriteLine($"Error: 'Pairs' array is null in JSON content from {fileName}.");
                    return;
                }

                // Remove the entry corresponding to the student to be removed
                var updatedPairs = new JArray();
                foreach (var pair in pairsToken)
                {
                    var keyValue = pair.ToObject<JObject>();
                    var key = keyValue["Key"].ToString();
                    if (!key.Contains(studentIdToRemove))
                    {
                        updatedPairs.Add(pair);
                    }
                }

                // Update the "Pairs" array in the JSON object
                jsonObject["Pairs"] = updatedPairs;

                // Serialize the modified JSON object
                string updatedJson = JsonConvert.SerializeObject(jsonObject);

                // Encrypt the updated JSON content
                string encryptedUpdatedJson = EncryptAes(updatedJson);

                // Write the encrypted updated JSON content back to the file
                File.WriteAllText(fileName, encryptedUpdatedJson);

                Console.WriteLine("Student removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing student: {ex.Message}");
                // Optionally, log the exception details for further analysis
                // LogException(ex);
            }
        }


    }
}
