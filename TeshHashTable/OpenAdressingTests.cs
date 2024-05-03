
/*
 namespace Lab_4_HashTable

{
   public class OpenAddressingTests
   {
       [Fact]
       public void LinearProbingTest()
       {
           // Arrange
           var hashTable = new MyHashTableList(100);
           var student1 = new Student("h21joste", "Johan Steinholtz");
           var student2 = new Student("h20jisjö", "Jim Sjögren");

           // Act
           hashTable.Add(student1.StudentId, student1);
           hashTable.Add(student2.StudentId, student2);

           // Assert
           Assert.NotNull(hashTable.Get(student1.StudentId));
           Assert.NotNull(hashTable.Get(student2.StudentId));
           Assert.Equal(student1.StudentId, hashTable.Get(student1.StudentId).StudentId);
           Assert.Equal(student1.Name, hashTable.Get(student1.StudentId).Name);
           Assert.Equal(student2.StudentId, hashTable.Get(student2.StudentId).StudentId);
           Assert.Equal(student2.Name, hashTable.Get(student2.StudentId).Name);
       }
       [Fact]
       public void QuadraticProbingTest()
       {
           // Arrange
           var hashTable = new MyHashTableArray(32);

           // Act
           for (int i = 0; i < 33; i++)
           {
               hashTable.Add($"Key{i}", new Student($"Name{i}", $"ID{i}"));
           }

           // Assert
           Assert.Equal(64, hashTable.GetCapacity());
       }
   }
}


*/