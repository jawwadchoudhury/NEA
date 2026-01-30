using System;

namespace maze_nea
{
    public class DynamicStack<T> // Using Generics to allow for a dynamic stack of any type
    {
        private T[] elements;
        private int count;
        private int capacity;
        public int getCount()
        {
            return count;
        }

        public DynamicStack(int initialCapacity = 1)
        {
            capacity = initialCapacity;
            elements = new T[capacity];
            count = 0;
        }

        public void Push(T item)
        {
            if (count == capacity)
            {
                capacity *= 2; // Using array doubling to reduce the resize frequency, reducing copy operations and ulitmately average time complexity to O(1)
                T[] newElements = new T[capacity];
                Array.Copy(elements, newElements, count);
                elements = newElements;
            }

            elements[count] = item;
            count++;
        }

        public T Pop()
        {
            if (count == 0) throw new InvalidOperationException("Stack is empty.");
            count--;
            T item = elements[count];
            elements[count] = default(T); // Setting this index to default value to prevent memory leaks, and allows the garbage collector to reclaim memory
            return item;
        }

        public T Peek()
        {
            if (count == 0) throw new InvalidOperationException("Stack is empty.");
            return elements[count - 1];
        }
    }
}
