namespace Lab_4_HashTable
{
    public class Student
    {
        public string id { get; set; }
        public string name { get; set; }

        // Constructor accepting ID and name
        public Student(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}