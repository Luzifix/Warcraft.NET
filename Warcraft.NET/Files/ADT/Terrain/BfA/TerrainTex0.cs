using TerrainTex0WoD = Warcraft.NET.Files.ADT.Terrain.WoD.TerrainTex0;

namespace Warcraft.NET.Files.ADT.Terrain.BfA
{
    public class TerrainTex0 : TerrainTex0WoD
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BfA.Terrain"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainTex0(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BfA.TerrainTex0"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainTex0(byte[] inData) : base(inData)
        {
        }
    }
}
