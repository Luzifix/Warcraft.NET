using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Files.ADT.Terrain.WoD
{
    public class TerrainObj0 : TerrainBase
    {
        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(2)]
        public MMDX Models { get; set; }

        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(3)]
        public MMID ModelIndices { get; set; }

        /// <summary>
        /// Gets or sets the contains a list of all WMOs referenced by this ADT.
        /// </summary>
        [ChunkOrder(4)]
        public MWMO WorldModelObjects { get; set; }

        /// <summary>
        /// Gets or sets the contains WMO indexes for the list in ADTWMOs (MWMO chunk).
        /// </summary>
        [ChunkOrder(5)]
        public MWID WorldModelObjectIndices { get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all M2 models in this ADT.
        /// </summary>
        [ChunkOrder(6)]
        public MDDF ModelPlacementInfo { get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(7)]
        public MODF WorldModelObjectPlacementInfo { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WoD.Terrain"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainObj0(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WoD.TerrainObj0"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObj0(byte[] inData) : base(inData)
        {
        }
    }
}
