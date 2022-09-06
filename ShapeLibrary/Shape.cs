namespace ShapeLibrary
{
    public abstract class Shape : IArea
    {
        public abstract double GetArea();
    }

    public class Circle : Shape
    {
        public double Diameter { get; private set; }

        public Circle(double diameter)
        {
            Diameter = diameter;
        }

        public override double GetArea()
        {
            double area;
            area = Math.PI * Math.Pow(Diameter, 2);
            return area;
        }
    }

    public class Triangle : Shape
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }
        public bool IsRightTriangle { get; private set; }

        public Triangle(double sideA, double sideB, double sideC)
        {
            A = sideA;
            B = sideB;
            C = sideC;

            SetRightTriangle();
        }

        public override double GetArea()
        {
            double p = (A + B + C) / 2.0;

            double area = Math.Sqrt(p * (p - A) * (p - B) * (p - C));

            return area;
        }

        private void SetRightTriangle()
        {
            if ((A * A + B * B == C * C) || (A * A + C * C == B * B) || (C * C + B * B == A * A))
            {
                IsRightTriangle = true;
            }
        }
    }
}