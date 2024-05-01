namespace Lab_4_HashTable
{
    public class ResizingTests
    {
        [Fact]
        public void ResizeIncrementalTest()
        {
            // Arrange
            const int IncrementAmount = 10;
            var hashTable = new MyHashTableLinkedList(10);
            int initialCapacity = hashTable.GetCapacity();

            // Act
            for (int i = 0; i < IncrementAmount; i++)
            {
                hashTable.Add($"Key{i}", new Student($"Name{i}", $"ID{i}"));
            }

            // Assert
            Assert.Equal(initialCapacity + IncrementAmount, hashTable.GetCapacity());
        }
        [Fact]
        public void ResizeGeometricTest()
        {
            // Arrange
            var hashTable = new MyHashTableList(32);

            // Act
            for (int i = 0; i < 33; i++)
            {
                hashTable.Add($"Key{i}", new Student($"Name{i}", $"ID{i}"));
            }

            // Assert
            Assert.Equal(48, hashTable.GetCapacity());
        }
        [Fact]
        public void ResizeQuadraticallyTest()
        {
            // Arrange
            int initialCapacity = 32;
            var hashTable = new MyHashTableArray(initialCapacity);

            // Act
            for (int i = 0; i < 33; i++)
            {
                hashTable.Add($"Key{i}", new Student($"Name{i}", $"ID{i}"));
            }

            // Assert
            Assert.Equal(initialCapacity * 2, hashTable.GetCapacity());
        }
    }
}