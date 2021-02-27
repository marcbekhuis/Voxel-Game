using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandeler : MonoBehaviour
{
    public static SpriteHandeler instance;

    [SerializeField] private Texture spriteSheet;
    [SerializeField] private Vector2Int spriteSize;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public Vector2 GetUvPosition(int triangle, Vector2Int spritePosition)
    {
        Vector2 spriteCount = new Vector2(spriteSheet.width / spriteSize.x, spriteSheet.height / spriteSize.y);
        return Vector2.one / spriteCount * spritePosition + VoxelData.voxelUv[triangle] / spriteCount;
    }
}
