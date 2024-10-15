
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapRendering : MonoBehaviour
{
    public GeneratorMap genMap;
    public Transform FatherFloors;
    public Transform FatherWalls;
    public GameObject Square;
    public GameObject PortalToHome;
    public StartSceneSampleScene startSceneSampleScene;

    public List<GameObject> Floors;
    public List<WallOptions> WallOptions;
    public List<WallOptions> WaterOptions;
    public List<GameObject> WallsNomberFour;

    [HideInInspector]
    public List<Vector2> SpawnPlaysDeadEndVectors2;

    public static List<Vector2Int> MainMap = new List<Vector2Int>();
    public static List<Vector2Int> PosChest = new List<Vector2Int>();
    public static List<Wall> MainWalls = new();
    public static List<Node> Grid = new List<Node>();
    public static Vector2Int PortalPos = new Vector2Int();



    public delegate void EventReadyMap();
    //public event EventReadyMap ReadyMap;



    public static int seed = 12;
    public GeneratorMap.dangType dangTypee;

    private void OnDisable()
    {
        MainMap.Clear();
        MainWalls.Clear();
        Grid.Clear();
        PortalPos = new Vector2Int(0, 0);

    }
    public void GenerateFloor()
    {
        genMap.onAddPoint += AddPoint;
        genMap.onDeadEnd += DeadEnd;
        genMap.Generate(dangTypee, seed, Ready);
    }
    void Ready(bool ok, Vector2Int finish)
    {
        CreatingAndRenderingWalls(); 
        PortalPos = new Vector2Int(finish.x, finish.y);
        Instantiate(PortalToHome, new Vector2(finish.x, finish.y), Quaternion.identity);
        startSceneSampleScene.EndCriateMap();
    }
    void AddPoint(string key, int X, int Y)
    {
        MainMap.Add(new Vector2Int(X, Y));
        Instantiate(Floors[ChoisFloor()], new Vector3(X, Y, 0), Quaternion.identity, FatherFloors);
    }
    void DeadEnd(string key, int X, int Y)
    {
        SpawnPlaysDeadEndVectors2.Add(new Vector2Int(X, Y));
    }

    void CreatingAndRenderingWalls()
    {
        MainWalls = CreatingWall.Create(MainMap);
        foreach (var wall in MainWalls)
        {
            GameObject obj;

            if (wall.type == TypeWall.water)
            {
                obj = FindWallOptions(wall.value, WaterOptions);
            }
            else if (wall.type == TypeWall.nomberFour)
            {
                obj = Random.Range(1, 101) < 60 ? WallsNomberFour[0] :
                    WallsNomberFour[Random.Range(0, WallsNomberFour.Count)];
            }
            else
            {
                obj = FindWallOptions(wall.value, WallOptions);
            }

            if (obj != null)
            {
                Instantiate(obj, new Vector3(wall.vector2Int.x, wall.vector2Int.y, 0), Quaternion.identity, FatherWalls);
            }

        }

        // AddYToMainMapAfterMakeWall();
    }

    GameObject FindWallOptions(int value, List<WallOptions> Options)
    {

        WallOptions wall = Options.FirstOrDefault(wall => wall.possibleValue.Any(i => i == value));
        if (wall != null && wall.Prefab != null)
        {
            return wall.Prefab.gameObject;
        }
        else
        {
            return Square;
        }
    }

    int ChoisFloor()
    {
        int i = Random.Range(1, 101);
        if (i < 85)
        {
            return Random.Range(0, 2);
        }
        else if (i < 92)
        {
            return Random.Range(2, 4);
        }
        else
        {
            if (i % 2 == 0)
            {
                return 6;
            }
            else
            {
                return Random.Range(4, 7);
            }
        }
    }
    private void AddYToMainMapAfterMakeWall()
    {
        List<Vector2Int> BonusMap = new List<Vector2Int>();
        int minValueX = 0;
        int maxValueX = 0;

        int minValueY = 0;
        int maxValueY = 0;
        foreach (var item in MainMap)
        {
            if (minValueX > item.x)
                minValueX = item.x;

            if (maxValueX < item.x)
                maxValueX = item.x;

            if (minValueY > item.y)
                minValueY = item.y;

            if (maxValueY < item.y)
                maxValueY = item.y;
        }
        bool MakePoint = false;
        for (int x = minValueX; x <= maxValueX; x++)
        {
            for (int y = maxValueY; y >= minValueY - 1; y--)
            {
                if (MakePoint && !MainMap.Contains(new Vector2Int(x, y)) && !BonusMap.Contains(new Vector2Int(x, y)))
                {

                    MakePoint = false;
                    if (!MainMap.Contains(new Vector2Int(x, y - 1)))
                    {
                        BonusMap.Add(new Vector2Int(x, y));
                    }
                }


                if (MainMap.Contains(new Vector2Int(x, y)))
                {

                    MakePoint = true;
                }
            }
        }

        // MainMap.AddRange(BonusMap);
    }


}
