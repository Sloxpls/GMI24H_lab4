/*
namespace Lab_4_HashTable
{
    public class ChainingCollisionTests
    {
        [Fact]
        public void LinkedListChainingCollisionTest()
        {
            // Arrange
            var hashTable = new MyHashTableLinkedList(100);
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
        public void ListChainingCollisionTest()
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
        public void ArrayChainingCollisionTest()
        {
            // Arrange
            var hashTable = new MyHashTableArray(100);
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
    }
}*/
