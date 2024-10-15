using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;
    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }
    public Node(bool walkable, Vector2Int worldPosition)
    {
        this.walkable = walkable;
        this.worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
        this.gridX = worldPosition.x;
        this.gridY = worldPosition.y;
    }
    public int fCost
    {
        get { return gCost + hCost; }
    }
 
}
