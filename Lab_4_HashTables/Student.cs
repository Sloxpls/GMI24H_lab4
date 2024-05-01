namespace Lab_4_HashTable
{
    public class Student {
        public string studentId { get; set; }
        public string name { get; set; }

        // Add other personal info properties as needed

        public Student(string studentId, string name)
        {
            this.studentId = studentId;
            this.name = name;
        }
        public override string ToString()
        {
            return $"ID: {studentId}, Name: {name}";
        }

    }
}