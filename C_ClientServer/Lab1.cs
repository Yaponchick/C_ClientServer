using System;
using System.Threading;

namespace MultiThread01
{
    class ProgrammLab01
    {
        public static void Run()
        {
            Console.WriteLine("Основной поток начал работу");

            // Создаем и запускаем потоки с помощью лямбда-выражений
            Thread thread1 = new Thread(() => PrintNumbers(5, 15));
            Thread thread2 = new Thread(() => PrintNumbers(10, 20));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Основной поток завершил работу");
        }

        static void PrintNumbers(int start, int end)
        {
            // Используем имя потока в выводе
            Console.WriteLine($"Поток {Thread.CurrentThread.Name ?? "Без имени"} начал работу");

            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.Name ?? "Без имени"}: {i}");
                Thread.Sleep(100); // Имитация работы
            }

            Console.WriteLine($"Поток {Thread.CurrentThread.Name ?? "Без имени"} завершил работу");
        }
    }
}