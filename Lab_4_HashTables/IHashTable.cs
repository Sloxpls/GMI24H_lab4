using Lab_4_HashTable;

public interface IHashTable {
    void Add(string key, object value);
    object Get(string key);
    void Remove(string key);
}
