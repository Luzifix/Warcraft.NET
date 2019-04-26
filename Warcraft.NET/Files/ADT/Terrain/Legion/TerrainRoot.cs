using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;
using TerrainRootWoD = Warcraft.NET.Files.ADT.Terrain.WoD.TerrainRoot;

namespace Warcraft.NET.Files.ADT.Terrain.Legion
{
    public class TerrainRoot : TerrainRootWoD
    {       
        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.TerrainRoot"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainRoot(byte[] inData) : base(inData)
        {
        }
    }
}
