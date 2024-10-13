namespace ShapesLibrary;

public class Triangle (double sideA, double sideB, double sideC) : Shape
{
    public double SideA { get; } = sideA;
    public double SideB { get; } = sideB;
    public double SideC { get; } = sideC;
    
    public override double GetArea()
    {
        double semiPerimeter = GetPerimetr() / 2;
        Area = Math.Sqrt(semiPerimeter * (semiPerimeter - SideA) * (semiPerimeter - SideB) * (semiPerimeter - SideC));
        return Area;
    }

    public override double GetPerimetr()
    {
        return SideA + SideB + SideC;
    }
}