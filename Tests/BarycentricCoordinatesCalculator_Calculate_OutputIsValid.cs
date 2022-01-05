using System;
using Barycentric;
using NUnit.Framework;

namespace Tests
{
    public class BarycentricCoordinatesCalculator_Calculate_OutputIsValid
    {
        private Point _pointA;
        private Point _pointB;
        private Point _pointC;
        private Point _pointD;

        private Point _pointToExamine;

        [SetUp]
        public void Setup()
        {
            _pointToExamine = new Point { X = 3, Y = 3, Z = 3, Value = 0 };

            _pointA = new Point { X = 1, Y = 1, Z = 1, Value = 10 };
            _pointB = new Point { X = 3, Y = 3, Z = 9, Value = 45 };
            _pointC = new Point { X = 3, Y = 6, Z = 1, Value = 30 };
            _pointD = new Point { X = 6, Y = 1, Z = 1, Value = 22 };
        }

        [Test]
        public void Execute()
        {
            new BarycentricCoordinatesCalculator(_pointA, _pointB, _pointC, _pointD, _pointToExamine).Calculate(out var coordinates);

            Assert.That(coordinates.LambdaA, Is.EqualTo(0.27));
            Assert.That(coordinates.LambdaB, Is.EqualTo(0.25));
            Assert.That(coordinates.LambdaC, Is.EqualTo(0.3));
            Assert.That(coordinates.LambdaD, Is.EqualTo(0.18));

        }
    }
}