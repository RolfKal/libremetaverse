using System;
using System.Collections.Generic;

namespace Simian
{
    public class DoubleDictionary<TKey1, TKey2, TValue>
    {
        Dictionary<TKey1, TValue> Dictionary1;
        Dictionary<TKey2, TValue> Dictionary2;
        object syncObject = new object();

        public DoubleDictionary()
        {
            Dictionary1 = new Dictionary<TKey1,TValue>();
            Dictionary2 = new Dictionary<TKey2,TValue>();
        }

        public DoubleDictionary(int capacity)
        {
            Dictionary1 = new Dictionary<TKey1, TValue>(capacity);
            Dictionary2 = new Dictionary<TKey2, TValue>(capacity);
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            lock (syncObject)
            {
                Dictionary1.Add(key1, value);
                Dictionary2.Add(key2, value);
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2)
        {
            lock (syncObject)
            {
                Dictionary1.Remove(key1);
                return Dictionary2.Remove(key2);
            }
        }

        public void Clear()
        {
            lock (syncObject)
            {
                Dictionary1.Clear();
                Dictionary2.Clear();
            }
        }

        public int Count
        {
            get { return Dictionary1.Count; }
        }

        public bool TryGetValue(TKey1 key, out TValue value)
        {
            return Dictionary1.TryGetValue(key, out value);
        }

        public bool TryGetValue(TKey2 key, out TValue value)
        {
            return Dictionary2.TryGetValue(key, out value);
        }

        public void ForEach(Action<TValue> action)
        {
            lock (syncObject)
            {
                foreach (TValue value in Dictionary1.Values)
                    action(value);
            }
        }
    }
}
