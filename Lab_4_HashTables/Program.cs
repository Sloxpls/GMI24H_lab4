using Lab_4_HashTable;

class Program {
    static void Main(string[] args) {
        IHashTable hashTable = new MyHashTableLinkedList(); 

        while (true) {
            Console.WriteLine("\nHash Table Operations:");
            Console.WriteLine("1. Add Key-Value");
            Console.WriteLine("2. Get Value by Key");
            Console.WriteLine("3. Remove Key");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option) {
                case 1:
                    Console.Write("Enter key: ");
                    string keyToAdd = Console.ReadLine();
                    
                    Console.Write("Enter student id: ");
                    string studentID = Console.ReadLine();
                    Console.Write("Enter student name: ");
                    string studentName = Console.ReadLine();
                    Student valueToAdd = new Student(studentID, studentName);
                    
                    hashTable.Add(keyToAdd, valueToAdd);
                    Console.WriteLine("Added successfully.");
                    break;
                
                case 2:
                    Console.Write("Enter key to retrieve: ");
                    string keyToGet = Console.ReadLine();
                    var value = hashTable.Get(keyToGet);
                    if (value != null)
                        Console.WriteLine($"Value: {value}");
                    else
                        Console.WriteLine("Key not found.");
                    break;
                
                case 3:
                    Console.Write("Enter key to remove: ");
                    string keyToRemove = Console.ReadLine();
                    hashTable.Remove(keyToRemove);
                    Console.WriteLine("Removed successfully.");
                    break;

                case 4:
                    return; // Exit the program
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}
