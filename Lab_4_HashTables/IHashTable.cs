namespace Lab_4_HashTable
{
    public interface IHashTable
    {
        void Add(string key, Student value);
        Student Get(string key);
        void Remove(string key);
    }
}