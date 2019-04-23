using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Tests.Files.Structures
{
    [TestClass]
    public class RotatorTests
    {
        [TestMethod]
        public void ToString()
        {
            // Arrage
            var rotator = new Rotator(.1f, .2f, .3f);

            // Act
            string result = rotator.ToString();

            // Assert
            Assert.AreEqual(result, "Pitch: 0,1, Yaw: 0,2, Roll: 0,3");
        }

        [TestMethod]
        public void Flatten()
        {
            // Arrage
            var rotator = new Rotator(.1f, .2f, .3f);

            // Act
            float[] result = rotator.Flatten().ToArray();

            // Assert
            CollectionAssert.AreEqual(result, new float[] { 0.1f, 0.2f, 0.3f });
        }
    }
}
