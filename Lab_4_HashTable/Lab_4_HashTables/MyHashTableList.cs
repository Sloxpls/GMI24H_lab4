namespace Lab_4_HashTable;
public class MyHashTableList<TKey, TValue> : IHashTable<TKey, TValue> {
    private List<KeyValuePair<TKey, TValue>?> buckets;
    private int size;
    private const double LoadFactorThreshold = 0.75;
    private const int InitialCapacity = 10;

    public MyHashTableList() {
        buckets = new List<KeyValuePair<TKey, TValue>?>(new KeyValuePair<TKey, TValue>?[InitialCapacity]);
        size = 0;
    }
    public void Add(TKey key, TValue value) {
        EnsureCapacity();
        int index = GetBucketIndex(key);
        int attempt = 1;

        while (buckets[index] != null && !buckets[index].Value.Key.Equals(key)) {
            index = (index + attempt * attempt) % buckets.Count; // Quadratic probing
            attempt++;
        }

        if (buckets[index] != null && buckets[index].Value.Key.Equals(key)) {
            throw new ArgumentException("An element with the same key already exists.");
        }

        buckets[index] = new KeyValuePair<TKey, TValue>(key, value);
        size++;
    }
    public TValue Get(TKey key) {
        int index = GetBucketIndex(key);
        int attempt = 1;

        while (buckets[index] != null) {
            if (buckets[index].Value.Key.Equals(key)) {
                return buckets[index].Value.Value;
            }

            index = (index + attempt * attempt) % buckets.Count; // Quadratic probing
            attempt++;
        }

        throw new KeyNotFoundException("Key not found.");
    }

    public void Remove(TKey key) {
        int index = GetBucketIndex(key);
        int attempt = 1;
        while (buckets[index] != null) {
            if (buckets[index].Value.Key.Equals(key)) {
                buckets[index] = null;
                size--;
                HandleRehashing(index);
                return;
            }

            index = (index + attempt * attempt) % buckets.Count;
            attempt++;
        }

        throw new KeyNotFoundException("Key not found.");
    }
    private void Resize() {
        int newCapacity = buckets.Count * 2;
        List<KeyValuePair<TKey, TValue>?> newBuckets = new List<KeyValuePair<TKey, TValue>?>(new KeyValuePair<TKey, TValue>?[newCapacity]);
        size = 0;

        foreach (var bucket in buckets) {
            if (bucket != null)
            {
                Add(bucket.Value.Key, bucket.Value.Value);
            }
        }

        buckets = newBuckets;
    }
    private void EnsureCapacity() {
        if ((double)size / buckets.Count >= LoadFactorThreshold) {
            Resize();
        }
        
    }
    private void HandleRehashing(int startIndex) {
        int index = (startIndex + 1) % buckets.Count;
        while (buckets[index] != null) {
            var rehashedKey = buckets[index].Value.Key;
            var rehashedValue = buckets[index].Value.Value;
            buckets[index] = null;
            size--;
            Add(rehashedKey, rehashedValue);
            index = (index + 1) % buckets.Count;
        }
    }
    private int GetBucketIndex(TKey key) {
        int hash = key.GetHashCode();
        hash ^= (hash << 13) ^ (hash >> 17);
        return Math.Abs(hash % buckets.Count);
    }
}

