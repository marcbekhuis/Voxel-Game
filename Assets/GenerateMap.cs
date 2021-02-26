using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private float scale = 1;
    [SerializeField] private int seed = 0;
    [SerializeField] private 

    // Start is called before the first frame update
    void Start()
    {
        Generate();
        RenderBlocks.instance.Render();
    }

    public void Generate()
    {
        for (int x = -1000; x < 1000; x++)
        {
            for (int z = -1000; z < 1000; z++)
            {
                int maxY = Mathf.RoundToInt(Mathf.Pow(Mathf.PerlinNoise(x / scale + seed, z / scale + seed), 2) * MapData.instance.maxLandscapeHeight);
                for (int y = maxY; y >= MapData.instance.maxDepth; y--)
                {
                    MapData.instance.blocks.Add(new Vector3Int(x, y, z), new BlockData(null));
                }
            }
        }
    }
}
