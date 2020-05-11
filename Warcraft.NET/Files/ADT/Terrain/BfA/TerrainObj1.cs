using TerrainObj1Legion = Warcraft.NET.Files.ADT.Terrain.Legion.TerrainObj1;

namespace Warcraft.NET.Files.ADT.Terrain.BfA
{
    public class TerrainObj1 : TerrainObj1Legion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BfA.TerrainObj1"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainObj1(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BfA.TerrainObj1"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObj1(byte[] inData) : base(inData)
        {
        }
    }
}
