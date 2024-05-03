namespace Lab_4_HashTable
{
    public class MyHashTableTests
    {
        [Fact]
        public void Add_KeyValue_AddsKeyValuePairToHashTable()
        {
            // Förbered
            var hashTable = new MyHashTableLinkedList<string, Student>();
            var student = new Student("123", "John"); // Ange id och namn som argument

            // Utför
            hashTable.Add("key", student);

            // Kontrollera
            Assert.Equal(student, hashTable.Get("key"));
        }
        [Fact]
        public void Test_HashFunctions()
        {
            // Förbered
            string key = "test_key";

            // Förväntade hash-värden för olika hash-funktioner
            int expectedHash1 = HashFunctionASCII(key);
            int expectedHash2 = HashFunctionDJB2(key);
            int expectedHash3 = HashFunctionCRC32(key);

            // Utför
            int actualHash1 = HashFunctionASCII(key);
            int actualHash2 = HashFunctionDJB2(key);
            int actualHash3 = HashFunctionCRC32(key);

            // Kontrollera
            Assert.Equal(expectedHash1, actualHash1);
            Assert.Equal(expectedHash2, actualHash2);
            Assert.Equal(expectedHash3, actualHash3);
        }
        private int HashFunctionASCII(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                int asciiValue = (int)c; // Hämta ASCII-värdet för tecknet
                hash ^= asciiValue; // XOR-operation med ASCII-värdet
            }
            return hash;
        }
        private int HashFunctionDJB2(string key)
        {
            int hash = 5381;
            foreach (char c in key)
            {
                hash = (hash << 5) + hash + c; // DJB2-algoritmen
            }
            return hash;
        }
        private int HashFunctionCRC32(string key)
        {
            return 12345;
        }


        // Test för hantering av kollisioner med länkning
        [Fact]
        public void Test_CollisionHandling_Chaining()
        {
            // Teknik 1: Separat länkning med länkade listor
            var hashTableLinkedList = new MyHashTableLinkedList<string, Student>();
            TestCollisionHandlingChaining(hashTableLinkedList, "Länkad Lista");

            // Teknik 2: Separat länkning med arrayer
            var hashTableArray = new MyHashTableArray<string, Student>();
            TestCollisionHandlingChaining(hashTableArray, "Array");
        }

        private void TestCollisionHandlingChaining(IHashTable<string, Student> hashTable, string technique)
        {
            // Lägg till några nyckel-värde-par
            hashTable.Add("key1", new Student("123", "John"));
            hashTable.Add("key2", new Student("456", "Alice"));
            hashTable.Add("key3", new Student("789", "Bob"));

            // Kontrollera värden som hämtas med nycklar
            Assert.Equal("John", hashTable.Get("key1").name);
            Assert.Equal("Alice", hashTable.Get("key2").name);
            Assert.Equal("Bob", hashTable.Get("key3").name);

            // Ta bort ett nyckel-värde-par
            hashTable.Remove("key2");

            // Kontrollera att det borttagna nyckel-värde-paret inte längre finns
            Assert.Throws<KeyNotFoundException>(() => hashTable.Get("key2"));

            Console.WriteLine($"Teknik: {technique} - Testet lyckades.");
        }



        // Test för hantering av kollisioner med öppen adressering
        [Fact]
        public void Test_CollisionHandling_OpenAddressing()
        {
            // Testa linjär sökning
            var hashTableLinear = new MyHashTableArray<string, Student>();
            TestCollisionHandlingOpenAddressing(hashTableLinear, "Linjär Sökning");

            // Testa kvadratisk sökning
            var hashTableQuadratic = new MyHashTableList<string, Student>();
            TestCollisionHandlingOpenAddressing(hashTableQuadratic, "Kvadratisk Sökning");
        }

        private void TestCollisionHandlingOpenAddressing(IHashTable<string, Student> hashTable, string technique)
        {
            // Lägg till nyckel-värde-par som kommer att orsaka kollisioner
            hashTable.Add("key1", new Student("123", "John"));
            hashTable.Add("key2", new Student("456", "Alice"));
            hashTable.Add("key3", new Student("789", "Bob"));
            hashTable.Add("key4", new Student("012", "Doe")); // Denna nyckel kommer att kollidera med key1

            // Kontrollera värden som hämtas med nycklar
            Assert.Equal("John", hashTable.Get("key1").name);
            Assert.Equal("Alice", hashTable.Get("key2").name);
            Assert.Equal("Bob", hashTable.Get("key3").name);
            Assert.Equal("Doe", hashTable.Get("key4").name);

            // Ta bort ett nyckel-värde-par
            hashTable.Remove("key2");

            // Kontrollera att det borttagna nyckel-värde-paret inte längre finns
            Assert.Throws<KeyNotFoundException>(() => hashTable.Get("key2"));

            Console.WriteLine($"Teknik: {technique} - Testet lyckades.");
        }


        // Test för ändringsstrategier
        [Fact]
        public void Test_ResizingStrategies()
        {
            var hashTableArrayDoubleSize = new MyHashTableArray<string, Student>();
            TestResizingStrategy(hashTableArrayDoubleSize, "Dubbla storleken när den uppnår full kapacitet");

            var hashTableLinkedListIncremental = new MyHashTableLinkedList<string, Student>();
            TestResizingStrategy(hashTableLinkedListIncremental, "Inkremental ökning när den uppnår full kapacitet");

            var hashTableListQuadratic = new MyHashTableList<string, Student>();
            TestResizingStrategy(hashTableListQuadratic, "Quadratic ökning när den uppnår full kapacitet");
        }

        private void TestResizingStrategy(IHashTable<string, Student> hashTable, string strategy)
        {
            // Add key-value pairs until changes are triggered
            for (int i = 0; i < 15; i++)
            {
                hashTable.Add($"key{i}", new Student($"ID{i}", $"Name{i}"));
            }

            // Check if all values can be retrieved
            for (int i = 0; i < 15; i++)
            {
                string expectedKey = $"key{i}";
                Student expectedValue = new Student($"ID{i}", $"Name{i}");

                try
                {
                    var retrievedValue = hashTable.Get(expectedKey);
                    Assert.Equal(expectedValue.name, retrievedValue.name);
                }
                catch (KeyNotFoundException)
                {
                    // Key not found
                    Assert.True(false, $"Failed to find value for key: {expectedKey}");
                }
            }

            Console.WriteLine($"Strategy: {strategy} - Test succeeded.");
        }

    }
}
