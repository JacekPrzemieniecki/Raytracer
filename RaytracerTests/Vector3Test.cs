using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raytracer;

namespace RaytracerTests
{
    [TestClass]
    public class Vector3Test
    {
        private const float Delta = 0.0001f;
        [TestMethod]
        public void TestAddition()
        {
            var lhs = new Vector3(1, 2, 3);
            var rhs = new Vector3(3, 3, 3);
            var sum = lhs + rhs;
            Assert.AreEqual(sum.x, 1 + 3, Delta);
            Assert.AreEqual(sum.y, 2 + 3, Delta);
            Assert.AreEqual(sum.z, 3 + 3, Delta);
        }

        [TestMethod]
        public void DotProductOfEqualVectors()
        {
            var v1 = new Vector3(3, 4, 5);
            var v2 = new Vector3(3, 4, 5);
            var dotProduct = Vector3.Dot(v1, v2);
            Assert.AreEqual(50, dotProduct, Delta);
        }

        [TestMethod]
        public void DotProductOfPerpendicularVectors()
        {
            var v1 = new Vector3(0, 4, 0);
            var v2 = new Vector3(4, 0, 2);
            var dotProduct = Vector3.Dot(v1, v2);
            Assert.AreEqual(0, dotProduct, Delta);
        }

        [TestMethod]
        public void Length()
        {
            var vector = new Vector3(3, 4, 5);
            Assert.AreEqual(Math.Sqrt(50), vector.Length(), Delta);
        }

        [TestMethod]
        public void Normalization()
        {
            var vector = new Vector3(0, 3, 4);
            var normalized = vector.Normalized();
            Assert.AreEqual(0, normalized.x, Delta);
            Assert.AreEqual(0.6f, normalized.y, Delta);
            Assert.AreEqual(0.8f, normalized.z, Delta);
        }

        [TestMethod]
        public void RotationByIdentityQuaternion()
        {
            var vector = new Vector3(3, 4, 5);
            var rotated = vector.RotatedBy(Quaternion.Identity);
            Assert.AreEqual(vector.x, rotated.x, Delta);
            Assert.AreEqual(vector.y, rotated.y, Delta);
            Assert.AreEqual(vector.z, rotated.z, Delta);
        }

        [TestMethod]
        public void RotationByNonZeroQuaternion()
        {
            var vector = new Vector3(3, 4, 5);
            var rotated = vector.RotatedBy(new Quaternion(0.5f, 0.5f, 0.5f, 0.5f));
            Assert.AreEqual(5, rotated.x, Delta);
            Assert.AreEqual(3, rotated.y, Delta);
            Assert.AreEqual(4, rotated.z, Delta);
        }

        [TestMethod]
        public void CrossProduct()
        {
            var v1 = new Vector3(3, -3, 1);
            var v2 = new Vector3(4, 9, 2);
            var crossProduct = Vector3.Cross(v1, v2);
            Assert.AreEqual(-15, crossProduct.x, Delta);
            Assert.AreEqual(-2, crossProduct.y, Delta);
            Assert.AreEqual(39, crossProduct.z, Delta);
        }
    }
}
