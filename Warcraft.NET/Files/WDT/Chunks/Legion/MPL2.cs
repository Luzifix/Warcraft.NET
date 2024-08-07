﻿using Warcraft.NET.Files.Interfaces;
using System.IO;
using System.Collections.Generic;
using Warcraft.NET.Files.WDT.Entries.Legion;
using Warcraft.NET.Attribute;

namespace Warcraft.NET.Files.WDT.Chunks.Legion
{
    /// <summary>
    /// MPL2 Chunk - Contains Legion point light information
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class MPL2 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MPL2";

        public List<MPL2Entry> Entries = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MPL2"/> class.
        /// </summary>
        public MPL2()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPL2"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MPL2(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mpl2Count = br.BaseStream.Length / MPL2Entry.GetSize();

                for (var i = 0; i < mpl2Count; ++i)
                {
                    Entries.Add(new MPL2Entry(br.ReadBytes(MPL2Entry.GetSize())));
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
                foreach (MPL2Entry mpl2Entry in Entries)
                {
                    bw.Write(mpl2Entry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}