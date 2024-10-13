namespace MyPractice;

public class Student(int id, string name, int courseId)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public int CourseId { get; set; } = courseId;
}