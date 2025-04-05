namespace C_ClientServer.sqlLite
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}