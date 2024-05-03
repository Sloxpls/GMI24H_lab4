namespace hashtable_inlämning;
public class Student {
    public string id { get; set; }
    public string name { get; set; }
    public Student() {
        Console.Write("Write Students ID: ");
        this.name = Console.ReadLine();
        Console.Write("Write Student Name: ");
        this.id = Console.ReadLine();
    }

    public void PrintStudentInfo() {
        Console.WriteLine("Student info");
        Console.WriteLine($"Student name {name}, id {id}");
    }
    // och annan info
    // bara lägga in i construkorn
}