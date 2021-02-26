using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData
{
    public BlockData(Vector3Int position)
    {
        this.position = position;
    }

    public Vector3Int position;
    public Dictionary<string, bool> faceVisible = new Dictionary<string, bool>
    {
        {"back", true }, // back Face
        {"front", true}, // Front Face
        {"top", true }, // Top Face
        {"bottom", true }, // Bottom Face
        {"left", true }, //Left Face
        {"right", true }// Right Face
    };

    public void SetFaceVisible(Vector3Int voxelPosition, Vector3Int voxelNeighborOffset)
    {
        if (MapData.instance.blocks.ContainsKey(voxelPosition + voxelNeighborOffset))
        {
            this.faceVisible[VoxelData.voxelOffsetToDirection[voxelNeighborOffset]] = false;
            MapData.instance.blocks[voxelPosition + voxelNeighborOffset].faceVisible[VoxelData.voxelOffsetToDirection[voxelNeighborOffset * -1]] = false;
        }
        else
        {
            this.faceVisible[VoxelData.voxelOffsetToDirection[voxelNeighborOffset]] = true;
        }
    }
}
