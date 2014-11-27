using System.Collections.Generic;

namespace SparkiyEngine.Graphics.DirectX
{
    internal class PushPopManagement<T>
    {
        private readonly Stack<T> stack = new Stack<T>();
        private readonly Dictionary<string, T> map = new Dictionary<string, T>();

        public void Push(T item)
        {
            this.stack.Push(item);
        }

        public T Pop()
        {
            if (this.stack.Count == 0)
                return default(T);
            return this.stack.Pop();
        }

        public void Save(string key, T item)
        {
            this.map[key] = item;
            this.Push(item);
        }

        public T Load(string key)
        {
            if (!this.map.ContainsKey(key))
                return default(T);
            return this.map[key];
        }

        public void Clear()
        {
            this.stack.Clear();
            this.map.Clear();
        }
    }
}