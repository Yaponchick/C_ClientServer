using System;
using System.Reflection.Emit;

namespace MultiThread01
{
    class Program
    {
        static void Main01()
        {
            for (; ; )
            {
                Console.WriteLine("Выберите программу для запуска:");
                Console.WriteLine("1 - Лабораторная работа 1 (Лямбда-выражения)");
                Console.WriteLine("2 - Лабораторная работа 2 (Эксперименты с потоками)");
                Console.WriteLine("3 - Информация о текущем потоке");
                Console.WriteLine("q - Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProgrammLab01.Run();
                        ThreadInfo.Run();
                        break;
                    case "2":
                        Lab02.Run();
                        ThreadInfo.Run();

                        break;
                    case "3":
                        ThreadInfo.Run();
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Программа завершена.");
                        return;
                }
            }
        }
    }
}