using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "ScriptableObjects/Block")]
public class BlockObject : ScriptableObject
{
    public string name = "new block";
    public Vector2Int topSpritePosition = new Vector2Int(0,0);
    public Vector2Int bottomSpritePosition = new Vector2Int(0, 0);
    public Vector2Int frontSpritePosition = new Vector2Int(0, 0);
    public Vector2Int backSpritePosition = new Vector2Int(0, 0);
    public Vector2Int leftSpritePosition = new Vector2Int(0, 0);
    public Vector2Int rightSpritePosition = new Vector2Int(0, 0);

    public Vector2Int GetSpritePosition(string face)
    {
        switch (face)
        {
            case "top":
                return topSpritePosition;
            case "bottom":
                return bottomSpritePosition;
            case "front":
                return frontSpritePosition;
            case "back":
                return backSpritePosition;
            case "left":
                return leftSpritePosition;
            case "right":
                return rightSpritePosition;
            default:
                return topSpritePosition;
        }
    }
}
