using C_ClientServer.sqlLite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            Student tom = new Student { Name = "Бодя", Age = 33 };
            Student alice = new Student { Name = "Алиса", Age = 26 };

            Teacher john = new Teacher { Name = "Кеша" };
            Teacher jane = new Teacher { Name = "Виктор" };

            Course math = new Course { Title = "Русский" };
            Course physics = new Course { Title = "ИЗО" };

            tom.Courses.Add(math);
            alice.Courses.Add(physics);

            john.Courses.Add(math);
            jane.Courses.Add(physics);

            db.Students.AddRange(tom, alice);
            db.Teachers.AddRange(john, jane);
            db.Courses.AddRange(math, physics);

            db.SaveChanges();
            Console.WriteLine("Успешно");

            var courses = db.Courses.Include(c => c.Students).Include(c => c.Teachers).ToList();
            Console.WriteLine("Список курсов:");
            foreach (var course in courses)
            {
                Console.WriteLine($"Курс: {course.Title}");
                Console.WriteLine("Преподаватели:");
                foreach (var teacher in course.Teachers)
                {
                    Console.WriteLine($"{teacher.Name}");
                }
                Console.WriteLine("Студенты:");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"{student.Name} ({student.Age} лет)");
                }
                Console.WriteLine();
            }
        }
    }
}