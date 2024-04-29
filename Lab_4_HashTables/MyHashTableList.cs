using System;
using System.Collections.Generic;

namespace Lab_4_HashTable
{
    public class MyHashTableList : IHashTable
    {
        private List<KeyValuePair<string, Student>>[] buckets;

        public MyHashTableList(int size)
        {
            buckets = new List<KeyValuePair<string, Student>>[size];
        }

        public void Add(string key, Student value)
        {
            int index = GetIndex(key);
            if (buckets[index] == null)
                buckets[index] = new List<KeyValuePair<string, Student>>();

            buckets[index].Add(new KeyValuePair<string, Student>(key, value));
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
                var item = bucket.Find(kvp => kvp.Key == key);
                if (item.Key == key)
                    bucket.Remove(item);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                    buckets[i].Clear();
            }
        }

        private int GetIndex(string key)
        {
            // Hash Function Simple modulo with array length
            return Math.Abs(key.GetHashCode() % buckets.Length);
        }
    }
}