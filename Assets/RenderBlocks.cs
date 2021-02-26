using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBlocks : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject cube;
    [SerializeField] private int renderDistance = 20;

    private Vector3Int lastPlayerGridPosition;

    public static RenderBlocks instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        else instance = this;

        lastPlayerGridPosition = new Vector3Int(0,-1000,0);
    }

    private void Update()
    {
        Render();
    }

    public void Render()
    {
        Vector3Int currentPlayerGridPosition = Vector3Int.RoundToInt(playerTransform.position);
        if (lastPlayerGridPosition == currentPlayerGridPosition) return;
        lastPlayerGridPosition = currentPlayerGridPosition;
        UnloadBlocks();
        LoadBlock();
    }

    private void UnloadBlocks()
    {
        if (MapData.instance.renderedBlocks.Count <= 0) return;
        int minRenderPositionX = Mathf.RoundToInt(playerTransform.position.x) - renderDistance;
        int minRenderPositionZ = Mathf.RoundToInt(playerTransform.position.z) - renderDistance;
        int maxRenderPositionX = Mathf.RoundToInt(playerTransform.position.x) + renderDistance;
        int maxRenderPositionZ = Mathf.RoundToInt(playerTransform.position.z) + renderDistance;
        Vector3Int cameraPosition = Vector3Int.RoundToInt(cameraTransform.position);
        List<Vector3Int> tempData = new List<Vector3Int>();
        foreach (var renderedBlock in MapData.instance.renderedBlocks)
        {
            if (renderedBlock.Key.x >= minRenderPositionX && renderedBlock.Key.x < maxRenderPositionX)
                if (renderedBlock.Key.z >= minRenderPositionZ && renderedBlock.Key.z < maxRenderPositionZ) continue;
            Destroy(renderedBlock.Value.block);
            renderedBlock.Value.block = null;
            tempData.Add(renderedBlock.Key);
        }
        foreach (var item in tempData)
        {
            MapData.instance.renderedBlocks.Remove(item);
        }
    }

    private void LoadBlock()
    {
        int minRenderPositionX = Mathf.RoundToInt(playerTransform.position.x) - renderDistance;
        int minRenderPositionZ = Mathf.RoundToInt(playerTransform.position.z) - renderDistance;
        int maxRenderPositionX = Mathf.RoundToInt(playerTransform.position.x) + renderDistance;
        int maxRenderPositionZ = Mathf.RoundToInt(playerTransform.position.z) + renderDistance;
        Vector3Int cameraPosition = Vector3Int.RoundToInt(cameraTransform.position);
        for (int x = minRenderPositionX; x < maxRenderPositionX; x++)
        {
            for (int z = minRenderPositionZ; z < maxRenderPositionZ; z++)
            {
                for (int y = 0; y < MapData.instance.maxHeight; y++)
                {
                    if(MapData.instance.blocks.ContainsKey(new Vector3Int(x, y, z)))
                    {
                        if (MapData.instance.renderedBlocks.ContainsKey(new Vector3Int(x, y, z))) continue;
                        if (!BlockHasAirAround(new Vector3Int(x, y, z))) continue;
                        GameObject placedCube = Instantiate(cube, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0));
                        MapData.instance.blocks[new Vector3Int(x, y, z)].block = placedCube;
                        MapData.instance.renderedBlocks.Add(new Vector3Int(x, y, z), MapData.instance.blocks[new Vector3Int(x, y, z)]);
                    }
                }
            }
        }
    }

    private bool BlockHasAirAround(Vector3Int blockPosition)
    {
        if (!MapData.instance.blocks.ContainsKey(blockPosition + new Vector3Int(1, 0, 0))) return true;
        if (!MapData.instance.blocks.ContainsKey(blockPosition + new Vector3Int(-1, 0, 0))) return true;
        if (!MapData.instance.blocks.ContainsKey(blockPosition + new Vector3Int(0, 0, 1))) return true;
        if (!MapData.instance.blocks.ContainsKey(blockPosition + new Vector3Int(0, 0, -1))) return true;
        if (!MapData.instance.blocks.ContainsKey(blockPosition + new Vector3Int(0, 1, 0))) return true;
        if (!MapData.instance.blocks.ContainsKey(blockPosition + new Vector3Int(0, -1, 0)))
            if (blockPosition.y > MapData.instance.maxDepth) return true; 
        return false;
    }
}
