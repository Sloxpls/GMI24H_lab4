public class MyHashTableLinkedList : IHashTable
{
    private class Node {
        public string Key { get; set; }
        public object Value { get; set; }
        public Node Next { get; set; }
        public Node(string key, object value) {
            Key = key;
            Value = value;
        }
    }

    private readonly int size;
    private Node[] buckets;

    public MyHashTableLinkedList(int size = 100) {
        this.size = size;
        buckets = new Node[size];
    }

    private int GetIndex(string key) {
        int hash = key.GetHashCode();
        return Math.Abs(hash % size);
    }

    public void Add(string key, object value) {
        int index = GetIndex(key);
        Node head = buckets[index];

        while (head != null) {
            if (head.Key.Equals(key))
            {
                head.Value = value; // Update existing key
                return;
            }
            head = head.Next;
        }

        Node newNode = new Node(key, value);
        newNode.Next = buckets[index];
        buckets[index] = newNode;
    }

    public object Get(string key) {
        int index = GetIndex(key);
        Node head = buckets[index];

        while (head != null)
        {
            if (head.Key.Equals(key))
            {
                return head.Value;
            }
            head = head.Next;
        }

        return null;
    }

    public void Remove(string key) {
        int index = GetIndex(key);
        Node head = buckets[index];
        Node prev = null;

        while (head != null)
        {
            if (head.Key.Equals(key))
            {
                if (prev != null)
                {
                    prev.Next = head.Next;
                }
                else
                {
                    buckets[index] = head.Next;
                }
                return;
            }
            prev = head;
            head = head.Next;
        }
    }
}