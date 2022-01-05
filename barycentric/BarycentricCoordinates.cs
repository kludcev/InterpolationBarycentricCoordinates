namespace Barycentric
{
    public class BarycentricCoordinates
    {
        public double LambdaA { get; set; }
        public double LambdaB { get; set; }
        public double LambdaC { get; set; }
        public double LambdaD { get; set; }

        public BarycentricCoordinates(double l1, double l2, double l3, double l4)
        {
            LambdaA = l1;
            LambdaB = l2;
            LambdaC = l3;
            LambdaD = l4;
        }
    }
}