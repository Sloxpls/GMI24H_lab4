namespace Lab_4_HashTable
{
    public class MyHashTableList : IHashTable
    {
        private List<KeyValuePair<string, Student>>[] buckets;
        private int capacity;
        private int Count;
        private const float LoadFactorThreshold = 0.8f;
        private const double GrowthFactor = 1.5;

        public MyHashTableList(int size)
        {
            buckets = new List<KeyValuePair<string, Student>>[size];
            capacity = size;
            Count = 0;
        }
        private int LinearProbe(int startIndex, int attempt)
        {
            return (startIndex + attempt) % buckets.Length;
        }
        public void Add(string key, Student value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if ((float)Count / capacity >= LoadFactorThreshold)
            {
                ResizeGeometric();
            }

            int index = GetIndex(key);
            if (buckets[index] == null)
            {
                buckets[index] = new List<KeyValuePair<string, Student>>();
            }

            int attempt = 0;
            int startIndex = index;

            do
            {
                if (buckets[index] == null || buckets[index].Count == 0 || buckets[index][0].Key == key)
                {
                    if (buckets[index] == null)
                    {
                        buckets[index] = new List<KeyValuePair<string, Student>>();
                    }
                    buckets[index].Add(new KeyValuePair<string, Student>(key, value));
                    Count++; // Increment Count
                    return;
                }


                // Linear probing
                index = LinearProbe(startIndex, ++attempt);
            } while (index != startIndex);

            throw new InvalidOperationException("Hash table is full. Cannot add.");
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
        private int FNV1aHash(string key)
        {
            unchecked
            {
                const uint FnvPrime = 16777619;
                const uint FnvOffsetBasis = 2166136261;

                uint hash = FnvOffsetBasis;
                foreach (char c in key)
                {
                    hash ^= c;
                    hash *= FnvPrime;
                }

                return (int)(hash % buckets.Length);
            }
        }
        private int GetIndex(string key)
        {
            return FNV1aHash(key);
        }
        private void ResizeGeometric()
        {
            int newCapacity = (int)(capacity * GrowthFactor);
            Console.WriteLine($"New capacity before adjustment: {newCapacity}");

           
            if (newCapacity % 2 != 0)
            {
                newCapacity++;
                Console.WriteLine($"Adjusted new capacity to ensure it's even: {newCapacity}");
            }

            var newBuckets = new List<KeyValuePair<string, Student>>[newCapacity];

            foreach (var bucket in buckets.Where(b => b != null))
            {
                foreach (var pair in bucket)
                {
                    int newIndex = GetIndex(pair.Key);
                    if (newBuckets[newIndex] == null)
                    {
                        newBuckets[newIndex] = new List<KeyValuePair<string, Student>>();
                    }

                    newBuckets[newIndex].Add(pair);
                }
            }

            buckets = newBuckets;
            capacity = newCapacity;
            Console.WriteLine($"Capacity after resize: {capacity}");
        }




    }
}