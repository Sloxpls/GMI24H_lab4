namespace Lab_4_HashTable
{
    public class Student
    {
        public string StudentId { get; }
        public string Name { get; }
        // Add other personal info properties as needed

        public Student(string studentId, string name)
        {
            StudentId = studentId;
            Name = name;
        }
    }
}