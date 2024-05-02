using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

namespace Lab_4_HashTable
{
    class Program
    {
        static string studentId;
        static string name;
        static string latestHashKeyArray;
        static string latestHashKeyList;
        static string latestHashKeyLinkedList;

        static void Main(string[] args)
        {
            // Create instances of the three list classes
            MyHashTableArray hashTableArray = new MyHashTableArray();
            MyHashTableList hashTableList = new MyHashTableList();
            MyHashTableLinkedList hashTableLinkedList = new MyHashTableLinkedList();

            while (true)
            {
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Unhashed List");
                Console.WriteLine("3. Save Lists");
                Console.WriteLine("4. Load Lists");
                Console.WriteLine("5. Remove Student");
                Console.WriteLine("6. Finalize and Exit");
                Console.WriteLine("Enter your choice:");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddStudent(hashTableArray, hashTableList, hashTableLinkedList);
                        break;
                    case 2:
                        ViewUnhashedList(hashTableArray, hashTableList, hashTableLinkedList);
                        break;
                    case 3:
                        SaveLists(hashTableArray, hashTableList, hashTableLinkedList);
                        break;
                    case 4:
                        LoadLists(hashTableArray, hashTableList, hashTableLinkedList);
                        break;
                    case 5:
                        RemoveStudent(hashTableArray, hashTableList, hashTableLinkedList);
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void AddStudent(MyHashTableArray hashTableArray, MyHashTableList hashTableList, MyHashTableLinkedList hashTableLinkedList)
        {
            Console.WriteLine("Enter Student ID:");
            studentId = Console.ReadLine();
            Console.WriteLine("Enter Student Name:");
            name = Console.ReadLine();

            // Create a Student object with provided ID and name
            Student student = new Student(studentId, name);

            // Hash the student information using different methods
            int hashArray = CodeManager.FNV1aHash($"{studentId}, {name}");
            int hashList = CodeManager.DJB2Hash($"{studentId}, {name}");
            string hashLinkedList = CodeManager.EncryptAes($"{studentId}, {name}");

            // Add the hashed student information to respective lists
            hashTableArray.Add($"{studentId}, {name}", student);
            hashTableList.Add($"{studentId}, {name}", student);
            hashTableLinkedList.Add($"{studentId}, {name}", student);
    
            Console.WriteLine("Student added successfully.");
            Console.WriteLine($"Hash keys: Array: {hashArray}, List: {hashList}, LinkedList: {hashLinkedList}");

            // Store the latest hash keys generated
            latestHashKeyArray = hashArray.ToString();
            latestHashKeyList = hashList.ToString();
            latestHashKeyLinkedList = hashLinkedList;
        }



        static void RemoveStudent(MyHashTableArray hashTableArray, MyHashTableList hashTableList, MyHashTableLinkedList hashTableLinkedList)
        {
            Console.WriteLine("Enter Student ID to remove:");
            string studentIdToRemove = Console.ReadLine();

            // Remove student from the hash tables
            hashTableArray.Remove($"{studentIdToRemove}, {name}");
            hashTableList.Remove($"{studentIdToRemove}, {name}");
            hashTableLinkedList.Remove($"{studentIdToRemove}, {name}");

            // Remove the entry corresponding to the removed student from the JSON files
            CodeManager.RemoveEntryFromJson("hashTableArray.json", studentIdToRemove);
            CodeManager.RemoveEntryFromJson("hashTableList.json", studentIdToRemove);
            CodeManager.RemoveEntryFromJson("hashTableLinkedList.json", studentIdToRemove);

            Console.WriteLine("Student removed successfully. Remember to Finalize and exit before trying to view the new list");
        }

        static void ViewUnhashedList(MyHashTableArray hashTableArray, MyHashTableList hashTableList,
            MyHashTableLinkedList hashTableLinkedList)
        {
            Console.WriteLine("Enter the hash key:");

            string hashKey = Console.ReadLine();

            Console.WriteLine("Enter the choice for the list you want to view:");
            Console.WriteLine("1. Array");
            Console.WriteLine("2. List");
            Console.WriteLine("3. LinkedList");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                return;
            }

            switch (choice)
            {
                case 1:
                    // Check if hash key matches
                    if (latestHashKeyArray == hashKey)
                    {
                        // Display unhashed list
                        Console.WriteLine("Array List:");
                        foreach (var pair in hashTableArray.GetAllPairs())
                        {
                            Console.WriteLine($"Student ID: {pair.Value.StudentId}, Name: {pair.Value.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect hash key.");
                    }

                    break;
                case 2:
                    // Check if hash key matches
                    if (latestHashKeyList == hashKey)
                    {
                        // Display unhashed list
                        Console.WriteLine("List:");
                        foreach (var pair in hashTableList.GetAllPairs())
                        {
                            Console.WriteLine($"Student ID: {pair.Value.StudentId}, Name: {pair.Value.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect hash key.");
                    }

                    break;
                case 3:
                    // Check if hash key matches
                    if (latestHashKeyLinkedList == hashKey)
                    {
                        // Display unhashed list
                        Console.WriteLine("LinkedList:");
                        foreach (var pair in hashTableLinkedList.GetAllPairs())
                        {
                            Console.WriteLine($"Student ID: {pair.Value.StudentId}, Name: {pair.Value.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect hash key.");
                    }

                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }

        static void SaveLists(MyHashTableArray hashTableArray, MyHashTableList hashTableList, MyHashTableLinkedList hashTableLinkedList)
        {
            try
            {
                // Serialize and encrypt hash table array data
                var dataArray = new { Pairs = hashTableArray.GetAllPairs(), LatestHashKeyArray = latestHashKeyArray };
                var jsonDataArray = JsonConvert.SerializeObject(dataArray);
                var encryptedDataArray = CodeManager.EncryptAes(jsonDataArray);
                File.WriteAllText("hashTableArray.json", encryptedDataArray);

                // Serialize and encrypt hash table list data
                var dataList = new { Pairs = hashTableList.GetAllPairs(), LatestHashKeyList = latestHashKeyList };
                var jsonDataList = JsonConvert.SerializeObject(dataList);
                var encryptedDataList = CodeManager.EncryptAes(jsonDataList);
                File.WriteAllText("hashTableList.json", encryptedDataList);

                // Serialize and encrypt hash table linked list data
                var dataLinkedList = new { Pairs = hashTableLinkedList.GetAllPairs(), LatestHashKeyLinkedList = latestHashKeyLinkedList };
                var jsonDataLinkedList = JsonConvert.SerializeObject(dataLinkedList);
                var encryptedDataLinkedList = CodeManager.EncryptAes(jsonDataLinkedList);
                File.WriteAllText("hashTableLinkedList.json", encryptedDataLinkedList);

                Console.WriteLine("Lists saved successfully.Remember to Finalize and Exit before viewing the list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving lists: {ex.Message}");
                // Optionally, log the exception details for further analysis
                // LogException(ex);
            }
        }


        static void LoadLists(MyHashTableArray hashTableArray, MyHashTableList hashTableList, MyHashTableLinkedList hashTableLinkedList)
        {
            try
            {
                // Load encrypted data from files
                var encryptedDataArray = File.ReadAllText("hashTableArray.json");
                var encryptedDataList = File.ReadAllText("hashTableList.json");
                var encryptedDataLinkedList = File.ReadAllText("hashTableLinkedList.json");

                // Decrypt the encrypted data
                var decryptedDataArray = CodeManager.DecryptString(encryptedDataArray);
                var decryptedDataList = CodeManager.DecryptString(encryptedDataList);
                var decryptedDataLinkedList = CodeManager.DecryptString(encryptedDataLinkedList);

                // Trim the common part until the start of the JSON object
                decryptedDataArray = TrimToJsonObject(decryptedDataArray);
                decryptedDataList = TrimToJsonObject(decryptedDataList);
                decryptedDataLinkedList = TrimToJsonObject(decryptedDataLinkedList);

                // Debug: Print trimmed decrypted data
                Console.WriteLine("Trimmed Decrypted Data Array: " + decryptedDataArray);
                Console.WriteLine("Trimmed Decrypted Data List: " + decryptedDataList);
                Console.WriteLine("Trimmed Decrypted Data LinkedList: " + decryptedDataLinkedList);

                // Deserialize hash tables from trimmed decrypted JSON data
                var dataArray = JsonConvert.DeserializeObject<dynamic>(decryptedDataArray);
                var dataList = JsonConvert.DeserializeObject<dynamic>(decryptedDataList);
                var dataLinkedList = JsonConvert.DeserializeObject<dynamic>(decryptedDataLinkedList);

                // Extract pairs and latest hash keys
                var hashTableArrayPairs = dataArray.Pairs.ToObject<List<KeyValuePair<string, Student>>>();
                var hashTableListPairs = dataList.Pairs.ToObject<List<KeyValuePair<string, Student>>>();
                var hashTableLinkedListPairs = dataLinkedList.Pairs.ToObject<List<KeyValuePair<string, Student>>>();

                latestHashKeyArray = dataArray.LatestHashKeyArray;
                latestHashKeyList = dataList.LatestHashKeyList;
                latestHashKeyLinkedList = dataLinkedList.LatestHashKeyLinkedList;

                // Clear existing hash tables
                hashTableArray.Clear();
                hashTableList.Clear();
                hashTableLinkedList.Clear();

                // Add items from JSON to hash tables
                foreach (var pair in hashTableArrayPairs)
                {
                    hashTableArray.Add(pair.Key, pair.Value);
                }

                foreach (var pair in hashTableListPairs)
                {
                    hashTableList.Add(pair.Key, pair.Value);
                }

                foreach (var pair in hashTableLinkedListPairs)
                {
                    hashTableLinkedList.Add(pair.Key, pair.Value);
                }

                Console.WriteLine("Lists loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading lists: {ex.Message}");
                // Optionally, log the exception details for further analysis
                // LogException(ex);
            }
        }

        static string TrimToJsonObject(string decryptedData)
        {
            int jsonStartIndex = decryptedData.IndexOf('{');
            if (jsonStartIndex >= 0)
            {
                return decryptedData.Substring(jsonStartIndex);
            }
            else
            {
                // If "{" is not found, return the original string
                return decryptedData;
            }
        }
    }
}
