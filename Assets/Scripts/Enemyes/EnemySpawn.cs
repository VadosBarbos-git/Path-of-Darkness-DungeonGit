
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public MapRendering MapRendr;
    public List<GameObject> Enemyes;
    public Transform FatherEnemiesTransform;
    public void SpawnMob()
    {
        for (int i = 0; i < MapRendr.SpawnPlaysDeadEndVectors2.Count; i++)
        {
            Instantiate(Enemyes[Random.Range(0, Enemyes.Count)], MapRendr.SpawnPlaysDeadEndVectors2[i], Quaternion.identity, FatherEnemiesTransform);
        }
    }
}
