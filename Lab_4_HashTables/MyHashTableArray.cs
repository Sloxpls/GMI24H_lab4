using System;
using System.Collections.Generic;

namespace Lab_4_HashTable
{
    public class MyHashTableArray : IHashTable
    {
        private const float LoadFactorThreshold = 0.8f; // Load factor threshold set to 80%
        private const int InitialCapacity = 16;

        private KeyValuePair<string, Student>[] buckets;
        private readonly object lockObject = new object(); // Lock object for synchronization
        public int Count { get; private set; }

        public MyHashTableArray()
        {
            buckets = new KeyValuePair<string, Student>[InitialCapacity];
        }

        public int GetCapacity()
        {
            return buckets.Length;
        }

        public void Add(string key, Student value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty.");

            // Check load factor
            if ((float)Count / buckets.Length >= 0.5f)
            {
                Resize();
            }

            int index = GetIndex(key);
            int startIndex = index;
            int attempt = 1;

            do
            {
                if (buckets[index].Key == null || buckets[index].Key == key)
                {
                    buckets[index] = new KeyValuePair<string, Student>(key, value);
                    Count++;
                    return;
                }

                // Quadratic probing
                index = (startIndex + attempt * attempt) % buckets.Length;
                attempt++;
            } while (index != startIndex);

            throw new InvalidOperationException("Hash table is full. Cannot add.");
        }
        public Student Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty.");

            lock (lockObject) // Lock to ensure thread safety
            {
                int index = GetIndex(key);
                return buckets[index].Key == key ? buckets[index].Value : null;
            }
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty.");

            lock (lockObject) // Lock to ensure thread safety
            {
                int index = GetIndex(key);
                if (buckets[index].Key == key)
                {
                    buckets[index] = new KeyValuePair<string, Student>();
                    Count--;
                    return;
                }
            }

            throw new KeyNotFoundException($"Key '{key}' not found.");
        }

        private int GetIndex(string key)
        {
            // Simple hash function to calculate index
            return Math.Abs(key.GetHashCode() % buckets.Length);
        }

        public void Resize()
        {
            int newCapacity = buckets.Length * 2;
            var oldBuckets = buckets;
            buckets = new KeyValuePair<string, Student>[newCapacity];
            Count = 0; // Reset Count before rehashing

            foreach (var bucket in oldBuckets)
            {
                if (bucket.Key != null)
                {
                    int index = GetIndex(bucket.Key);
                    buckets[index] = new KeyValuePair<string, Student>(bucket.Key, bucket.Value);
                    Count++;
                }
            }
        }
        public void Clear()
        {
            lock (lockObject) // Lock to ensure thread safety
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = default; // Reset each bucket to default value
                }
                Count = 0;
                Console.WriteLine("Hash table cleared.");
            }
        }
    }
}
