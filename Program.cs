using System;
using System.Collections.Generic;
using System.Threading;

namespace laba22
{
    class Program
    {
        // Черги для двох процесорів
        static Queue<string> processor1Queue = new Queue<string>();
        static Queue<string> processor2Queue = new Queue<string>();

        // Об'єкт для блокування при взаємодії з чергами
        static object lockObject = new object();

        // Головний метод програми
        static void Main(string[] args)
        {
            // Симулюємо прибуття нових процесів
            for (int i = 1; i <= 10; i++)
            {
                ProcessArrival($"Процес {i}");
            }

            // Виводимо результати
            Console.WriteLine($"Розмiр Черги Процесора 1: {processor1Queue.Count}");
            Console.WriteLine($"Розмiр Черги Процесора 2: {processor2Queue.Count}");
        }

        // Метод для обробки прибуття нового процесу
        static void ProcessArrival(string process)
        {
            lock (lockObject)
            {
                // Перевіряємо, чи хоча б один процесор вільний або яка черга коротша
                if (!IsProcessorBusy(1) || processor1Queue.Count <= processor2Queue.Count)
                {
                    // Додаємо процес до черги процесора 1
                    processor1Queue.Enqueue(process);
                    Console.WriteLine($"Процес {process} доданий до Черги Процесора 1");
                }
                else
                {
                    // Додаємо процес до черги процесора 2
                    processor2Queue.Enqueue(process);
                    Console.WriteLine($"Процес {process} доданий до Черги Процесора 2");
                }
            }
        }

        // Метод для перевірки, чи процесор вільний
        static bool IsProcessorBusy(int processorNumber)
        {
            // Ваша логіка перевірки, чи процесор зайнятий
            // У цьому прикладі просто повертаємо випадкове значення
            Random random = new Random();
            return random.Next(2) == 1;
        }
    }
}
