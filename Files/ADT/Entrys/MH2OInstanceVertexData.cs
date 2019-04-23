using System.IO;

namespace Warcraft.NET.Files.ADT.Entrys
{
    public class MH2OInstanceVertexData
    {
        /// <summary>
        /// Gets or sets the height map.
        /// </summary>
        public float[,] HeightMap { get; set; } = new float[8, 8];
        /// <summary>
        /// Gets or sets the depth map.
        /// </summary>
        public byte[,] DepthMap { get; set; } = new byte[8, 8];

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2OInstanceVertexData"/> class.
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="instance"></param>
        public MH2OInstanceVertexData(byte[] inData, MH2OInstance instance)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                if (instance.LiquidObjectOrVertexFormat != 2)
                {
                    for (byte z = instance.OffsetY; z < instance.Height + instance.OffsetY; z++)
                        for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                            HeightMap[z, x] = br.ReadSingle();
                }

                for (byte z = instance.OffsetY; z < instance.Height + instance.OffsetY; z++)
                    for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                        DepthMap[z, x] = br.ReadByte();
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return sizeof(float) * 64 + sizeof(byte) * 64;
        }

        /// <inheritdoc/>
        public byte[] Serialize(MH2OInstance instance)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                if (instance.LiquidObjectOrVertexFormat != 2)
                {
                    for (byte y = instance.OffsetY; y < instance.Height + instance.OffsetY; y++)
                        for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                            bw.Write(HeightMap[y, x]);
                }

                for (byte y = instance.OffsetY; y < instance.Height + instance.OffsetY; y++)
                    for (byte x = instance.OffsetX; x < instance.Width + instance.OffsetX; x++)
                        bw.Write(DepthMap[y, x]);

                return ms.ToArray();
            }
        }
    }
}
