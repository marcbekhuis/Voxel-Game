using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    public static MapRenderer instance;

    public int chunkRenderDistance = 2;
    public int chunksize = 15;
    public Dictionary<Vector2Int, ChunkRenderer> chunks = new Dictionary<Vector2Int, ChunkRenderer>();

    [SerializeField] private ChunkRenderer chunkPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform chunksParent;

    private Vector2Int lastPlayerChunkPosition = new Vector2Int(100, 100);
    private Queue<ChunkRenderer> unUsedChunks = new Queue<ChunkRenderer>();

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Update()
    {
        Render();
    }

    public void Render()
    {
        Vector3Int tempPosition = Vector3Int.RoundToInt(playerTransform.position / chunksize);
        Vector2Int currentPlayerChunkPosition = new Vector2Int(tempPosition.x, tempPosition.z);
        if (currentPlayerChunkPosition == lastPlayerChunkPosition) return;
        StopAllCoroutines();
        GetUnUsedChunks(currentPlayerChunkPosition);
        StartCoroutine(RenderChunks(currentPlayerChunkPosition));
        lastPlayerChunkPosition = currentPlayerChunkPosition;
    }

    private IEnumerator RenderChunks(Vector2Int currentPlayerChunkPosition)
    {
        for (int y = currentPlayerChunkPosition.y - chunkRenderDistance; y <= currentPlayerChunkPosition.y + chunkRenderDistance; y++)
        {
            for (int x = currentPlayerChunkPosition.x - chunkRenderDistance; x <= currentPlayerChunkPosition.x + chunkRenderDistance; x++)
            {
                if (!InsideCircle(currentPlayerChunkPosition, new Vector2(x, y), chunkRenderDistance + 0.5f)) continue;
                Vector2Int chunkPosition = new Vector2Int(x,y);
                if (!chunks.ContainsKey(chunkPosition))
                    if (unUsedChunks.Count > 0) ReUseChunk(chunkPosition);
                    else CreateChunk(chunkPosition);
                yield return null;
            }
        }

        StartCoroutine(DestroyUnUsedChunks());
    }

    private void GetUnUsedChunks(Vector2Int currentPlayerChunkPosition)
    {
        List<Vector2Int> keys = new List<Vector2Int>();
        foreach (var chunk in chunks)
        {
            if (chunk.Key.x >= currentPlayerChunkPosition.x - chunkRenderDistance && chunk.Key.x <= currentPlayerChunkPosition.x + chunkRenderDistance)
                if (chunk.Key.y >= currentPlayerChunkPosition.y - chunkRenderDistance && chunk.Key.y <= currentPlayerChunkPosition.y + chunkRenderDistance) continue;
            unUsedChunks.Enqueue(chunk.Value);
            keys.Add(chunk.Key);
        }
        foreach (var key in keys)
        {
            chunks.Remove(key);
        }
    }

    private void CreateChunk(Vector2Int chunkPosition)
    {
        Debug.LogError("Create Chunk");
        ChunkRenderer placedChunk = Instantiate(chunkPrefab, chunksParent);
        placedChunk.transform.position = new Vector3(chunkPosition.x, 0, chunkPosition.y) * chunksize;
        chunks.Add(chunkPosition, placedChunk);
        placedChunk.RenderChunk(new Vector3Int(chunkPosition.x, 0 , chunkPosition.y));
    }

    private void ReUseChunk(Vector2Int chunkPosition)
    {
        Debug.LogError("Reuse Chunk");
        ChunkRenderer placedChunk = unUsedChunks.Dequeue();
        placedChunk.transform.position = new Vector3(chunkPosition.x, 0, chunkPosition.y) * chunksize;
        chunks.Add(chunkPosition, placedChunk);
        placedChunk.RenderChunk(new Vector3Int(chunkPosition.x, 0, chunkPosition.y));
    }

    private bool InsideCircle(Vector2 centerCircle, Vector2 tilePostion, float radius)
    {
        float dx = centerCircle.x - tilePostion.x;
        float dy = centerCircle.y - tilePostion.y;
        float distanceSquared = dx * dx + dy * dy;
        return distanceSquared <= radius * radius;
    }

    private IEnumerator DestroyUnUsedChunks()
    {
        while (unUsedChunks.Count > 0)
        {
            Destroy(unUsedChunks.Dequeue().gameObject);
            yield return null;
        }
    }
}
