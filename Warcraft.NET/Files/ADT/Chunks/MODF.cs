﻿using Warcraft.NET.Files.ADT.Entries;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;

namespace Warcraft.NET.Files.ADT.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAll)]
    public class MODF : IIFFChunk, IBinarySerializable
    {
        public const string Signature = "MODF";

        /// <summary>
        /// Gets or sets <see cref="MODFEntry"/>s.
        /// </summary>
        public List<MODFEntry> MODFEntries { get; set; } = new();

        public MODF()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODF"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MODF(byte[] inData)
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
                var objCount = br.BaseStream.Length / MODFEntry.GetSize();

                for (var i = 0; i < objCount; ++i)
                {
                    MODFEntries.Add(new MODFEntry(br.ReadBytes(MODFEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (MODFEntry obj in MODFEntries)
                {
                    bw.Write(obj.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}
