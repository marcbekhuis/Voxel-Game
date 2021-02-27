using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;

    private Vector3Int lastPlayerGridPosition;
    private int vertexIndex = 0;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    public void RenderChunk(Vector3Int chunkPosition)
    {
        vertexIndex = 0;
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();

        int halfsize = Mathf.RoundToInt(MapRenderer.instance.chunksize / 2f + MapRenderer.instance.chunksize % 2f);
        for (int x = -halfsize; x <= halfsize; x++)
        {
            for (int z = -halfsize; z <= halfsize; z++)
            {
                for (int y = 0; y < MapData.instance.maxHeight; y++)
                {
                    Vector3Int blockPosition = chunkPosition * MapRenderer.instance.chunksize + new Vector3Int(x, y, z);
                    if (MapData.instance.blocks.ContainsKey(blockPosition))
                    {
                        AddVoxel(new Vector3Int(x,y,z), MapData.instance.blocks[blockPosition]);
                    }
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    private void AddVoxel(Vector3Int voxelPosition, BlockData blockData)
    {
        foreach (var voxelFace in blockData.faceVisible)
        {
            if(voxelFace.Value) AddFace(voxelFace.Key, voxelPosition, blockData);
        }
    }

    private void AddFace(string face, Vector3Int voxelPosition, BlockData blockData)
    {
        for (int i = 0; i < 6; i++)
        {
            vertices.Add(voxelPosition + VoxelData.voxelVerts[VoxelData.voxelFace[face][i]]);
            triangles.Add(vertexIndex);

            uvs.Add(SpriteHandeler.instance.GetUvPosition(i, blockData.blockObject.GetSpritePosition(face)));

            vertexIndex++;
        }
    }
}
