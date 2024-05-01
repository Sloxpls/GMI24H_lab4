namespace Lab_4_HashTable
{
    public class MyHashTableLinkedList : IHashTable
    {
        private LinkedList<KeyValuePair<string, Student>>[] buckets;
        private int capacity;
        private int Count;
        private const float LoadFactorThreshold = 0.8f;
        private const int IncrementAmount = 10;
        public MyHashTableLinkedList(int size)
        {
            buckets = new LinkedList<KeyValuePair<string, Student>>[size];
            capacity = size;
            Count = 0;
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
            Count++;
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
        public int GetCapacity()
        {
            return capacity;
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
            return Math.Abs(DJB2Hash(key) % buckets.Length);
        }

        private int DJB2Hash(string key)
        {
            uint hash = 5381;
            foreach (char c in key)
            {
                hash = ((hash << 5) + hash) + c;
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
