using System;


namespace barycentric
{
    class Program
    {
        static void Main(string[] args)
        {
            var findPoint = new Point3D { X = 3, Y = 3, Z = 3, ValueExplanation = "Water, %", Value = 0 };

            var pointA = new Point3D { X = 1, Y = 1, Z = 1, ValueExplanation = "Water, %", Value = 10 };
            var pointB = new Point3D { X = 3, Y = 3, Z = 10, ValueExplanation = "Water, %", Value = 45 };
            var pointC = new Point3D { X = 3, Y = 6, Z = 1, ValueExplanation = "Water, %", Value = 30 };
            var pointD = new Point3D { X = 6, Y = 1, Z = 1, ValueExplanation = "Water, %", Value = 22 };

            bool check = CheckIfPointIsInsideOfThetraedron(pointA, pointB, pointC, pointD, findPoint);
            if (check)
            {
                var barycentric = CalculateCoordinates(pointA, pointB, pointC, pointD, findPoint);
                var findPointValue = CalculateValue(pointA, pointB, pointC, pointD, barycentric, findPoint);

                Show(barycentric, findPointValue);
            }

            else
            {
                Show();
            }
        }

        private static bool CheckIfPointIsInsideOfThetraedron(Point3D a, Point3D b, Point3D c, Point3D d, Point3D findPoint)
        {
            int basic = matrix.Determinant(a, b, c, d);
            int t1 = matrix.Determinant(a, b, c, findPoint);
            int t2 = matrix.Determinant(a, b, findPoint, d);
            int t3 = matrix.Determinant(a, findPoint, c, d);
            int t4 = matrix.Determinant(findPoint, b, c, d);

            if (t1 * basic > 0 && t2 * basic > 0 && t3 * basic > 0 && t4 * basic > 0)
                return true;
            return false;
        }
        private static BarycentricCoordinates CalculateCoordinates(Point3D A, Point3D B, Point3D C, Point3D D, Point3D findPoint)
        {


            float det = (A.X - D.X) * (B.Y - D.Y) * (C.Z - D.Z) + (B.X - D.X) * (C.Y - D.Y) * (A.Z - D.Z) + (A.Y - D.Y) * (B.Z - D.Z) * (C.X - D.X) -
                   (C.X - D.X) * (B.Y - D.Y) * (A.Z - D.Z) - (B.Z - D.Z) * (C.Y - D.Y) * (A.X - D.X) - (A.Y - D.Y) * (B.X - D.X) * (C.Z - D.Z);

            float l1 = (-C.Y * B.Z + D.Y * B.Z + B.Y * C.Z - D.Y * C.Z - B.Y * D.Z + C.Y * D.Z) * (findPoint.X - D.X) +
                  (C.X * B.Z - D.X * B.Z - B.X * C.Z + D.X * C.Z + B.X * D.Z - C.X * D.Z) * (findPoint.Y - D.Y) +
                  (-C.X * B.Y + D.X * B.Y + B.X * C.Y - D.X * C.Y - B.X * D.Y + C.X * D.Y) * (findPoint.Z - D.Z);

            l1 = l1 / det;


            float l2 = (C.Y * A.Z - D.Y * A.Z - A.Y * C.Z + D.Y * C.Z + A.Y * D.Z - C.Y * D.Z) * (findPoint.X - D.X) +
                  (-C.X * A.Z + D.X * A.Z + A.X * C.Z - D.X * C.Z - A.X * D.Z + C.X * D.Z) * (findPoint.Y - D.Y) +
                  (C.X * A.Y - D.X * A.Y - A.X * C.Y + D.X * C.Y + A.X * D.Y - C.X * D.Y) * (findPoint.Z - D.Z);

            l2 = l2 / det;


            float l3 = (-B.Y * A.Z + D.Y * A.Z + A.Y * B.Z - D.Y * B.Z - A.Y * D.Z + B.Y * D.Z) * (findPoint.X - D.X) +
                  (B.X * A.Z - D.X * A.Z - A.X * B.Z + D.X * B.Z + A.X * D.Z - B.X * D.Z) * (findPoint.Y - D.Y) +
                  (-B.X * A.Y + D.X * A.Y + A.X * B.Y - D.X * B.Y - A.X * D.Y + B.X * D.Y) * (findPoint.Z - D.Z);

            l3 = l3 / det;


            float l4 = 1 - l1 - l2 - l3;

            return new BarycentricCoordinates(l1, l2, l3, l4);

        }
        private static Point3D CalculateValue(Point3D pointA, Point3D pointB, Point3D pointC, Point3D pointD, BarycentricCoordinates barycentric, Point3D findPoint)
        {
            float value = barycentric.LambdaA * pointA.Value + barycentric.LambdaB * pointB.Value +
                          barycentric.LambdaC * pointC.Value + barycentric.LambdaD * pointD.Value;

            return new Point3D(findPoint.X, findPoint.Y, findPoint.Z, findPoint.ValueExplanation, value);
        }

        private static void Show(BarycentricCoordinates barycentric, Point3D findPoint)
        {
            Console.WriteLine("Barycentric coordinates");
            Console.Write("for point A: ");
            Console.WriteLine(barycentric.LambdaA);
            Console.Write("for point B: ");
            Console.WriteLine(barycentric.LambdaB);
            Console.Write("for point C: ");
            Console.WriteLine(barycentric.LambdaC);
            Console.Write("for point D: ");
            Console.WriteLine(barycentric.LambdaD);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("FindPoint characteristics: " + findPoint.Value);

            Console.ReadLine();
        }

        private static void Show()
        {
            Console.WriteLine("Исследуемая точка лежит вне тетраэдра.");
            Console.ReadLine();
        }


    }

    public class Point3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public string ValueExplanation { get; set; }
        public float Value { get; set; }

        public Point3D(int x, int y, int z, string valstr, float val)
        {
            X = x;
            Y = y;
            Z = z;
            ValueExplanation = valstr;
            Value = val;
        }

        public Point3D()
        {

        }
    }

    public static class matrix
    {
        public static int Determinant(Point3D a, Point3D b, Point3D c, Point3D d)
        {
            return

           1 * b.Z * c.Y * d.X - a.Z * 1 * c.Y * d.X -
           1 * b.Y * c.Z * d.X + a.Y * 1 * c.Z * d.X +
           a.Z * b.Y * 1 * d.X - a.Y * b.Z * 1 * d.X -
           1 * b.Z * c.X * d.Y + a.Z * 1 * c.X * d.Y +
           1 * b.X * c.Z * d.Y - a.X * 1 * c.Z * d.Y -
           a.Z * b.X * 1 * d.Y + a.X * b.Z * 1 * d.Y +
           1 * b.Y * c.X * d.Z - a.Y * 1 * c.X * d.Z -
           1 * b.X * c.Y * d.Z + a.X * 1 * c.Y * d.Z +
           a.Y * b.X * 1 * d.Z - a.X * b.Y * 1 * d.Z -
           a.Z * b.Y * c.X * 1 + a.Y * b.Z * c.X * 1 +
           a.Z * b.X * c.Y * 1 - a.X * b.Z * c.Y * 1 -
           a.Y * b.X * c.Z * 1 + a.X * b.Y * c.Z * 1;

        }
    }
    class BarycentricCoordinates
    {
        public float LambdaA { get; set; }
        public float LambdaB { get; set; }
        public float LambdaC { get; set; }
        public float LambdaD { get; set; }

        public BarycentricCoordinates(float l1, float l2, float l3, float l4)
        {
            LambdaA = l1;
            LambdaB = l2;
            LambdaC = l3;
            LambdaD = l4;
        }

    }
}
