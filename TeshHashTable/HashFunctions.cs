/*
using Xunit;

namespace Lab_4_HashTable
{
    public class HashFunctionTests
    {
        [Fact]
        public void DJB2HashFunctionTest()
        {
            // Arrange
            var hashTable = new MyHashTableLinkedList(100);
            var student = new Student("h21joste", "Johan Steinholtz");

            // Act
            hashTable.Add(student.StudentId, student);
            var retrievedStudent = hashTable.Get(student.StudentId);

            // Assert
            Assert.NotNull(retrievedStudent);
            Assert.Equal(student.StudentId, retrievedStudent.StudentId);
            Assert.Equal(student.Name, retrievedStudent.Name);
        }

        [Fact]
        public void FNV1aHashFunctionTest()
        {
            // Arrange
            var hashTable = new MyHashTableList(100);
            var student = new Student("h21joste", "Johan Steinholtz");

            // Act
            hashTable.Add(student.StudentId, student);
            var retrievedStudent = hashTable.Get(student.StudentId);

            // Assert
            Assert.NotNull(retrievedStudent);
            Assert.Equal(student.StudentId, retrievedStudent.StudentId);
            Assert.Equal(student.Name, retrievedStudent.Name);
        }

        [Fact]
        public void SimpleModuloHashFunctionTest()
        {
            // Arrange
            var hashTable = new MyHashTableArray(100);
            var student = new Student("h21joste", "Johan Steinholtz");

            // Act
            hashTable.Add(student.StudentId, student);
            var retrievedStudent = hashTable.Get(student.StudentId);

            // Assert
            Assert.NotNull(retrievedStudent);
            Assert.Equal(student.StudentId, retrievedStudent.StudentId);
            Assert.Equal(student.Name, retrievedStudent.Name);
        }
    }
    
}*/