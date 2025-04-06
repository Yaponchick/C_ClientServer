using C_ClientServer.sqlLite;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            /*db.SeedData();*/

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Просмотреть студентов");
                Console.WriteLine("2. Добавить студента");
                Console.WriteLine("3. Обновить студента");
                Console.WriteLine("4. Удалить студента");
                Console.WriteLine("5. Ленивая загрузка");
                Console.WriteLine("q. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ReadStudents(db);
                        break;
                    case "2":
                        Console.Write("Введите имя: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Введите фамилию: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Введите возраст: ");
                        int age = int.Parse(Console.ReadLine());
                        CreateStudent(db, firstName, lastName, age);
                        break;
                    case "3":
                        Console.Write("Введите ID студента для обновления: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("Введите новое имя: ");
                        string newFirstName = Console.ReadLine();
                        Console.Write("Введите новую фамилию: ");
                        string newLastName = Console.ReadLine();
                        UpdateStudent(db, updateId, newFirstName, newLastName);
                        break;
                    case "4":
                        Console.Write("Введите ID студента для удаления: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        DeleteStudent(db, deleteId);
                        break;
                    case "5":
                        LazyLoadingExample(db);
                        break;
                    case "q":
                        Console.WriteLine("Выход");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!1!1!!!!");
                        break;
                }
            }
        }
    }

    static void CreateStudent(ApplicationContext db, string firstName, string lastName, int age)
    {
        var student = new Student { FirstName = firstName, LastName = lastName, Age = age };
        db.Students.Add(student);
        db.SaveChanges();
        Console.WriteLine($"Создан студент: {firstName} {lastName}");
    }

    static void ReadStudents(ApplicationContext db)
    {
        var students = db.Students.ToList();
        Console.WriteLine("Список студентов:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.StudentId}: {student.FirstName} {student.LastName}, возраст: {student.Age}");
        }
    }

    static void UpdateStudent(ApplicationContext db, int id, string newFirstName, string newLastName)
    {
        var student = db.Students.Find(id);
        if (student != null)
        {
            student.FirstName = newFirstName;
            student.LastName = newLastName;
            db.SaveChanges();
            Console.WriteLine($"Студент с ID {id} обновлен.");
        }
        else
        {
            Console.WriteLine($"Студент с ID {id} не найден.");
        }
    }

    static void DeleteStudent(ApplicationContext db, int id)
    {
        var student = db.Students.Find(id);
        if (student != null)
        {
            db.Students.Remove(student);
            db.SaveChanges();
            Console.WriteLine($"Студент с ID {id} удален.");
        }
        else
        {
            Console.WriteLine($"Студент с ID {id} не найден.");
        }
    }

    static void LazyLoadingExample(ApplicationContext db)
    {
        Console.WriteLine("ЛЕНИВАЯ ЗАГРУЗКА");

        var studentLazy = db.Students.OrderBy(s => s.StudentId).FirstOrDefault();
        if (studentLazy != null)
        {
            Console.WriteLine($"Студент: {studentLazy.FirstName} {studentLazy.LastName}");
            foreach (var enrollment in studentLazy.Enrollments)
            {
                Console.WriteLine($"  Курс: {enrollment.Course.CourseName}");
            }
        }
        else
        {
            Console.WriteLine("Студенты не найдены.");
        }
    }
}