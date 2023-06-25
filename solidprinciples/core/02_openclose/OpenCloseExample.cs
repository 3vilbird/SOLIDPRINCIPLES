namespace solidprinciples.core
{
    public interface IShape
    {
        public double Area();
    }

    public class Rectangle : IShape
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public Rectangle(double h, double w)
        {
            Height = h;
            Width = w;
        }

        public double Area()
        {
            return Height * Width;
        }
    }
    public class Circle : IShape
    {
        public double Radius { get; set; }
        public Circle(double r)
        {
            Radius = r;
        }
        public double Area()
        {
            return Radius * Radius * Math.PI;
        }
    }

    public static class AreaCalculator
    {
        public static double TotalArea(IShape[] arrShapes)
        {
            double area = 0;
            foreach (var objShape in arrShapes)
            {
                area += objShape.Area();
            }
            return area;
        }
    }



    public class OpenCloseExample
    {
        public static void Index()
        {
            IShape[] data = new IShape[2];
            IShape rect = new Rectangle(10, 10);
            IShape cir = new Circle(10);
            data[0] = rect;
            data[1] = cir;
            var area = AreaCalculator.TotalArea(data);
            Console.WriteLine(area);
        }

    }
}