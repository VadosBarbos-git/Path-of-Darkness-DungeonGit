using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public static class CriateGrid
{
    public static List<Node> Grid(List<Vector2Int> mainMap, List<Wall> walls, List<Vector2Int> Chests)
    {
        List<Node> list = new List<Node>();

        for (int i = 0; i < mainMap.Count; i++)
        {
            if (Chests.Any(chest => chest == mainMap[i]))
            {
                list.Add(new Node(false, mainMap[i]));
            }
            else
            {
                list.Add(new Node(true, mainMap[i])); 
            }
        }
        foreach (var item in walls)
        {
            list.Add(new Node(false, item.vector2Int));
        }
        return list;
    }

}
