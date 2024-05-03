using System;

namespace Lab_4_HashTable
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select which hash table to use:");
                Console.WriteLine("1. LinkedList");
                Console.WriteLine("2. List");
                Console.WriteLine("3. Array");
                Console.WriteLine("4. Exit Program");
                Console.Write("Select an option: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        IHashTable<string, Student> hashTable1 = new MyHashTableLinkedList<string, Student>();
                        HashTableMenu(hashTable1);
                        break;
                    case 2:
                        IHashTable<string, Student> hashTable2 = new MyHashTableList<string, Student>();
                        HashTableMenu(hashTable2);
                        break;
                    case 3:
                        IHashTable<string, Student> hashTable3 = new MyHashTableArray<string, Student>();
                        HashTableMenu(hashTable3);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        static void HashTableMenu(IHashTable<string, Student> hashTable)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Hash Table Operations:");
                Console.WriteLine("1. Add Key and Value");
                Console.WriteLine("2. Get Value by Key");
                Console.WriteLine("3. Remove Key");
                Console.WriteLine("4. Go back");
                Console.Write("Select an option: ");

                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.Write("Enter key: ");
                        string keyToAdd = Console.ReadLine();
                        Console.Write("Enter student ID: ");
                        string studentId = Console.ReadLine();
                        Console.Write("Enter student name: ");
                        string studentName = Console.ReadLine();
                        Student valueToAdd = new Student(studentId, studentName);
                        hashTable.Add(keyToAdd, valueToAdd);
                        break;
                    case 2:
                        Console.Write("Enter key to retrieve: ");
                        string key = Console.ReadLine();
                        try
                        {
                            Student value = hashTable.Get(key);
                            PrintStudentInfo(value);
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Key not found.");
                        }
                        break;
                    case 3:
                        Console.Write("Enter key to remove: ");
                        string keyToRemove = Console.ReadLine();
                        try
                        {
                            hashTable.Remove(keyToRemove);
                            Console.WriteLine("Removed successfully.");
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Key not found.");
                        }
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
        // Method to print student information
        public static void PrintStudentInfo(Student student)
        {
            Console.WriteLine("Student info");
            Console.WriteLine($"Student name {student.name}, id {student.id}");
        }
    }
}
