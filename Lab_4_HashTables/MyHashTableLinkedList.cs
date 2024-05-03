namespace Lab_4_HashTable;

// <TKey, TValue> används för att göra klassen generisk
// alltså kan ta emot olika variabel typer
public class MyHashTableLinkedList<TKey, TValue> : IHashTable<TKey, TValue> {
    
    private LinkedList<KeyValuePair<TKey, TValue>>[] buckets;
    private int size;
    private const double LoadFactorThreshold = 0.75; // används för rezising
    private const int InitialCapacity = 10;

    
    //skappar en linktlist vilkets element är linkt lists
    // med en initsial kapasitet av 10
    public MyHashTableLinkedList() {
        buckets = new LinkedList<KeyValuePair<TKey, TValue>>[InitialCapacity];
        for (int i = 0; i < InitialCapacity; i++) {
            buckets[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }
        size = 0;
    }

    public void Add(TKey key, TValue value) {
        if ((double)size / buckets.Length > LoadFactorThreshold)// om listan är mer än 75% full Resize 
        {
            Resize();
        }
        
        int index = GetBucketIndex(key);
        buckets[index].AddLast(new KeyValuePair<TKey, TValue>(key, value)); // lägar alltid till sista noden / ny node i slutet
        size++;
    }

    public TValue Get(TKey key) {
        int index = GetBucketIndex(key);
        foreach (var pair in buckets[index]) {
            if (pair.Key.Equals(key)) {
                return pair.Value;
            }
        }
        throw new KeyNotFoundException("Key not found.");
    }

    public void Remove(TKey key) {
        int index = GetBucketIndex(key);
        foreach (var pair in buckets[index]) {
            if (pair.Key.Equals(key)) {
                buckets[index].Remove(pair);
                size--;
                return;
            }
        }
        throw new KeyNotFoundException("Key not found.");
    }

    private void Resize()
    {
        int newCapacity = buckets.Length + 10; // Incremental increase by 10
        var newBuckets = new List<LinkedList<KeyValuePair<TKey, TValue>>>(newCapacity);
        for (int i = 0; i < newCapacity; i++)
        {
            newBuckets.Add(new LinkedList<KeyValuePair<TKey, TValue>>());
        }

        foreach (var bucket in buckets)
        {
            foreach (var pair in bucket)
            {
                int newIndex = GetBucketIndex(pair.Key, newCapacity);
                newBuckets[newIndex].AddLast(pair);
            }
        }

        buckets = newBuckets.ToArray();
    }



    // hash funktion, dbj2 
    private int GetBucketIndex(TKey key, int capacity = -1) {
        ulong hash = 5381;
        
        foreach (char c in key.ToString()) {
            hash = ((hash << 5) + hash) + (ulong)c; 
        }
        if (capacity == -1)
        {
            capacity = buckets.Length;
        }
        return (int)(hash % (ulong)capacity);
    }
}
