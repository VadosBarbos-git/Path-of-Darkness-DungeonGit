using UnityEngine;

public class StartSceneMainCamp : MonoBehaviour
{
    public Animator animatorPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (StaticCharacterPlayer.Player == null)
        {
            StaticCharacterPlayer.Player = new SoliderDes();
        }
       
        animatorPlayer.SetTrigger("EndGanarateMap");
        StaticInventory.StartSameScene(); 
    } 
}
