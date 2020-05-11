using Warcraft.NET.Files.ADT.Entrys.Legion;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLMD Chunk - Contains m2 placement information like <see cref="MDDF"/>
    /// </summary>
    public class MLDD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLDD";

        /// <summary>
        /// Gets or sets <see cref="MLDDEntry"/>s.
        /// </summary>
        public List<MLDDEntry> MLDDEntrys { get; set; } = new List<MLDDEntry>();

        public MLDD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLDD"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MLDD(byte[] inData)
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
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var doodadCount = br.BaseStream.Length / MLDDEntry.GetSize();

                for (var i = 0; i < doodadCount; ++i)
                {
                    MLDDEntrys.Add(new MLDDEntry(br.ReadBytes(MLDDEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (MLDDEntry doodad in MLDDEntrys)
                {
                    bw.Write(doodad.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}
