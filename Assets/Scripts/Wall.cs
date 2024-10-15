 
using UnityEngine; 
public class Wall
{
    public Vector2Int vector2Int { get; private set; }
    public int value { get; private set; }
    public TypeWall type { get; private set; }

    public void SetValue(int value)
    {
        this.value = value;
    }
    public void SetTWall(TypeWall TypeWall)
    {
        this.type = TypeWall;
    }
    public Wall(Vector2Int vector2Int, TypeWall TypeWall)
    {
        this.vector2Int = vector2Int;
        this.type = TypeWall;
        value = 0;
    }
}

