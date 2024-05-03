namespace Lab_4_HashTable;
public interface IHashTable<TKey, TValue> {
    void Add(TKey key, TValue value);
    TValue Get(TKey key);
    void Remove(TKey key);
    
}