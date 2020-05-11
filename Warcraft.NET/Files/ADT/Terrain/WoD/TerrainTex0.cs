using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Files.ADT.Terrain.WoD
{
    public class TerrainTex0 : TerrainBase
    {
        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        //[ChunkOrder(2)]
        //public MAMP MAMP { get; set; }

        /// <summary>
        /// Gets or sets the contains a list of all textures referenced by this ADT.
        /// </summary>
        [ChunkOrder(3)]
        public MTEX Textures { get; set; }

        /// <summary>
        /// Gets or sets the contains an array of offsets where MCNKs are in the file.
        /// </summary>
        //[ChunkOrder(4)]
        //public MCNK Chunk { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WoD.Terrain"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainTex0(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WoD.TerrainTex0"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainTex0(byte[] inData) : base(inData)
        {
            
        }
    }
}
