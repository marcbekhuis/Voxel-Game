using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private float scale = 1;
    [SerializeField] private int seed = 0;
    [SerializeField] private Vector2Int minMapsize = new Vector2Int(-100, -100);
    [SerializeField] private Vector2Int maxMapsize = new Vector2Int(100, 100);

    // Start is called before the first frame update
    void Start()
    {
        Generate();
        MapRenderer.instance.Render();
    }

    public void Generate()
    {
        for (int x = minMapsize.x; x < maxMapsize.x; x++)
        {
            for (int z = minMapsize.y; z < maxMapsize.y; z++)
            {
                int maxY = Mathf.RoundToInt(Mathf.Pow(Mathf.PerlinNoise(x / scale + seed, z / scale + seed), 2) * MapData.instance.maxLandscapeHeight);
                for (int y = maxY; y >= MapData.instance.maxDepth; y--)
                {
                    Vector3Int position = new Vector3Int(x, y, z);
                    BlockData block = new BlockData(position);
                    block.SetFaceVisible(position, new Vector3Int(-1, 0, 0));
                    block.SetFaceVisible(position, new Vector3Int(0, 0, -1));
                    block.SetFaceVisible(position, new Vector3Int(0, 1, 0));
                    MapData.instance.blocks.Add(position, block);
                }
            }
        }
    }
}
