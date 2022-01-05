using System;

namespace Barycentric
{
    public class BarycentricCoordinatesCalculator
    {
        private readonly Point _pointA;
        private readonly Point _pointB;
        private readonly Point _pointC;
        private readonly Point _pointD;

        private readonly Point _pointToExamine;

        public BarycentricCoordinates BarycentricCoordinates { get; private set; }

        public BarycentricCoordinatesCalculator(Point pointA, Point pointB, Point pointC, Point pointD, Point pointToExamine)
        {
            _pointA = pointA ?? throw new ArgumentNullException(nameof(pointA));
            _pointB = pointB ?? throw new ArgumentNullException(nameof(pointA));
            _pointC = pointC ?? throw new ArgumentNullException(nameof(pointA));
            _pointD = pointD ?? throw new ArgumentNullException(nameof(pointA));

            _pointToExamine = pointToExamine ?? throw new ArgumentNullException(nameof(pointToExamine));

            if (!CheckIfPointIsInsideOfThetraedron())
                throw new ApplicationException("Examined point is outside of thetraedron");
        }

        public BarycentricCoordinatesCalculator Calculate(out BarycentricCoordinates coordinates)
        {
            var l1 = (-_pointC.Y * _pointB.Z + _pointD.Y * _pointB.Z + _pointB.Y * _pointC.Z - _pointD.Y * _pointC.Z - _pointB.Y * _pointD.Z + _pointC.Y * _pointD.Z) * (_pointToExamine.X - _pointD.X) +
                       (_pointC.X * _pointB.Z - _pointD.X * _pointB.Z - _pointB.X * _pointC.Z + _pointD.X * _pointC.Z + _pointB.X * _pointD.Z - _pointC.X * _pointD.Z) * (_pointToExamine.Y - _pointD.Y) +
                       (-_pointC.X * _pointB.Y + _pointD.X * _pointB.Y + _pointB.X * _pointC.Y - _pointD.X * _pointC.Y - _pointB.X * _pointD.Y + _pointC.X * _pointD.Y) * (_pointToExamine.Z - _pointD.Z);

            var l2 = (_pointC.Y * _pointA.Z - _pointD.Y * _pointA.Z - _pointA.Y * _pointC.Z + _pointD.Y * _pointC.Z + _pointA.Y * _pointD.Z - _pointC.Y * _pointD.Z) * (_pointToExamine.X - _pointD.X) +
                       (-_pointC.X * _pointA.Z + _pointD.X * _pointA.Z + _pointA.X * _pointC.Z - _pointD.X * _pointC.Z - _pointA.X * _pointD.Z + _pointC.X * _pointD.Z) * (_pointToExamine.Y - _pointD.Y) +
                       (_pointC.X * _pointA.Y - _pointD.X * _pointA.Y - _pointA.X * _pointC.Y + _pointD.X * _pointC.Y + _pointA.X * _pointD.Y - _pointC.X * _pointD.Y) * (_pointToExamine.Z - _pointD.Z);

            var l3 = (-_pointB.Y * _pointA.Z + _pointD.Y * _pointA.Z + _pointA.Y * _pointB.Z - _pointD.Y * _pointB.Z - _pointA.Y * _pointD.Z + _pointB.Y * _pointD.Z) * (_pointToExamine.X - _pointD.X) +
                       (_pointB.X * _pointA.Z - _pointD.X * _pointA.Z - _pointA.X * _pointB.Z + _pointD.X * _pointB.Z + _pointA.X * _pointD.Z - _pointB.X * _pointD.Z) * (_pointToExamine.Y - _pointD.Y) +
                       (-_pointB.X * _pointA.Y + _pointD.X * _pointA.Y + _pointA.X * _pointB.Y - _pointD.X * _pointB.Y - _pointA.X * _pointD.Y + _pointB.X * _pointD.Y) * (_pointToExamine.Z - _pointD.Z);

            var det = (_pointA.X - _pointD.X) * (_pointB.Y - _pointD.Y) * (_pointC.Z - _pointD.Z) + (_pointB.X - _pointD.X) * (_pointC.Y - _pointD.Y) * (_pointA.Z - _pointD.Z) + (_pointA.Y - _pointD.Y) * (_pointB.Z - _pointD.Z) * (_pointC.X - _pointD.X) -
                        (_pointC.X - _pointD.X) * (_pointB.Y - _pointD.Y) * (_pointA.Z - _pointD.Z) - (_pointB.Z - _pointD.Z) * (_pointC.Y - _pointD.Y) * (_pointA.X - _pointD.X) - (_pointA.Y - _pointD.Y) * (_pointB.X - _pointD.X) * (_pointC.Z - _pointD.Z);

            l1 /= det;
            l2 /= det;
            l3 /= det;
            var l4 = 1 - l1 - l2 - l3;

            BarycentricCoordinates = coordinates = new BarycentricCoordinates(l1, l2, l3, l4);

            return this;
        }

        public double Interpolate()
        {
            if (BarycentricCoordinates == null)
                Calculate(out _);

            return BarycentricCoordinates.LambdaA * _pointA.Value + BarycentricCoordinates.LambdaB * _pointB.Value +
                   BarycentricCoordinates.LambdaC * _pointC.Value + BarycentricCoordinates.LambdaD * _pointD.Value;
        }

        private bool CheckIfPointIsInsideOfThetraedron()
        {
            var basic = MatrixDeterminant(_pointA, _pointB, _pointC, _pointD); 
            var t1 = MatrixDeterminant(_pointA, _pointB, _pointC, _pointToExamine);
            var t2 = MatrixDeterminant(_pointA, _pointB, _pointToExamine, _pointD);
            var t3 = MatrixDeterminant(_pointA, _pointToExamine, _pointC, _pointD);
            var t4 = MatrixDeterminant(_pointToExamine, _pointB, _pointC, _pointD);

            return t1 * basic > 0 && t2 * basic > 0 && t3 * basic > 0 && t4 * basic > 0;

            double MatrixDeterminant(Point a, Point b, Point c, Point d)
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
    }
}
