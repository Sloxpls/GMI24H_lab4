namespace Lab_4_HashTable
{
    public interface IHashTable
    {
        void Add(string key, Student student);
        Student Get(string key);
        void Remove(string key);
        void Clear();
        int Count { get; }
        IEnumerable<KeyValuePair<string, Student>> GetAllPairs();
        
        
        
        
    }
}