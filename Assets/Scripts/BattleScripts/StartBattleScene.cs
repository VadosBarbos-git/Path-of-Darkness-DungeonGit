using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattleScene : MonoBehaviour
{
    public GameObject BattleSceneObject;
    public GameObject GlobalMapObject;
    public ArenaControler arenaControler;
    [HideInInspector] public static CharactersDescription Player;
    [HideInInspector] public static CharactersDescription Enemy;
    private GameObject EnemyObj;
    public static Sprite spriteEnemy;
    public void StartScene(CharactersDescription Player, CharactersDescription Enemy, GameObject EnemyObj)
    {
        this.EnemyObj = EnemyObj;
        spriteEnemy = EnemyObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        StartBattleScene.Player = StaticCharacterPlayer.Player;
        StartBattleScene.Enemy = Enemy;
        GlobalMapObject.SetActive(false);
        BattleSceneObject.SetActive(true);
    }
    public void CloseSceneAndOpenMainScene()
    {

        Destroy(EnemyObj);
        GlobalMapObject.SetActive(true);
        BattleSceneObject.SetActive(false);
    }
    public void DeathPlayer()
    {
        StaticInventory.BagCells = null;
        StaticInventory.BeltBagCells = null;
        StaticInventory.BodyCells = null;
        SceneManager.LoadScene("MainCamp");
    }


}
