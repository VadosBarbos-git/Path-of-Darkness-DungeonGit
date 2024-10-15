using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public static class CreatingWall
{
    public static List<Wall> Create(List<Vector2Int> map)
    {
        List<Wall> Walls = new List<Wall>();
        foreach (var kvp in map)
        {
            Vector2Int CurVector = new Vector2Int(kvp.x, kvp.y + 1);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }
            CurVector = new Vector2Int(kvp.x, kvp.y - 1);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }
            CurVector = new Vector2Int(kvp.x + 1, kvp.y);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }
            CurVector = new Vector2Int(kvp.x - 1, kvp.y);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }


            CurVector = new Vector2Int(kvp.x - 1, kvp.y + 1);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }
            CurVector = new Vector2Int(kvp.x + 1, kvp.y + 1);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }
            CurVector = new Vector2Int(kvp.x + 1, kvp.y - 1);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }
            CurVector = new Vector2Int(kvp.x - 1, kvp.y - 1);
            if (!map.Contains(CurVector) && !Walls.Any(wall => wall.vector2Int == CurVector))
            {
                Walls.Add(new Wall(CurVector, TypeWall.wall));
            }

        }
        CreateNomberFour(Walls, map);
        CreateWater(Walls, map);
        CreateAllWalls(Walls, map);


        return Walls;
    }
    private static void CreateWater(List<Wall> Walls, List<Vector2Int> map)
    {
        for (int i = 0; i < Walls.Count; i++)
        {
            if (Walls[i].type == TypeWall.nomberFour)
            {
                continue;
            }
            Vector2Int CurentVector = new Vector2Int(Walls[i].vector2Int.x, Walls[i].vector2Int.y - 1);
            if (!Walls.Any(Wall => Wall.vector2Int == CurentVector) && map.Contains(CurentVector))
            {
                Walls[i].SetTWall(TypeWall.water);
            }
        }
    }
    private static void CreateNomberFour(List<Wall> Walls, List<Vector2Int> map)
    {
        for (int i = 0; i < Walls.Count; i++)
        {
            Vector2Int CurVector = new Vector2Int(Walls[i].vector2Int.x, Walls[i].vector2Int.y);
            if (map.Contains(new Vector2Int(CurVector.x, CurVector.y - 1)) && !map.Contains(new Vector2Int(CurVector.x, CurVector.y + 1)))
            {
                if (!Walls.Any(wall => wall.vector2Int == new Vector2Int(CurVector.x, CurVector.y + 1)))
                {
                    Walls.Add(new Wall(new Vector2Int(CurVector.x, CurVector.y + 1), TypeWall.wall));
                }

                if (!map.Contains(new Vector2Int(CurVector.x - 1, CurVector.y)) &&
                    !Walls.Any(wall => wall.vector2Int == new Vector2Int(CurVector.x - 1, CurVector.y)))
                {
                    Walls.Add(new Wall(new Vector2Int(CurVector.x - 1, CurVector.y), TypeWall.wall));
                }

                if (!map.Contains(new Vector2Int(CurVector.x + 1, CurVector.y)) &&
                    !Walls.Any(wall => wall.vector2Int == new Vector2Int(CurVector.x + 1, CurVector.y)))
                {
                    Walls.Add(new Wall(new Vector2Int(CurVector.x + 1, CurVector.y), TypeWall.wall));
                }



                if (!map.Contains(new Vector2Int(CurVector.x + 1, CurVector.y + 1)) &&
                    !Walls.Any(wall => wall.vector2Int == new Vector2Int(CurVector.x + 1, CurVector.y + 1)))
                {
                    Walls.Add(new Wall(new Vector2Int(CurVector.x + 1, CurVector.y + 1), TypeWall.wall));
                }
                if (!map.Contains(new Vector2Int(CurVector.x - 1, CurVector.y + 1)) &&
                    !Walls.Any(wall => wall.vector2Int == new Vector2Int(CurVector.x - 1, CurVector.y + 1)))
                {
                    Walls.Add(new Wall(new Vector2Int(CurVector.x - 1, CurVector.y + 1), TypeWall.wall));
                }


                Walls[i].SetValue(4);
                Walls[i].SetTWall(TypeWall.nomberFour);
            }

        }
    }
    private static bool CanIAddValue(Vector2Int CurentVector, List<Vector2Int> map, List<Wall> Walls)
    {
        return map.Contains(CurentVector) || Walls.Any(wall => wall.vector2Int == CurentVector && wall.type == TypeWall.nomberFour);
    }
    private static bool ThisWater(List<Wall> Walls, Vector2Int CurentVector)
    {
        return Walls.Any(wall => wall.vector2Int == CurentVector && wall.type == TypeWall.water);
    }
    private static void CreateAllWalls(List<Wall> Walls, List<Vector2Int> map)
    {
        for (int i = 0; i < Walls.Count; i++)
        {
            if (Walls[i].type == TypeWall.nomberFour)
            {
                continue;
            }
            //с водой мы будем работать иначе:Будем смотреть нет ли соседних с водой 
            if (Walls[i].type == TypeWall.water)
            {
                int val = 0;

                Vector2Int Cur = new Vector2Int(Walls[i].vector2Int.x - 1, Walls[i].vector2Int.y);
                if (Walls.Any(wall => wall.vector2Int == Cur && wall.type == TypeWall.water))
                {
                    val += 8;
                }
                Cur = new Vector2Int(Walls[i].vector2Int.x + 1, Walls[i].vector2Int.y);
                if (Walls.Any(wall => wall.vector2Int == Cur && wall.type == TypeWall.water))
                {
                    val += 2;
                }

                Walls[i].SetValue(val);
                continue;
            }

            int value = 0;
            Vector2Int CurentVector = new Vector2Int(Walls[i].vector2Int.x, Walls[i].vector2Int.y + 1);
            if (map.Contains(CurentVector) || ThisWater(Walls, CurentVector))
            {
                value += 1;
            }
            CurentVector = new Vector2Int(Walls[i].vector2Int.x + 1, Walls[i].vector2Int.y);
            if (CanIAddValue(CurentVector, map, Walls) || ThisWater(Walls, CurentVector))
            {
                value += 2;
            }
            CurentVector = new Vector2Int(Walls[i].vector2Int.x, Walls[i].vector2Int.y - 1);
            if (CanIAddValue(CurentVector, map, Walls) || ThisWater(Walls, CurentVector))
            {
                value += 4;
            }
            CurentVector = new Vector2Int(Walls[i].vector2Int.x - 1, Walls[i].vector2Int.y);
            if (CanIAddValue(CurentVector, map, Walls) || ThisWater(Walls, CurentVector))
            {
                value += 8;
            }
            CurentVector = new Vector2Int(Walls[i].vector2Int.x - 1, Walls[i].vector2Int.y + 1);
            if (map.Contains(CurentVector) || ThisWater(Walls, CurentVector))
            {
                value += 16;
            }
            CurentVector = new Vector2Int(Walls[i].vector2Int.x + 1, Walls[i].vector2Int.y + 1);
            if (map.Contains(CurentVector) || ThisWater(Walls, CurentVector))
            {
                value += 32;
            }

            CurentVector = new Vector2Int(Walls[i].vector2Int.x + 1, Walls[i].vector2Int.y - 1);
            if (CanIAddValue(CurentVector, map, Walls) || ThisWater(Walls, CurentVector))
            {
                value += 64;
            }
            CurentVector = new Vector2Int(Walls[i].vector2Int.x - 1, Walls[i].vector2Int.y - 1);
            if (CanIAddValue(CurentVector, map, Walls) || ThisWater(Walls, CurentVector))
            {
                value += 128;
            }

            if (value == 0)
            {
                value = -1;
            }
            if (value == 4)
            {
                value = 0;
            }
            Walls[i].SetValue(value);
        }
    }
}
