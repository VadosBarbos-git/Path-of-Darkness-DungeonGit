
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class SpawnChest : MonoBehaviour
{
    public GameObject ChestPrefab;
    private List<Vector2Int> posChest = new();
    Transform _parentDroppedItemsTransform;
    public void Start()
    {
        _parentDroppedItemsTransform = GameObject.FindGameObjectWithTag("ParentDroppedItems").transform;
    }

    public void Spawn()
    {
        posChest.Clear();
       
        for (int i = 0; i < MapRendering.MainMap.Count; i++)
        {
            if (ChacPoint(MapRendering.MainMap[i]))
            {
                Instantiate(ChestPrefab, new Vector2(MapRendering.MainMap[i].x, MapRendering.MainMap[i].y), Quaternion.identity, _parentDroppedItemsTransform);
                posChest.Add(MapRendering.MainMap[i]);
            }
        }
    }
    bool ChacPoint(Vector2Int pos)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int curVec = new Vector2Int(pos.x + x, pos.y + y);
                //чтоб сундуки не стояли близко 
                for (int i = 0; i < posChest.Count; i++)
                {
                    if (posChest.Count > 0 && Vector2.Distance(curVec, posChest[i]) < 10)
                    {
                        return false;
                    }
                }

                if (!MapRendering.MainMap.Any(vec => vec == curVec) ||
                    posChest.Any(vec => vec == curVec) ||
                    curVec == new Vector2Int(0, 0) || MapRendering.PortalPos == curVec)
                {
                    return false;
                }

                if (Random.Range(0, 100) < 40)
                {
                    return false;
                }

            }
        }
        return true;
    }
}
