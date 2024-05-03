namespace Lab_4_HashTable;
public class MyHashTableArray<TKey, TValue> : IHashTable<TKey, TValue> {
    private KeyValuePair<TKey, TValue>?[] buckets; // ? används för att säga att elementen kan vara null
    private int size;
    private const double LoadFactorThreshold = 0.75;
    private const int InitialCapacity = 10;

    // construktor som skapar en ny hash tabell med en initial kapacitet på 10
    public MyHashTableArray() {
        buckets = new KeyValuePair<TKey, TValue>?[InitialCapacity];
        size = 0;
    }

    // lägger till ett nyckel värde par i hashtabellen om nyckeln redan finns kastas ett undantag
    public void Add(TKey key, TValue value) {
        if ((double)size / buckets.Length >= LoadFactorThreshold) {
            Resize();// utöka storleken på arrayen vid behov
        }

        int index = GetBucketIndex(key);
	while (buckets[index] != null) {
	    try
	    {
		if (buckets[index].Value.Key.Equals(key)) {
		    throw new ArgumentException("An element with the same key already exists.");
		}
	    }
	    catch (ArgumentException ex)
	    {
		// Handle the exception gracefully (e.g., log it)
		Console.WriteLine($"An error occurred: {ex.Message}");
		// Optionally, you can choose to break out of the loop here
		break;
	    }
	    index = (index + 1) % buckets.Length; //linear probing
	}


        buckets[index] = new KeyValuePair<TKey, TValue>(key, value);
        size++;
    }
    // hämtar värde baserat på nyckeln, kastar exception om nyckeln inte hittas
    public TValue Get(TKey key) {
        int index = GetBucketIndex(key);
        while (buckets[index] != null) {
            if (buckets[index].Value.Key.Equals(key)) {
                return buckets[index].Value.Value;
            }
            index = (index + 1) % buckets.Length; 
        }

        throw new KeyNotFoundException("Key not found.");
    }
//radera ett element baserat på nyckel och gör omhashning av efterföljande element 
// så inget index lämnas tom och den slutar leta
    public void Remove(TKey key) {
        int index = GetBucketIndex(key);
        while (buckets[index] != null) {
            if (buckets[index].Value.Key.Equals(key)) {
                buckets[index] = null; 
                size--;
                int nextIndex = (index + 1) % buckets.Length;
                while (buckets[nextIndex] != null) {
                    var rehashedKey = buckets[nextIndex].Value.Key;
                    var rehashedValue = buckets[nextIndex].Value.Value;
                    buckets[nextIndex] = null;
                    size--;
                    Add(rehashedKey, rehashedValue);
                    nextIndex = (nextIndex + 1) % buckets.Length;
                }
                return;
            }
            index = (index + 1) % buckets.Length;
        }

        throw new KeyNotFoundException("Key not found.");
    }

    private void Resize()
    {
        int newCapacity = buckets.Length * 2;
        var newBuckets = new KeyValuePair<TKey, TValue>?[newCapacity];
        size = 0;

        foreach (var bucket in buckets)
        {
            if (bucket != null)
            {
                int index = GetBucketIndex(bucket.Value.Key, newCapacity);
                while (newBuckets[index] != null)
                {
                    index = (index + 1) % newCapacity;
                }
                newBuckets[index] = bucket;
                size++;
            }
        }
        buckets = newBuckets;
    }
    // hash funktion använder sig av modulo eller rest för att få index
    private int GetBucketIndex(TKey key, int capacity = -1) {
        int hash = key.GetHashCode();
        if (capacity == -1) capacity = buckets.Length;
        return Math.Abs(hash % capacity);
    }
}
