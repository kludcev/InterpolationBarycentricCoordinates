using System;
using Barycentric;
using NUnit.Framework;

namespace Tests
{
    public class BarycentricCoordinatesCalculator_ArgumentsAreNotInitialized_ThrowsArgumentNullExeption
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
            _pointB = new Point { X = 3, Y = 3, Z = 10, Value = 45 };
            _pointC = new Point { X = 3, Y = 6, Z = 1, Value = 30 };
            _pointD = new Point { X = 6, Y = 1, Z = 1, Value = 22 };
        }

        [Test]
        public void Execute()
        {
            Assert.That(() =>
            {
                var calc = new BarycentricCoordinatesCalculator(null, _pointB, _pointC, _pointD, _pointToExamine);
            }, Throws.Exception.TypeOf<ArgumentNullException>());

            Assert.That(() =>
            {
                var calc = new BarycentricCoordinatesCalculator(_pointA, null, _pointC, _pointD, _pointToExamine);
            }, Throws.Exception.TypeOf<ArgumentNullException>());

            Assert.That(() =>
            {
                var calc = new BarycentricCoordinatesCalculator(_pointA, _pointB, null, _pointD, _pointToExamine);
            }, Throws.Exception.TypeOf<ArgumentNullException>());

            Assert.That(() =>
            {
                var calc = new BarycentricCoordinatesCalculator(_pointA, _pointB, _pointC, null, _pointToExamine);
            }, Throws.Exception.TypeOf<ArgumentNullException>());

            Assert.That(() =>
            {
                var calc = new BarycentricCoordinatesCalculator(_pointA, _pointB, _pointC, _pointD, null);
            }, Throws.Exception.TypeOf<ArgumentNullException>());
        }
    }
}