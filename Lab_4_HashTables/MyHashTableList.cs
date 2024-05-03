namespace Lab_4_HashTable;
public class MyHashTableList<TKey, TValue> : IHashTable<TKey, TValue>
{
    private KeyValuePair<TKey, TValue>?[] buckets;
    private int size;
    private const double LoadFactorThreshold = 0.75;
    private const int InitialCapacity = 10;

    public MyHashTableList()
    {
        buckets = new KeyValuePair<TKey, TValue>?[InitialCapacity];
        size = 0;
    }

    public void Add(TKey key, TValue value)
    {
        EnsureCapacity();
        int index = GetBucketIndex(key);
        int attempt = 1;

        while (buckets[index] != null && !buckets[index].Value.Key.Equals(key))
        {
            index = (index + attempt * attempt) % buckets.Length; // Quadratic probing
            attempt++;
        }

        if (buckets[index] != null && buckets[index].Value.Key.Equals(key))
        {
            throw new ArgumentException("An element with the same key already exists.");
        }

        buckets[index] = new KeyValuePair<TKey, TValue>(key, value);
        size++;
    }

    public TValue Get(TKey key)
    {
        int index = GetBucketIndex(key);
        int attempt = 1;

        while (buckets[index] != null)
        {
            if (buckets[index].Value.Key.Equals(key))
            {
                return buckets[index].Value.Value;
            }
            index = (index + attempt * attempt) % buckets.Length; // Quadratic probing
            attempt++;
        }

        throw new KeyNotFoundException("Key not found.");
    }

    public void Remove(TKey key)
    {
        int index = GetBucketIndex(key);
        int attempt = 1;

        while (buckets[index] != null)
        {
            if (buckets[index].Value.Key.Equals(key))
            {
                buckets[index] = null;
                size--;
                HandleRehashing(index);
                return;
            }
            index = (index + attempt * attempt) % buckets.Length;
            attempt++;
        }

        throw new KeyNotFoundException("Key not found.");
    }

    private void Resize()
    {
        int newCapacity = buckets.Length * 2; // Increase by doubling the size
        var newBuckets = new KeyValuePair<TKey, TValue>?[newCapacity];

        foreach (var bucket in buckets)
        {
            if (bucket != null)
            {
                int index = GetBucketIndex(bucket.Value.Key, newCapacity); // Pass the new capacity
                int attempt = 1;
                while (newBuckets[index] != null)
                {
                    index = (index + attempt * attempt) % newCapacity; // Quadratic probing
                    attempt++;
                }
                newBuckets[index] = bucket;
            }
        }

        buckets = newBuckets;
    }

    private void EnsureCapacity()
    {
        if ((double)size / buckets.Length >= LoadFactorThreshold)
        {
            Resize();
        }
    }

    private void HandleRehashing(int startIndex)
    {
        int index = (startIndex + 1) % buckets.Length;
        while (buckets[index] != null)
        {
            var rehashedKey = buckets[index].Value.Key;
            var rehashedValue = buckets[index].Value.Value;
            buckets[index] = null;
            size--;
            Add(rehashedKey, rehashedValue);
            index = (index + 1) % buckets.Length;
        }
    }

    private int GetBucketIndex(TKey key, int capacity) // Add capacity parameter
    {
        int hash = key.GetHashCode();
        hash ^= (hash << 13) ^ (hash >> 17);
        return Math.Abs(hash % capacity); // Use the new capacity for calculation
    }

    private int GetBucketIndex(TKey key) // This overload is used for initial capacity
    {
        return GetBucketIndex(key, buckets.Length); // Call the other overload with the current capacity
    }
}



