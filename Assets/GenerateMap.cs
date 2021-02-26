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
        //RenderBlocks.instance.Render();
        LandscapeRenderer.instance.Render();
    }

    public void Generate()
    {
        for (int x = -100; x < 100; x++)
        {
            for (int z = -100; z < 100; z++)
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
