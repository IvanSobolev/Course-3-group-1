namespace ShapesLibrary;

public class Circle (double radius) : Shape
{
    public double Radius { get; } = radius;

    public override double GetArea()
    {
        Area = Math.PI * Radius * Radius;
        return Area;
    }

    public override double GetPerimetr()
    {
        return 2 * Math.PI * Radius;
    }
    
    public double GetDiameter()
    {
        return 2 * Radius;
    }
}