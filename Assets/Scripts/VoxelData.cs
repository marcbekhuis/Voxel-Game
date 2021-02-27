using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly Vector3[] voxelVerts = new Vector3[8]
    {
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f)
    };

    public static readonly Dictionary<string, int[]> voxelFace = new Dictionary<string, int[]>
    {
        {"back", new int[]{0,3,1,1,3,2 } }, // back Face
        {"front", new int[]{5,6,4,4,6,7 }}, // Front Face
        {"top", new int[]{3, 7, 2, 2, 7, 6} }, // Top Face
        {"bottom", new int[]{1, 5, 0, 0, 5, 4} }, // Bottom Face
        {"left", new int[]{4,7,0,0,7,3 } }, //Left Face
        {"right", new int[]{1,2,5,5,2,6 } }// Right Face
    };

    public static readonly Dictionary<string, Vector3Int> voxelNeighborOffset = new Dictionary<string, Vector3Int>
    {
        {"back", new Vector3Int(0,0,-1) }, // back Face
        {"front", new Vector3Int(0,0,1)}, // Front Face
        {"top", new Vector3Int(0,1,0) }, // Top Face
        {"bottom", new Vector3Int(0,-1,0) }, // Bottom Face
        {"left", new Vector3Int(-1,0,0) }, //Left Face
        {"right", new Vector3Int(1,0,0) }// Right Face
    };

    public static readonly Dictionary<Vector3Int, string> voxelOffsetToDirection = new Dictionary<Vector3Int, string>
    {
        {new Vector3Int(0,0,-1), "back"}, // back Face
        {new Vector3Int(0,0,1), "front"}, // Front Face
        {new Vector3Int(0,1,0), "top"}, // Top Face
        {new Vector3Int(0,-1,0), "bottom"}, // Bottom Face
        {new Vector3Int(-1,0,0), "left"}, //Left Face
        {new Vector3Int(1,0,0), "right"}// Right Face
    };

    public static readonly Vector2[] voxelUv = new Vector2[]
    {
        new Vector2(0.0f, 0.0f), // back Face
        new Vector2(0.0f, 1.0f), // Front Face
        new Vector2(1.0f, 0.0f), // Top Face
        new Vector2(1.0f, 0.0f), // Bottom Face
        new Vector2(0.0f, 1.0f), //Left Face
        new Vector2(1.0f, 1.0f)// Right Face
    };
}
