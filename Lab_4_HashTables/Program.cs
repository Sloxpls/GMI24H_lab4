using Lab_4_HashTable;

class Program
{
    public static void Main(string[] args) {
        Student student1 = new Student("1", "Jim");
        Student student2 = new Student("2", "Z-tanic");
        Student student3 = new Student("3", "Ola");

        IHashTable hashTableLinkedList = new MyHashTableLinkedList();
        hashTableLinkedList.Add(student1.studentId, student1);
        hashTableLinkedList.Add(student2.studentId, student2);
        hashTableLinkedList.Add(student3.studentId, student3);

        var testStudent = (Student)hashTableLinkedList.Get(student3.studentId);
        if (testStudent != null) {
            Console.WriteLine(testStudent.name);  
        }
        else {
            Console.WriteLine("Student not found.");
        }




    }
}