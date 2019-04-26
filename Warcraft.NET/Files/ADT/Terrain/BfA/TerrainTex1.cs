using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;
using TerrainTex1WoD = Warcraft.NET.Files.ADT.Terrain.WoD.TerrainTex1;

namespace Warcraft.NET.Files.ADT.Terrain.BfA
{
    public class TerrainTex1 : TerrainTex1WoD
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BfA.TerrainTex1"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainTex1(byte[] inData) : base(inData)
        {
        }
    }
}
