using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private HeightNoise heightNoise;
    [SerializeField] private Vector2Int minMapsize = new Vector2Int(-100, -100);
    [SerializeField] private Vector2Int maxMapsize = new Vector2Int(100, 100);
    [SerializeField] private BlockObject grass;
    [SerializeField] private BlockObject dirt;
    [SerializeField] private BlockObject stone;

    // Start is called before the first frame update
    void Start()
    {
        heightNoise.Setup();
        DebugDuration.instance.DebugProcessDuration("Setup HeightNoise");
        Generate();
        DebugDuration.instance.DebugProcessDuration("Generate Landscape");
        MapRenderer.instance.Render();
    }

    public void Generate()
    {
        for (int x = minMapsize.x; x < maxMapsize.x; x++)
        {
            for (int z = minMapsize.y; z < maxMapsize.y; z++)
            {
                int maxY = heightNoise.GetValue(x, z, MapData.instance.maxLandscapeHeight);
                int depth = 0;
                for (int y = maxY; y >= MapData.instance.maxDepth; y--)
                {
                    Vector3Int position = new Vector3Int(x, y, z);
                    BlockData block = null;
                    if (depth <= 0) block = new BlockData(position, grass);
                    else if (depth <= 3) block = new BlockData(position, dirt);
                    else block = new BlockData(position, stone);

                    block.SetFaceVisible(position, new Vector3Int(-1, 0, 0));
                    block.SetFaceVisible(position, new Vector3Int(0, 0, -1));
                    block.SetFaceVisible(position, new Vector3Int(0, 1, 0));
                    MapData.instance.blocks.Add(position, block);
                    depth++;
                }
            }
        }
    }
}
