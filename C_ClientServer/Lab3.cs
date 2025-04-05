using System;
using System.Threading;

namespace MultiThread01
{
    class ThreadInfo
    {
        public static void Run()
        {
            Console.WriteLine("Информация о текущем потоке:");

            // Получаем текущий поток
            Thread currentThread = Thread.CurrentThread;

            // Получаем имя потока
            Console.WriteLine($"Имя потока: {currentThread.Name}");
            currentThread.Name = "Основной поток";
            Console.WriteLine($"Имя потока после изменения: {currentThread.Name}");

            Console.WriteLine($"Запущен ли поток: {currentThread.IsAlive}");
            Console.WriteLine($"Id потока: {currentThread.ManagedThreadId}");
            Console.WriteLine($"Приоритет потока: {currentThread.Priority}");
            Console.WriteLine($"Статус потока: {currentThread.ThreadState}");
        }
    }
}