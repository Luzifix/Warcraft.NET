using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Files.ADT.Terrain.WoD
{
    public class TerrainRoot : TerrainBase
    {
        /// <summary>
        /// Gets or sets the contains the ADT Header with offsets. The header has offsets to the other chunks in the
        /// ADT.
        /// </summary>
        [ChunkOrder(2)]
        public MHDR Header { get; set; }

        /// <summary>
        /// Gets or sets the water informations in this ADT.
        /// </summary>
        [ChunkOrder(3), ChunkOptional]
        public MH2O Water { get; set; }

        /// <summary>
        /// Gets or sets the contains an array of offsets where MCNKs are in the file.
        /// </summary>
        //[ChunkOrder(4)]
        //public MCNK Chunk { get; set; }

        /// <summary>
        /// Gets or sets the contains the ADT bounding box
        /// ADT.
        /// </summary>
        [ChunkOrder(99), ChunkOptional]
        public MFBO BoundingBox { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wotlk.Terrain"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainRoot(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WoD.TerrainRoot"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainRoot(byte[] inData) : base(inData)
        {
        }
    }
}
