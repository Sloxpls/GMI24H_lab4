using System;
using System.Collections.Generic;

namespace Lab_4_HashTable
{
    public class MyHashTableLinkedList : IHashTable
    {
        private LinkedList<KeyValuePair<string, Student>>[] buckets;

        public MyHashTableLinkedList(int size)
        {
            buckets = new LinkedList<KeyValuePair<string, Student>>[size];
        }

        public void Add(string key, Student value)
        {
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

        private int GetIndex(string key)
        {
            // Choose hash function
            return Math.Abs(DJB2Hash(key) % buckets.Length);
        }

        private int DJB2Hash(string key)
        {
            uint hash = 5381;
            foreach (char c in key)
            {
                hash = ((hash << 5) + hash) + c; // hash * 33 + c
            }

            return (int)(hash % buckets.Length);
        }

        public void Clear()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                    buckets[i].Clear();
            }
        }
    }
}
