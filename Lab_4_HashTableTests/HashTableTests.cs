using System;
using Xunit;
namespace Lab_4_HashTable
{
    public class HashTableTests : IDisposable
    {
        public const float LoadFactorThreshold = 0.75f;
        private readonly MyHashTableArray _arrayHashTable;
        private readonly MyHashTableList _listHashTable;
        private readonly MyHashTableLinkedList _linkedListHashTable;

        public HashTableTests()
        {
            _arrayHashTable = new MyHashTableArray();
            _listHashTable = new MyHashTableList(100);
            _linkedListHashTable = new MyHashTableLinkedList(100);
        }

        public void Dispose()
        {
            // Clean up after each test
            _arrayHashTable.Clear();
            _listHashTable.Clear();
            _linkedListHashTable.Clear();

            // Add debug statement
            Console.WriteLine("Hash tables cleared.");
        }

        private void ClearHashTables()
        {
            _arrayHashTable.Clear();
            _listHashTable.Clear();
            _linkedListHashTable.Clear();
        }

        // Interface Test.
        [Fact]
        public void TestHashTableInterface()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableList(100);
            var student = new Student("h21joste", "Johan Steinholtz");

            // Act
            hashTable.Add("h21joste", student);
            var result = hashTable.Get("h21joste");

            // Assert
            Assert.Equal(student, result);
        }

        // Use interface to test hashtables.
        [Fact]
        public void TestArrayHashTable()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Act & Assert
            TestHashTable(_arrayHashTable);
        }

        [Fact]
        public void TestListHashTable()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Act & Assert
            TestHashTable(_listHashTable);
        }

        [Fact]
        public void TestLinkedListHashTable()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Act & Assert
            TestHashTable(_linkedListHashTable);
        }

        private void TestHashTable(IHashTable hashTable)
        {
            // Arrange
            var student1 = new Student("h21joste", "Johan Steinholtz");
            var student2 = new Student("h20jisjö", "Jim Sjögren");

            // Act
            hashTable.Add(student1.StudentId, student1);
            hashTable.Add(student2.StudentId, student2);

            // Assert
            Assert.Equal(student1, hashTable.Get("h21joste"));
            Assert.Equal(student2, hashTable.Get("h20jisjö"));

            // Act
            hashTable.Remove("h21joste");

            // Assert
            Assert.Null(hashTable.Get("h21joste")); // Ensure removed item is null
            Assert.Equal(student2, hashTable.Get("h20jisjö"));
        }


        //Test Student Class.
        [Fact]
        public void TestStudentClass()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var student = new Student("h21joste", "Johan Steinholtz");

            // Assert
            Assert.Equal("h21joste", student.StudentId);
            Assert.Equal("Johan Steinholtz", student.Name);
        }

        // Test hash collision using DJB2.(method currently used in LinkedList.).
        [Fact]
        public void TestDJB2HashCollision()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableLinkedList(100);
            var student1 = new Student("h21joste", "Johan Steinholtz");
            var student2 = new Student("h22joste", "Johan Steinholtz"); // Same hash as student1
            var student3 = new Student("h23joste", "Johan Steinholtz"); // Same hash as student1 and student2

            // Act
            hashTable.Add(student1.StudentId, student1);
            hashTable.Add(student2.StudentId, student2);
            hashTable.Add(student3.StudentId, student3);

            // Assert
            Assert.Equal(student1, hashTable.Get("h21joste"));
            Assert.Equal(student2, hashTable.Get("h22joste"));
            Assert.Equal(student3, hashTable.Get("h23joste"));
        }

        // Test2 hash collision using DJB2.(method currently used in LinkedList.).
        [Fact]
        public void TestCollisionHandlingWithDJB2Hash()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableLinkedList(100); // Using linked list bucket implementation
            var student1 = new Student("h21joste", "Johan Steinholtz");
            var student2 = new Student("h22joste", "Johan Steinholtz"); // Same hash as student1

            // Act
            hashTable.Add(student1.StudentId, student1);
            hashTable.Add(student2.StudentId, student2);

            // Assert
            Assert.Equal(student1, hashTable.Get("h21joste"));
            Assert.Equal(student2, hashTable.Get("h22joste"));
        }

        // Additional tests...

        [Fact]
        public void TestEmptyStringAsKey()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableArray();

            // Act
            Action addEmptyKey = () => hashTable.Add("", new Student("h21joste", "Johan Steinholtz"));

            // Assert
            Assert.Throws<ArgumentException>(addEmptyKey);
        }

        [Fact]
        public void TestRemoveNonExistentKey()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableArray();

            // Act
            Action removeNonExistentKey = () => hashTable.Remove("nonexistent_key");

            // Assert
            Assert.Throws<KeyNotFoundException>(removeNonExistentKey);
        }

        [Fact]
        public void TestRemoveKey()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableArray();
            var student = new Student("h21joste", "Johan Steinholtz");
            hashTable.Add(student.StudentId, student);

            // Act
            hashTable.Remove(student.StudentId);

            // Assert
            Assert.Null(hashTable.Get(student.StudentId));
        }

        [Fact]
        public void TestGetNonExistentKey()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableArray();
            var student = new Student("h21joste", "Johan Steinholtz");

            // Act
            var result = hashTable.Get("nonexistentkey");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestAddAndGet()
        {
            ClearHashTables(); // Clear hash tables before each test
            // Arrange
            var hashTable = new MyHashTableList(100); // Assuming MyHashTableList constructor takes size parameter
            var student = new Student("h21joste", "Johan Steinholtz");

            // Act
            hashTable.Add(student.StudentId, student);
            var retrievedStudent = hashTable.Get(student.StudentId);

            // Assert
            Assert.NotNull(retrievedStudent);
            Assert.Equal(student, retrievedStudent);
        }

        [Fact]
        public void Resize_DoublesCapacity()
        {
            // Arrange
            var hashTable = new MyHashTableArray();
            int initialCapacity = hashTable.GetCapacity();

            // Act
            // Clear the hash table
            hashTable.Clear();
            // Add elements until resizing occurs multiple times
            int addedElements = 0;
            while (addedElements < initialCapacity * 2)
            {
                hashTable.Add($"Key{addedElements}", new Student($"Name{addedElements}", $"ID{addedElements}"));
                addedElements++;
            }

            // Assert
            // Check if the capacity has doubled after resizing each time it reaches 50% full
            Assert.True(hashTable.GetCapacity() >= initialCapacity * 2);
        }



    }
}
