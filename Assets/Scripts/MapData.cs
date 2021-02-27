using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public int maxHeight = 100;
    public int maxLandscapeHeight = 100;
    public int maxDepth = 0;
    public Dictionary<Vector3Int, BlockData> blocks = new Dictionary<Vector3Int, BlockData>();
    public Dictionary<Vector3Int, BlockData> renderedBlocks = new Dictionary<Vector3Int, BlockData>();

    public static MapData instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
}
