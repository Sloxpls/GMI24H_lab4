using System;
using System.Collections.Generic;

namespace Lab_4_HashTable
{
    public class MyHashTableLinkedList : IHashTable
    {
        public MyHashTableLinkedList() : this(100) { }
        
        private LinkedList<KeyValuePair<string, Student>>[] buckets;
        private int capacity;
        private const float LoadFactorThreshold = 0.8f;
        private const int IncrementAmount = 10;

        public MyHashTableLinkedList(int size)
        {
            buckets = new LinkedList<KeyValuePair<string, Student>>[size];
            capacity = size;
        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (var bucket in buckets)
                {
                    if (bucket != null)
                    {
                        count += bucket.Count;
                    }
                }
                return count;
            }
        }

        public void Add(string key, Student value)
        {
            if ((float)Count / capacity >= LoadFactorThreshold)
            {
                ResizeIncremental();
            }

            int index = GetIndex(key);
            if (buckets[index] == null)
                buckets[index] = new LinkedList<KeyValuePair<string, Student>>();

            buckets[index].AddLast(new KeyValuePair<string, Student>(key, value));
        }

        public Student Get(string key)
        {
            int index = GetIndex(key);
            var bucket = buckets[index];

            if (bucket != null)
            {
                foreach (var pair in bucket)
                {
                    if (pair.Key == key)
                        return pair.Value;
                }
            }
            return null;
        }
        
        public void Remove(string key)
        {
            int index = GetIndex(key);
            var bucket = buckets[index];

            if (bucket != null)
            {
                var node = bucket.First;
                while (node != null)
                {
                    if (node.Value.Key == key)
                    {
                        bucket.Remove(node);
                        break;
                    }

                    node = node.Next;
                }
            }
        }

        public IEnumerable<KeyValuePair<string, Student>> GetAllPairs()
        {
            foreach (var bucket in buckets)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        yield return pair;
                    }
                }
            }
        }

        public int GetIndex(string key)
        {
            // Implement your own hash function here
            // For demonstration purposes, a simple mod function is used
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }

        public void Clear()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                    buckets[i].Clear();
            }
        }

        private void ResizeIncremental()
        {
            int newCapacity = capacity + IncrementAmount;
            var newBuckets = new LinkedList<KeyValuePair<string, Student>>[newCapacity];

            foreach (var bucket in buckets.Where(b => b != null))
            {
                foreach (var pair in bucket)
                {
                    int newIndex = GetIndex(pair.Key);
                    if (newBuckets[newIndex] == null)
                        newBuckets[newIndex] = new LinkedList<KeyValuePair<string, Student>>();

                    newBuckets[newIndex].AddLast(pair);
                }
            }
            buckets = newBuckets;
            capacity = newCapacity;
        }
    }
}
