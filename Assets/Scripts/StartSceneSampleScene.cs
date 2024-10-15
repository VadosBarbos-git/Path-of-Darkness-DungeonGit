
using UnityEngine;

public class StartSceneSampleScene : MonoBehaviour
{
    public MapRendering mapRendring;
    public EnemySpawn spawn;
    public SpawnChest spawnChest;
    public PanelInventoryArena arenaInventory;
    int timer = 100;
    public Animator animatorPlayer;


    void Start()
    {
        SceneTransition.WaitForSameTime = true;
        if (StaticCharacterPlayer.Player == null)
        {
            StaticCharacterPlayer.Player = new SoliderDes();
        }
        MapRendering.seed = Random.Range(0, 12232);
        //mapRendring.ReadyMap += ReadyMapCriate;
        mapRendring.GenerateFloor();
        StaticInventory.StartSameScene();
    }

    void ReadyMapCriate()
    {
        MapRendering.Grid = CriateGrid.Grid(MapRendering.MainMap, MapRendering.MainWalls, MapRendering.PosChest);
        spawn.SpawnMob();
    }
    //Этот метод вызывается после создания  Карты 
    public void EndCriateMap()
    {
        spawnChest.Spawn();
        ReadyMapCriate();

        SceneTransition.OpenSceneAfterWaitSameTime(); 
        animatorPlayer.SetTrigger("EndGanarateMap");
    }



}
