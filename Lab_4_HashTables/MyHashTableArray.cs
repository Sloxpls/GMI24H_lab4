using System;
using System.Collections.Generic;

namespace Lab_4_HashTable
{
    public class MyHashTableArray : IHashTable
    {
        private const float LoadFactorThreshold = 0.75f;
        private KeyValuePair<string, Student>[] buckets;
        private int count;

        public MyHashTableArray() : this(100) { }

        public MyHashTableArray(int initialCapacity)
        {
            if (initialCapacity <= 0)
            {
                throw new ArgumentException("Initial capacity must be greater than zero.", nameof(initialCapacity));
            }
            buckets = new KeyValuePair<string, Student>[initialCapacity];
        }
        
        public IEnumerable<KeyValuePair<string, Student>> GetAllPairs()
        {
            foreach (var pair in buckets)
            {
                if (pair.Key != null)
                {
                    yield return pair;
                }
            }
        }
        
        public int Count => count;

        public void Add(string key, Student value)
        {
            int index = HashFunction(key);
            int attempt = 0;
            int startIndex = index;
            do
            {
                if (buckets[index].Key == null)
                {
                    buckets[index] = new KeyValuePair<string, Student>(key, value);
                    count++;
                    if ((float)(count + 1) / buckets.Length >= LoadFactorThreshold)
                    {
                        Console.WriteLine("Resizing quadratically...");
                        ResizeQuadratically();
                        Console.WriteLine($"New Capacity after quadratic resizing: {buckets.Length}");
                        index = HashFunction(key);
                        startIndex = index;
                        attempt = 0;
                    }
                    return;
                }
                index = QuadraticProbe(startIndex, ++attempt);
            } while (index != startIndex);
            throw new InvalidOperationException("Hash table is full. Cannot add.");
        }
        
        public void ResizeQuadratically()
        {
            int newCapacity = buckets.Length * 2;
            var oldBuckets = buckets;
            buckets = new KeyValuePair<string, Student>[newCapacity];
            count = 0;
            foreach (var bucket in oldBuckets)
            {
                if (bucket.Key != null)
                {
                    int index = HashFunction(bucket.Key);
                    buckets[index] = bucket;
                    count++;
                }
            }
        }
        
        public Student Get(string key)
        {
            int index = HashFunction(key);
            return buckets[index].Key == key ? buckets[index].Value : null;
        }
        
        public void Remove(string key)
        {
            int index = HashFunction(key);
            buckets[index] = default;
            count--;
        }
        
        private int QuadraticProbe(int startIndex, int attempt)
        {
            return (startIndex + (attempt * attempt)) % buckets.Length;
        }
        
        private int HashFunction(string key)
        {
            // Implement your own hash function here
            // For demonstration purposes, a simple mod function is used
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }
        
        public void Clear()
        {
            Array.Clear(buckets, 0, buckets.Length);
            count = 0;
        }
    }
}
