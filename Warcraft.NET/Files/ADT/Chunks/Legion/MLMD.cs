using Warcraft.NET.Files.ADT.Entrys.Legion;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLMD Chunk - Contains wmo placement information like <see cref="MODF"/> without extens
    /// </summary>
    public class MLMD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLMD";

        /// <summary>
        /// Gets or sets model extents.
        /// </summary>
        public List<MLMDEntry> Entries { get; set; } = new List<MLMDEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MLMD"/> class.
        /// </summary>
        public MLMD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLMD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLMD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var entryCount = br.BaseStream.Length / MLMDEntry.GetSize();

                for (var i = 0; i < entryCount; ++i)
                {
                    Entries.Add(new MLMDEntry(br.ReadBytes(MLMDEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (MLMDEntry entry in Entries)
                {
                    bw.Write(entry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}