using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Entrys
{
    public class MH2OInstanceVertexData
    {
        /// <summary>
        /// Gets or sets the height map.
        /// </summary>
        public float[,] HeightMap { get; set; } = new float[0, 0];

        /// <summary>
        /// Gets or sets the depth map.
        /// </summary>
        public byte[,] DepthMap { get; set; } = new byte[0, 0];

        /// <summary>
        /// Gets or sets the UV map.
        /// </summary>
        public UVMapEntry[,] UVMap { get; set; } = new UVMapEntry[0, 0];

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
                switch (instance.LiquidObjectOrVertexFormat)
                {
                    case 0:
                        HeightMap = new float[instance.Height + 1, instance.Width + 1];
                        DepthMap = new byte[instance.Height + 1, instance.Width + 1];
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                HeightMap[y, x] = br.ReadSingle();

                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                DepthMap[y, x] = br.ReadByte();
                        break;
                    case 1:
                        HeightMap = new float[instance.Height + 1, instance.Width + 1];
                        UVMap = new UVMapEntry[instance.Height + 1, instance.Width + 1];
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                HeightMap[y, x] = br.ReadSingle();

                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                UVMap[y, x] = br.ReadUVMapEntry();
                        break;
                    case 2:
                        DepthMap = new byte[instance.Height + 1, instance.Width + 1];
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                DepthMap[y, x] = br.ReadByte();
                        break;
                    case 3:
                        HeightMap = new float[instance.Height + 1, instance.Width + 1];
                        UVMap = new UVMapEntry[instance.Height + 1, instance.Width + 1];
                        DepthMap = new byte[instance.Height + 1, instance.Width + 1];
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                HeightMap[y, x] = br.ReadSingle();
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                UVMap[y, x] = br.ReadUVMapEntry();
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                DepthMap[y, x] = br.ReadByte();
                        break;
                    default: // The value is probably >= 42 so it's a LiquidObject taken out of a DBC file
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize(MH2OInstance instance)
        {
            return sizeof(float) * (instance.Height + 1) * (instance.Width + 1) + sizeof(byte) * (instance.Height + 1) * (instance.Width + 1);
        }

        /// <inheritdoc/>
        public byte[] Serialize(MH2OInstance instance)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                switch (instance.LiquidObjectOrVertexFormat)
                {
                    case 0:
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.Write(HeightMap[y, x]);

                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.Write(DepthMap[y, x]);
                        break;
                    case 1:
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.Write(HeightMap[y, x]);

                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.WriteUVMapEntry(UVMap[y, x]);
                        break;
                    case 2:
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.Write(DepthMap[y, x]);
                        break;
                    case 3:
                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.Write(HeightMap[y, x]);

                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.WriteUVMapEntry(UVMap[y, x]);

                        for (byte y = 0; y < instance.Height + 1; y++)
                            for (byte x = 0; x < instance.Width + 1; x++)
                                bw.Write(DepthMap[y, x]);
                        break;
                    default: // The value is probably >= 42 so it's a LiquidObject taken out of a DBC file
                        break;
                }
                return ms.ToArray();
            }
        }
    }
}
