
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDungeonGenerateReady(bool ok, Vector2Int finish);
public delegate void OnDungeonGenerateAddPoint(string key, int X, int Y);
public delegate void OnDungeonGenerateDeadEnd(string key, int X, int Y);
public class GeneratorMap : MonoBehaviour
{
    const int SQS = 1;//Square size 
    const int MSP = 5;//Max sub Path 
    Vector2Int sizeCave = new Vector2Int(10, 15);//20 30 длина коридоров 
    Vector2Int tot = new Vector2Int(7, 10); //5 10 количество комнат 
    Vector2Int pathS = new Vector2Int(4, 7);//4 7
    Vector2Int sizeRoom = new Vector2Int(1, 3);//1 3


    public Dictionary<string, bool> map { private set; get; }
    public Dictionary<string, Vector2Int> isDeadEnd { private set; get; }
    public List<string> deadEnds { private set; get; }
    OnDungeonGenerateReady readyCallback;
    public event OnDungeonGenerateAddPoint onAddPoint;
    public event OnDungeonGenerateDeadEnd onDeadEnd;
    public int total { get; private set; }
    public int ready { get; private set; }
    public bool nowGenerate { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    int distortion;
    bool round;
    int pathSteps;
    public Vector2Int finish { get; private set; }
    enum dir
    {
        left, right, top, down
    }
    dir dirr;
    public enum dangType
    {
        cave,
        bunker
    }
    dangType phs;
    List<Vector2Int> subPaths;
    int subPathsCount;
    public static string Key(int x, int y)
    {
        return x.ToString() + " " + y.ToString();
    }
    public void Generate(dangType type, int seed, OnDungeonGenerateReady callback)
    {
        if (nowGenerate)
        {
            callback(false, new Vector2Int(0, 0));
            return;
        }
        map = new Dictionary<string, bool>();
        isDeadEnd = new Dictionary<string, Vector2Int>();
        deadEnds = new List<string>();
        Random.InitState(seed);
        X = 0;
        Y = 0;
        readyCallback = callback;
        total = Random.Range(sizeCave.x, sizeCave.y);
        ready = 0;
        phs = type;
        switch (Random.Range(0, 9999) % 4)
        {
            case 0: dirr = dir.down; break;
            case 1: dirr = dir.left; break;
            case 2: dirr = dir.right; break;
            default: dirr = dir.top; break;
        }
        switch (type)
        {
            case dangType.cave:
                distortion = 1;
                round = true;
                break;
            case dangType.bunker:
                distortion = 0;
                round = false;
                break;
        }
        subPathsCount = 0;
        subPaths = new List<Vector2Int>();

        BuildRoomByType(1);
        nowGenerate = true;
    }
    public void Update()
    {
        if (nowGenerate)
        {
            PathGenerate();
        }
    }
    void PathGenerate()
    {
        if (ready >= total)
        {
            string key = Key(X, Y);
            isDeadEnd[key] = new Vector2Int(X, Y);
            deadEnds.Add(key);
            onDeadEnd?.Invoke(key, X, Y);
            BuildRoomByType(Random.Range(0, 2));
            if (subPaths.Count < 1)
            {
                Complete();
                return;
            }
            else
            {
                Vector2Int sp = subPaths[0];
                subPaths.RemoveAt(0);
                total += Random.Range(tot.x, tot.y);
                X = sp.x;
                Y = sp.y;
            }
        }
        ready++;
        pathSteps = Random.Range(pathS.x, pathS.y);
        while (pathSteps > 0)
        {
            pathSteps--;
            DrowSq();
            bool distortionDirX;
            switch (dirr)
            {
                case dir.right:
                    X += SQS + 1;
                    distortionDirX = false;
                    break;
                case dir.left:
                    X -= SQS - 1;
                    distortionDirX = false;
                    break;
                case dir.top:
                    Y += SQS + 1;
                    distortionDirX = true;
                    break;
                default:
                    Y -= SQS + 1;
                    distortionDirX = true;
                    break;
            }
            if (distortion > 0 && Random.Range(0, 100) < 40)
            {
                DrowSq();
                if (distortionDirX)
                {
                    X += Random.Range(-distortion, distortion);
                }
                else
                {
                    Y += Random.Range(-distortion, distortion);
                }
            }
        }

        if (Random.Range(0, 100) < 70 && !map.ContainsKey(Key(X, Y)))
        {
            BuildRoomByType(Random.Range(0, 2));
        }
        NextDir();
    }
    void BuildRoomByType(int size = 0)
    {
        int roomDif;
        int minSize;
        int maxSize;
        switch (phs)
        {
            case dangType.cave:
                minSize = size + sizeRoom.x;
                maxSize = size + sizeRoom.y;
                roomDif = Random.Range(2, 4);
                break;
            default:
                minSize = size + sizeRoom.x;
                maxSize = size + sizeRoom.y;
                roomDif = 1;
                break;

        }
        for (int i = 0; i < roomDif; i++)
        {
            BuildRoom(minSize, maxSize);
        }
        if (Random.Range(0, 100) < (50 - subPathsCount * 4) && subPathsCount < MSP)
        {
            subPaths.Add(new Vector2Int(X, Y));
            subPathsCount++;
        }

    }
    void BuildRoom(int minSize, int maxSize)
    {
        int rX = Random.Range(SQS + minSize, SQS + maxSize);
        int rY = Random.Range(SQS + minSize, SQS + maxSize);
        DrowSq(rX, rY);
        X += Random.Range(-rX + 1, rX - 1);
        Y += Random.Range(-rY + 1, rY - 1);
    }
    void DrowSq(int sx = SQS, int sy = SQS)
    {
        string key;
        float radius = 0f;
        if (round && sx > SQS && sy > SQS)
        {
            radius = (float)Mathf.Max(sx, sy) * 1.1f;
        }
        for (int xx = X - sx; xx < X + sx; xx++)//=rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr
        {
            for (int yy = Y - sy; yy < Y + sy; yy++)//=
            {
                if (round && radius > 0f && Vector2.Distance(new Vector2(xx, yy), new Vector2(X, Y)) > radius)
                {
                    continue;
                }
                key = Key(xx, yy);
                if (map.ContainsKey(key))
                {
                    continue;
                }
                map[key] = true;
                onAddPoint?.Invoke(key, xx, yy);
            }
        }
    }
    void Complete()
    {
        float maxDir = 0f;
        string finKey = "";
        foreach (var pair in isDeadEnd)
        {
            float dist = Vector2.Distance(Vector2.zero, pair.Value);
            if (dist > maxDir)
            {
                maxDir = dist;
                finish = pair.Value;
                finKey = pair.Key;
            }
        }
        isDeadEnd.Remove(finKey);
        nowGenerate = false;
        readyCallback(true, finish);

    }
    void NextDir()
    {
        int r = Random.Range(0, 9999) % 3;
        switch (dirr)
        {
            case dir.left:
                switch (r)
                {
                    case 0: dirr = dir.down; return;
                    case 1: dirr = dir.left; return;
                    default: dirr = dir.top; return;
                }
            case dir.right:
                switch (r)
                {
                    case 0: dirr = dir.down; return;
                    case 1: dirr = dir.right; return;
                    default: dirr = dir.top; return;
                }
            case dir.down:
                switch (r)
                {
                    case 0: dirr = dir.right; return;
                    case 1: dirr = dir.left; return;
                    default: dirr = dir.down; return;
                }
            default:
                switch (r)
                {
                    case 0: dirr = dir.down; return;
                    case 1: dirr = dir.left; return;
                    default: dirr = dir.right; return;
                }
        }
    }
}
