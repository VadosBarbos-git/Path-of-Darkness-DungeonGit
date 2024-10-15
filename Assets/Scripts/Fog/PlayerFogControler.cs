using System.Collections; 
using UnityEngine;

public class PlayerFogControler : MonoBehaviour
{
    public FogSpriteMask fogSpriteMask;
    public Transform SecondaryFogOfWar;
    [Range(0, 5)]
    public float sightDistance;
    public float checkInterval;

    private void Start()
    {
        StartCoroutine(CheckFogOfWarfloat(checkInterval));
        SecondaryFogOfWar.localScale = new Vector2(sightDistance, sightDistance) * 10f;
    }
    public void Update()
    {

    }
    IEnumerator CheckFogOfWarfloat(float checkInterval)
    {
        while (true)
        {
            fogSpriteMask.MakeHole(transform.position, sightDistance);
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
