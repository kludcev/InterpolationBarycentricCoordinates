using System;

namespace Barycentric
{
    internal class Program
    {
        private static void Main()
        {
            var findPoint = new Point { X = 3, Y = 3, Z = 3, Value = 0 };

            var pointA = new Point { X = 1, Y = 1, Z = 1, Value = 10 };
            var pointB = new Point { X = 3, Y = 3, Z = 10, Value = 45 };
            var pointC = new Point { X = 3, Y = 6, Z = 1, Value = 30 };
            var pointD = new Point { X = 6, Y = 1, Z = 1, Value = 22 };

            try
            {
                var barycentricCalc = new BarycentricCoordinatesCalculator(pointA, pointB, pointC, pointD, findPoint);
                var interpolatedValue = barycentricCalc.Calculate(out var coordinates).Interpolate();

                Console.WriteLine("Barycentric coordinates \n" +
                                  $"for point A: {coordinates.LambdaA} \n" +
                                  $"for point B: {coordinates.LambdaB} \n" +
                                  $"for point C: {coordinates.LambdaC} \n" +
                                  $"for point D: {coordinates.LambdaD} \n" +
                                  $"interpolated value: {interpolatedValue}");
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
