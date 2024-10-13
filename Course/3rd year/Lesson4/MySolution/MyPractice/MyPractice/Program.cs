namespace MyPractice;

static class Program
{
    static void Main(string[] args)
    {
        List<Student> students = new List<Student>
        {
            new Student(1, "Dima", 1),
            new Student(2, "Vanya", 1),
            new Student(3, "Fenya", 2),
        };
        
        List<Course> courses = new List<Course>
        {
            new Course(1, "Back-end"),
            new Course(2, "Front-end"),
            new Course(3, "Front-end")
        };

        //task 1
        Console.WriteLine("task 1 \n");
        
        var studentCourses = students.Join(courses, 
            student => student.CourseId,
            course => course.Id,
            (student, course) => new StudentCourse(student.Id, student.Name, course.CourseName));

        foreach (var studentCourse in studentCourses)
        {
            Console.WriteLine($"{studentCourse.Name} - {studentCourse.CourseName}");
        }
        
        //task 2
        Console.WriteLine("\ntask 2 \n");
        
        var grouped = studentCourses.GroupBy(sc => sc.CourseName);

        foreach (var group in grouped)
        {
            Console.WriteLine($"Курс: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"  Студент: {student.Name}");
            }
        }
        
        //task 3
        Console.WriteLine("\ntask 3 \n");
        
        var grades = new List<int> { 85, 90, 78, 92, 88 };
        int total = grades.Sum();
        Console.WriteLine($"Общая сумма оценок: {total}");
        
        //task 4
        Console.WriteLine("\ntask 4 \n");
        
        double average = grades.Average();
        Console.WriteLine($"Средняя оценка: {average}");
        
        //task 5
        Console.WriteLine("\ntask 5 \n");
        
        var courses_name = new List<string> { "Back-end", "Front-end", "Design", "Back-end" };
        var uniqueCourses = courses_name.Distinct();

        foreach (var course in uniqueCourses)
        {
            Console.WriteLine(course);
        }
        
        //task 6
        Console.WriteLine("\ntask 6 \n");
        
        var students1 = new List<string> { "Алексей", "Мария" };
        var students2 = new List<string> { "Мария", "Ольга" };

        var allStudents = students1.Union(students2);

        foreach (var student in allStudents)
        {
            Console.WriteLine(student);
        }
        
        //task 7
        Console.WriteLine("\ntask 7 \n");
        
        var commonStudents = students1.Intersect(students2);

        foreach (var student in commonStudents)
        {
            Console.WriteLine(student);
        }
        
        //task 8
        Console.WriteLine("\ntask 8 \n");
        
        var onlyFirstCourseStudents = students1.Except(students2);

        foreach (var student in onlyFirstCourseStudents)
        {
            Console.WriteLine(student);
        }
        
        //task 9
        Console.WriteLine("\ntask 9 \n");
        
        var filteredStudents = students.FilterByFirstNameLetter('D');

        foreach (var student in filteredStudents)
        {
            Console.WriteLine(student.Name);
        }
        
        //task 10
        Console.WriteLine("\ntask 10 \n");
        
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

        var filteredNumbers = numbers.Where(n => n > 3);

        Console.WriteLine("Запрос еще не выполнен.");

        var result = filteredNumbers.ToList();

        Console.WriteLine("Запрос выполнен:");
        foreach (var number in result)
        {
            Console.WriteLine(number);
        }
    }
    
    public static IEnumerable<Student> FilterByFirstNameLetter(this IEnumerable<Student> students, char letterName)
    {
        return students.Where(s => s.Name.StartsWith(letterName));
    }
}