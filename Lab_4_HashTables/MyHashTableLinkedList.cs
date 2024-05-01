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

    private Node[] buckets;
    private int size;

    public MyHashTableLinkedList(int size = 100){
        this.size = size;
        buckets = new Node[size];
    }

    private static ulong Djb2(string str) {
        ulong hash = 5381; 
        foreach (char c in str) {
            hash = ((hash << 5) + hash) + c; 
        }
        return hash;
    }

    private int GetIndex(string key) {
        ulong hash = Djb2(key);
        return (int)(hash % (ulong)size);
    }

    public void Add(string key, object value) {
        int index = GetIndex(key);
        Node head = buckets[index];

        // Check if key exists and update value
        while (head != null)
        {
            if (head.Key.Equals(key))
            {
                head.Value = value;
                return;
            }
            head = head.Next;
        }

        // Insert new node at the head of the list in the bucket
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

        return null; // Key not found
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
                    prev.Next = head.Next; // Bypass the node
                }
                else
                {
                    buckets[index] = head.Next; // Remove head node
                }
                return;
            }
            prev = head;
            head = head.Next;
        }
    }
}
