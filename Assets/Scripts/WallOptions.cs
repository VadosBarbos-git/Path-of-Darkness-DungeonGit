 
using UnityEngine;

[CreateAssetMenu(fileName = "WallOptions", menuName = "ScriptableObjects/WallOptions")]
public class WallOptions : ScriptableObject
{
    public GameObject Prefab;
    public int[] possibleValue;

}
