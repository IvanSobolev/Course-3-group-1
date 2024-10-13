namespace ShapesLibrary;

public abstract class Shape
{
    public double Area { get; protected set; }

    public abstract double GetArea();

    public abstract double GetPerimetr();
}