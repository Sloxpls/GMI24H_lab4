namespace Lab_4_HashTable
{
    public class MyHashTableArray : IHashTable
    {
        private const float LoadFactorThreshold = 0.75f;
        private KeyValuePair<string, Student>[] buckets;
        private int count;
        public MyHashTableArray(int initialCapacity)
        {
            if (initialCapacity <= 0)
            {
                throw new ArgumentException("Initial capacity must be greater than zero.", nameof(initialCapacity));
            }
            buckets = new KeyValuePair<string, Student>[initialCapacity];
        }
        public int Count => count;

        public int GetCapacity()
        {
            return buckets.Length;
        }
        private int QuadraticProbe(int startIndex, int attempt)
        {
            return (startIndex + (attempt * attempt)) % buckets.Length;
        }
        public void Add(string key, Student value)
        {
            int index = GetIndex(key);
            int attempt = 0;
            int startIndex = index;
            do
            {
                if (buckets[index].Key == null)
                {
                    buckets[index] = new KeyValuePair<string, Student>(key, value);
                    count++;
                    Console.WriteLine($"Added element: {key}. Count: {count}, Capacity: {buckets.Length}, Load Factor: {(float)count / buckets.Length}");
                    if ((float)(count + 1) / buckets.Length >= LoadFactorThreshold)
                    {
                        Console.WriteLine("Resizing quadratically...");
                        ResizeQuadratically();
                        Console.WriteLine($"New Capacity after quadratic resizing: {buckets.Length}");
                        index = GetIndex(key);
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
                    int index = GetIndex(bucket.Key);
                    buckets[index] = bucket;
                    count++;
                }
            }
        }

        public Student Get(string key)
        {
            int index = GetIndex(key);
            return buckets[index].Key == key ? buckets[index].Value : null;
        }

        public void Remove(string key)
        {
            int index = GetIndex(key);
            buckets[index] = default;
            count--;
        }

        private int GetIndex(string key)
        {
            if (buckets.Length == 0)
            {
                throw new InvalidOperationException("Hash table has zero capacity. Cannot calculate index.");
            }

            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode) % buckets.Length;
        }
        public void Clear()
        {
            Array.Clear(buckets, 0, buckets.Length);
            count = 0;
        }
    }
}
