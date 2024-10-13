namespace ShapesLibrary;

public class Rectangle (double width, double height) : Shape
{
    public double Width { get; } = width;
    public double Height { get; } = height;
    
    public override double GetArea()
    {
        Area = Width * Height;
        return Area;
    }

    public override double GetPerimetr()
    {
        return 2 * (Width + Height);
    }
}