using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeRenderer : MonoBehaviour
{
    public static LandscapeRenderer instance;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private int renderDistance = 20;
    [Space]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;

    private Vector3Int lastPlayerGridPosition;
    private int vertexIndex = 0;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        else instance = this;
        lastPlayerGridPosition = new Vector3Int(0, -1000, 0);
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
        RenderLandscape();
    }

    public void RenderLandscape()
    {
        vertexIndex = 0;
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();

        int minRenderPositionX = Mathf.RoundToInt(playerTransform.position.x) - renderDistance;
        int minRenderPositionZ = Mathf.RoundToInt(playerTransform.position.z) - renderDistance;
        int maxRenderPositionX = Mathf.RoundToInt(playerTransform.position.x) + renderDistance;
        int maxRenderPositionZ = Mathf.RoundToInt(playerTransform.position.z) + renderDistance;
        for (int x = minRenderPositionX; x < maxRenderPositionX; x++)
        {
            for (int z = minRenderPositionZ; z < maxRenderPositionZ; z++)
            {
                for (int y = 0; y < MapData.instance.maxHeight; y++)
                {
                    if (MapData.instance.blocks.ContainsKey(new Vector3Int(x, y, z)))
                    {
                        AddVoxel(new Vector3Int(x,y,z));
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

    private void AddVoxel(Vector3Int voxelPosition)
    {
        foreach (var voxelFace in MapData.instance.blocks[voxelPosition].faceVisible)
        {
            if(voxelFace.Value) AddFace(voxelFace.Key, voxelPosition);
        }
    }

    private void AddFace(string face, Vector3Int voxelPosition)
    {
        for (int i = 0; i < 6; i++)
        {
            vertices.Add(voxelPosition + VoxelData.voxelVerts[VoxelData.voxelFace[face][i]]);
            triangles.Add(vertexIndex);

            uvs.Add(Vector2.zero);

            vertexIndex++;
        }
    }
}
