﻿using SharpDX;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.ADT.Terrain.MCMK.Flags;

namespace Warcraft.NET.Files.ADT.Terrain.MCMK
{
    /// <summary>
    /// Header data for a map chunk.
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Gets or sets flags for the MCNK.
        /// </summary>
        public MCNKFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the zero-based X position of the MCNK.
        /// </summary>
        public uint MapIndexX { get; set; }

        /// <summary>
        /// Gets or sets the zero-based Y position of the MCNK.
        /// </summary>
        public uint MapIndexY { get; set; }

        /// <summary>
        /// Gets or sets the number of alpha map layers in the MCNK.
        /// </summary>
        public uint TextureLayerCount { get; set; }

        /// <summary>
        /// Gets or sets the number of doodad references in the MCNK.
        /// </summary>
        public uint ModelReferenceCount { get; set; }

        /// <summary>
        /// Gets or sets the high res holes. This is a bitmap of boolean values.
        /// </summary>
        public ulong HighResHoles { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCVT Heightmap Chunk.
        /// </summary>
        public uint HeightmapOffset { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MMCNR Normal map Chunk.
        /// </summary>
        public uint VertexNormalOffset { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCLY Alpha Map Layer Chunk.
        /// </summary>
        public uint TextureLayersOffset { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCRF Object References Chunk.
        /// </summary>
        public uint ModelReferencesOffset { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCAL Alpha Map Chunk.
        /// </summary>
        public uint AlphaMapsOffset { get; set; }

        /// <summary>
        /// Gets or sets the size of the Alpha Map chunk.
        /// </summary>
        public uint AlphaMapsSize { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCSH Static shadow Chunk. This is only set with flags MCNK_MCSH.
        /// </summary>
        public uint BakedShadowsOffset { get; set; }

        /// <summary>
        /// Gets or sets the size of the shadow map chunk.
        /// </summary>
        public uint BakedShadowsSize { get; set; }

        /// <summary>
        /// Gets or sets the area ID for the MCNK.
        /// </summary>
        public uint AreaID { get; set; }

        /// <summary>
        /// Gets or sets the number of object references in this MCNK.
        /// </summary>
        public uint WorldModelObjectReferenceCount { get; set; }

        /// <summary>
        /// Gets or sets low-resolution holes in the MCNK. This is a bitmap of boolean values.
        /// </summary>
        public ushort LowResHoles { get; set; }

        /// <summary>
        /// Gets or sets an unknown value. It is used, but it's unclear for what.
        /// </summary>
        public ushort Unknown { get; set; }

        /// <summary>
        /// Gets or sets a low-quality texture map of the MCNK. Used with LOD.
        /// </summary>
        public ushort LowResTextureMap { get; set; }

        /// <summary>
        /// Gets or sets it PredTex. It is not yet known what PredTex does.
        /// </summary>
        public uint PredTex { get; set; }

        /// <summary>
        /// Gets or sets NoEffectDoodad. It is not yet known what NoEffectDoodad does.
        /// </summary>
        public uint NoEffectDoodad { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCSE Sound Emitters Chunk.
        /// </summary>
        public uint SoundEmittersOffset { get; set; }

        /// <summary>
        /// Gets or sets the number of sound emitters in the MCNK.
        /// </summary>
        public uint SoundEmitterCount { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCLQ Liquid Chunk.
        /// </summary>
        public uint LiquidOffset { get; set; }

        /// <summary>
        /// Gets or sets the size of the liquid chunk. This is 8 when not used - if it is 8, use the newer MH2O chunk.
        /// </summary>
        public uint LiquidSize { get; set; }

        /// <summary>
        /// Gets or sets the map tile position. This position is a global offset that is applied to the entire heightmap
        /// to allow for far greater height differences in the world.
        /// </summary>
        public Vector3 MapTilePosition { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCCV Chunk.
        /// </summary>
        public uint VertexShadingOffset { get; set; }

        /// <summary>
        /// Gets or sets the relative offset of the MCLV Chunk. Introduced in Cataclysm.
        /// </summary>
        public uint VertexLightingOffset { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public Header(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    Flags = (MCNKFlags)br.ReadUInt32();
                    MapIndexX = br.ReadUInt32();
                    MapIndexY = br.ReadUInt32();
                    TextureLayerCount = br.ReadUInt32();
                    ModelReferenceCount = br.ReadUInt32();

                    if (Flags.HasFlag(MCNKFlags.UsesHighResHoles))
                    {
                        HighResHoles = br.ReadUInt64();
                    }

                    HeightmapOffset = br.ReadUInt32();
                    VertexNormalOffset = br.ReadUInt32();
                    TextureLayersOffset = br.ReadUInt32();
                    ModelReferencesOffset = br.ReadUInt32();
                    AlphaMapsOffset = br.ReadUInt32();
                    AlphaMapsSize = br.ReadUInt32();
                    BakedShadowsOffset = br.ReadUInt32();
                    BakedShadowsSize = br.ReadUInt32();
                    AreaID = br.ReadUInt32();
                    WorldModelObjectReferenceCount = br.ReadUInt32();

                    if (!Flags.HasFlag(MCNKFlags.UsesHighResHoles))
                    {
                        LowResHoles = br.ReadUInt16();
                    }

                    Unknown = br.ReadUInt16();
                    LowResTextureMap = br.ReadUInt16();
                    PredTex = br.ReadUInt32();
                    NoEffectDoodad = br.ReadUInt32();
                    SoundEmittersOffset = br.ReadUInt32();
                    SoundEmitterCount = br.ReadUInt32();
                    LiquidOffset = br.ReadUInt32();
                    LiquidSize = br.ReadUInt32();
                    MapTilePosition = br.ReadVector3();

                    if (Flags.HasFlag(MCNKFlags.HasVertexShading))
                    {
                        VertexShadingOffset = br.ReadUInt32();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 128;
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((uint)Flags);
                bw.Write(MapIndexX);
                bw.Write(MapIndexY);
                bw.Write(TextureLayerCount);
                bw.Write(ModelReferenceCount);

                if (Flags.HasFlag(MCNKFlags.UsesHighResHoles))
                {
                    bw.Write(HighResHoles);
                }

                bw.Write(HeightmapOffset);
                bw.Write(VertexNormalOffset);
                bw.Write(TextureLayersOffset);
                bw.Write(ModelReferencesOffset);
                bw.Write(AlphaMapsOffset);
                bw.Write(AlphaMapsSize);
                bw.Write(BakedShadowsOffset);
                bw.Write(BakedShadowsSize);
                bw.Write(AreaID);
                bw.Write(WorldModelObjectReferenceCount);

                if (!Flags.HasFlag(MCNKFlags.UsesHighResHoles))
                {
                    bw.Write(LowResHoles);
                }

                bw.Write(Unknown);
                bw.Write(LowResTextureMap);
                bw.Write(PredTex);
                bw.Write(NoEffectDoodad);
                bw.Write(SoundEmittersOffset);
                bw.Write(SoundEmitterCount);
                bw.Write(LiquidOffset);
                bw.Write(LiquidSize);
                bw.WriteVector3(MapTilePosition);

                if (Flags.HasFlag(MCNKFlags.HasVertexShading))
                {
                    bw.Write(VertexShadingOffset);
                }

                return ms.ToArray();
            }
        }
    }
}