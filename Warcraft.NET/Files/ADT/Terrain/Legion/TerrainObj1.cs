using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks.Legion;

namespace Warcraft.NET.Files.ADT.Terrain.Legion
{
    public class TerrainObj1 : TerrainBase
    {
        /// <summary>
        /// Gets or sets lod information about m2 and wmo
        /// </summary>
        [ChunkOrder(2), ChunkOptional]
        public MLFD LodInfo { get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all M2 models in this ADT.
        /// </summary>
        [ChunkOrder(3)]
        public MLDD ModelPlacementInfo { get; set; }

        /// <summary>
        /// Gets or sets M2 model Extents
        /// </summary>
        [ChunkOrder(4)]
        public MLDX ModelExtents{ get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(5)]
        public MLMD WorldModelObjectPlacementInfo { get; set; }

        /// <summary>
        /// Gets or sets M2 model Extents
        /// </summary>
        [ChunkOrder(6)]
        public MLMX WorldModelObjectExtents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.TerrainObj1"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainObj1(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.TerrainObj1"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObj1(byte[] inData) : base(inData)
        {
        }
    }
}
