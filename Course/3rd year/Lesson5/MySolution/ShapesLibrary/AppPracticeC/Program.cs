namespace AppPracticeC;

using ShapesLibrary;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выберите фигуру: 1 - Круг, 2 - Прямоугольник, 3 - Треугольник");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                CreateCircle();
                break;
            case 2:
                CreateRectangle();
                break;
            case 3:
                CreateTriangle();
                break;
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }

    static void CreateCircle()
    {
        Console.Write("Введите радиус круга: ");
        double radius = double.Parse(Console.ReadLine());

        Circle circle = new Circle(radius);
        Console.WriteLine($"Площадь круга: {circle.GetArea()}");
        Console.WriteLine($"Периметр круга: {circle.GetPerimetr()}");
        Console.WriteLine($"Диаметр круга: {circle.GetDiameter()}");
    }

    static void CreateRectangle()
    {
        Console.Write("Введите ширину прямоугольника: ");
        double width = double.Parse(Console.ReadLine());

        Console.Write("Введите высоту прямоугольника: ");
        double height = double.Parse(Console.ReadLine());

        Rectangle rectangle = new Rectangle(width, height);
        Console.WriteLine($"Площадь прямоугольника: {rectangle.GetArea()}");
        Console.WriteLine($"Периметр прямоугольника: {rectangle.GetPerimetr()}");
    }

    static void CreateTriangle()
    {
        Console.Write("Введите длину стороны A: ");
        double sideA = double.Parse(Console.ReadLine());

        Console.Write("Введите длину стороны B: ");
        double sideB = double.Parse(Console.ReadLine());

        Console.Write("Введите длину стороны C: ");
        double sideC = double.Parse(Console.ReadLine());

        try
        {
            Triangle triangle = new Triangle(sideA, sideB, sideC);
            Console.WriteLine($"Площадь треугольника: {triangle.GetArea()}");
            Console.WriteLine($"Периметр треугольника: {triangle.GetPerimetr()}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}