using System;
using System.Threading;

namespace MultiThread01
{
    class Lab02
    {
        static Thread? thread1;

        public static void Run()
        {
            Console.WriteLine("Выберите эксперимент:");
            Console.WriteLine("1 - Эксперимент 1: Без задержки между запусками");
            Console.WriteLine("2 - Эксперимент 2: С задержкой 1с между запусками");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunExperiment1();
                    break;
                case "2":
                    RunExperiment2();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Программа завершена.");
                    return;
            }

            Console.WriteLine("\nОсновной поток завершил работу");
        }

        static void RunExperiment1()
        {
            Console.WriteLine("Основной поток начал работу");

            // Первый эксперимент - без задержки
            Console.WriteLine("\nЭксперимент 1: Без задержки между запусками");
            thread1 = new Thread(PrintNumbersFirst);
            Thread thread2 = new Thread(PrintNumbersSecond);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        static void RunExperiment2()
        {
            Console.WriteLine("Основной поток начал работу");

            // Второй эксперимент - с задержкой
            Console.WriteLine("\nЭксперимент 2: С задержкой 1с между запусками");
            thread1 = new Thread(PrintNumbersFirst);
            Thread thread2 = new Thread(PrintNumbersSecond);

            thread1.Start();
            Thread.Sleep(1000); // Задержка 1 секунда
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        static void PrintNumbersFirst()
        {
            Console.WriteLine("Поток 1 начал работу");
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine($"Поток 1: {i}");
            }
            Console.WriteLine("Поток 1 завершил работу");
        }

        static void PrintNumbersSecond()
        {
            Console.WriteLine("Поток 2 ожидает завершения потока 1...");
            //thread1?.Join(); // Ожидание 1 потока

            Console.WriteLine("Поток 2 начал работу");
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine($"Поток 2: {i}");
            }
            Console.WriteLine("Поток 2 завершил работу");
        }
    }
}