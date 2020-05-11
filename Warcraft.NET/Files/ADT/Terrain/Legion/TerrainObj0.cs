using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;
using TerrainObj0WoD = Warcraft.NET.Files.ADT.Terrain.WoD.TerrainObj0;

namespace Warcraft.NET.Files.ADT.Terrain.Legion
{
    public class TerrainObj0 : TerrainObj0WoD
    {
        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(2), ChunkOptional]
        public new MMDX Models { get; set; }

        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(3), ChunkOptional]
        public new MMID ModelIndices { get; set; }

        /// <summary>
        /// Gets or sets the contains a list of all WMOs referenced by this ADT.
        /// </summary>
        [ChunkOrder(4), ChunkOptional]
        public new MWMO WorldModelObjects { get; set; }

        /// <summary>
        /// Gets or sets the contains WMO indexes for the list in ADTWMOs (MWMO chunk).
        /// </summary>
        [ChunkOrder(5), ChunkOptional]
        public new MWID WorldModelObjectIndices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.TerrainObj0"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainObj0(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.TerrainObj0"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObj0(byte[] inData) : base(inData)
        {
        }
    }
}
