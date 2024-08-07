﻿using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.WDT.Chunks;
using Warcraft.NET.Files.WDT.Chunks.TWW;

namespace Warcraft.NET.Files.WDT.Fog.TWW
{
    [AutoDocFile("wdt", "_fogs WDT")]
    public class WorldFogTable : BfA.WorldFogTable
    {
        /// <summary>
        /// Gets or sets the VFEX chunks.
        /// </summary>
        public List<VFEX> VFEXList { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        public WorldFogTable() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldFogTable(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public new void LoadBinaryData(byte[] inData)
        {
            using var ms = new MemoryStream(inData);
            using var br = new BinaryReader(ms);

            Version = br.ReadIFFChunk<MVER>(false, false);
            VolumeFogs = br.ReadIFFChunk<VFOG>(false, false);

            if (VolumeFogs != null)
            {
                for (var i = 0; i < VolumeFogs.Entries.Count; i++)
                {
                    VFEXList.Add(br.ReadIFFChunk<VFEX>(false, false));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize()
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms))
            {
                bw.WriteIFFChunk(Version);
                if (VolumeFogs != null)
                {
                    bw.WriteIFFChunk(VolumeFogs);
                    foreach (var vfex in VFEXList)
                    {
                        bw.WriteIFFChunk(vfex);
                    }
                }
            }

            return ms.ToArray();
        }
    }
}
