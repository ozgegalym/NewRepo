using System;

namespace IndexerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new SquaredArray(5);

            // Устанавливаем значения через индексатор
            array[0] = 2;
            array[1] = 3;
            array[2] = 4;
            array[3] = 5;
            array[4] = 6;

            // Получаем и выводим значения через индексатор
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Значение элемента {i} = {array[i]}");
            }
        }
    }

    class SquaredArray
    {
        private int[] array;

        public SquaredArray(int size)
        {
            array = new int[size];
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return array[index];
            }
            set
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                array[index] = value * value;
            }
        }
    }
}
