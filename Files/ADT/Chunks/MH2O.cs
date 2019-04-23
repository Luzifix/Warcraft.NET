using System.IO;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.ADT.Entrys;

namespace Warcraft.NET.Files.ADT.Chunks
{
    public class MH2O : IIFFChunk, IBinarySerializable
    {
        public const string Signature = "MH2O";

        /// <summary>
        /// Gets or sets <see cref="MH2OHeader"/>s.
        /// </summary>
        public MH2OHeader[] MH2OHeaders { get; set; } = new MH2OHeader[256];

        public MH2O()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2O"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MH2O(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            int size = MH2OHeader.GetSize() * 256;

            foreach (var header in MH2OHeaders)
            {
                if (header.Attributes != null && !header.Attributes.HasOnlyZeroes)
                    size += MH2OAttribute.GetSize();

                foreach (var instance in header.Instances)
                {
                    size += MH2OInstance.GetSize();

                    if (instance.RenderBitmapBytes.Length == (instance.Width * instance.Height + 7) / 8)
                        size += instance.RenderBitmapBytes.Length;
                    if (instance.VertexData != null)
                        size += MH2OInstanceVertexData.GetSize(instance);
                }
            }
            return (uint)size;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                for (int i = 0; i < 256; i++)
                    MH2OHeaders[i] = new MH2OHeader(br.ReadBytes(MH2OHeader.GetSize()));

                foreach (var header in MH2OHeaders)
                {
                    // load MH2O header subdata
                    if (header.LayerCount > 0)
                    {
                        // load MH2O instances
                        br.BaseStream.Position = header.OffsetInstances;
                        for (int i = 0; i < header.LayerCount; i++)
                            header.Instances[i] = new MH2OInstance(br.ReadBytes(MH2OInstance.GetSize()));

                        // load MH2O attributes
                        if (header.OffsetAttributes > 0)
                        {
                            br.BaseStream.Position = header.OffsetAttributes;
                            header.Attributes = new MH2OAttribute(br.ReadBytes(MH2OAttribute.GetSize()));
                        }

                        // load MH2O instance subdata
                        foreach (var instance in header.Instances)
                        {
                            if (instance.OffsetExistsBitmap > 0)
                            {
                                br.BaseStream.Position = instance.OffsetExistsBitmap;
                                instance.RenderBitmapBytes = br.ReadBytes((instance.Width * instance.Height + 7) / 8);
                            }

                            if (instance.OffsetVertexData > 0)
                            {
                                br.BaseStream.Position = instance.OffsetVertexData;
                                instance.VertexData = new MH2OInstanceVertexData(br.ReadBytes(MH2OInstanceVertexData.GetSize(instance)), instance);
                            }
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                // Write MH2O headers later when we got the offsets
                bw.Seek(256 * MH2OHeader.GetSize(), SeekOrigin.Begin);

                // Write MH2O instances later when we got the offsets
                foreach (var header in MH2OHeaders)
                {
                    header.OffsetInstances = (uint)bw.BaseStream.Position;
                    bw.Seek(MH2OInstance.GetSize() * header.Instances.Length, SeekOrigin.Current);
                }

                foreach (var header in MH2OHeaders)
                {
                    header.LayerCount = (uint)header.Instances.Length;
                    if (header.LayerCount > 0)
                    {
                        foreach (var instance in header.Instances)
                        {
                            // Write MH2O instance subdata
                            if (instance.VertexData is null)
                                instance.OffsetVertexData = 0;
                            else
                            {
                                instance.OffsetVertexData = (uint)bw.BaseStream.Position;
                                bw.Write(instance.VertexData.Serialize(instance));
                            }

                            // Don't write RenderBitmapBytes when the length is incorrect
                            if (instance.RenderBitmapBytes != null && instance.RenderBitmapBytes.Length == (instance.Width * instance.Height + 7) / 8)
                            {
                                instance.OffsetExistsBitmap = (uint)bw.BaseStream.Position;
                                bw.Write(instance.RenderBitmapBytes);
                            }
                            else
                                instance.OffsetExistsBitmap = 0;
                        }

                        // Write MH2O attributes
                        if (header.Attributes is null || header.Attributes.HasOnlyZeroes)
                            header.OffsetAttributes = 0;
                        else
                        {
                            header.OffsetAttributes = (uint)bw.BaseStream.Position;
                            bw.Write(header.Attributes.Serialize());
                        }
                    }
                    else
                    {
                        header.OffsetAttributes = 0;
                        header.OffsetInstances = 0;
                    }
                }

                // Write MH2O instance data
                foreach (var header in MH2OHeaders)
                {
                    bw.BaseStream.Position = header.OffsetInstances;
                    foreach (var instance in header.Instances)
                        bw.Write(instance.Serialize());
                }

                // Write MH2O header data
                bw.Seek(0, SeekOrigin.Begin);
                foreach (var header in MH2OHeaders)
                    bw.Write(header.Serialize());

                return ms.ToArray();
            }
        }
    }
}
