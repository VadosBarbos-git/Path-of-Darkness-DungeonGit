using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StaticInventory
{
    public static List<Cell> BagCells;
    public static List<Cell> BeltBagCells;
    public static List<Cell> BodyCells;
    public static List<Cell> SaveChestCells; 
    public static List<Cell> MerchanCells;

    public delegate void CloseMainInventoryDelegate();
    public static event CloseMainInventoryDelegate closeInventory;
    public static int Coins { get; private set; }
    public static void StartSameScene()
    {
        OpenInventory();
        CloseInventory();
        Inventory.AddBonusesToPlayer();
    }
    public static void CloseInventory()
    {
        BagCells.Clear();
        BagCells.AddRange(Inventory.BagCells);

        BeltBagCells.Clear();
        BeltBagCells.AddRange(Inventory.BeltBagCells);

        BodyCells.Clear();
        BodyCells.AddRange(Inventory.BodyCells);

        SaveChestCells.Clear();
        SaveChestCells.AddRange(Inventory.SaveChestCells);

        MerchanCells.Clear();
        MerchanCells.AddRange(Inventory.MerchanCells);


        _chackLists();
        if (closeInventory != null)
        {
            closeInventory();
        }
    }
    public static void OpenInventory()
    {
        _chackLists();
        Inventory.BagCells.Clear();
        Inventory.BagCells.AddRange(BagCells);

        Inventory.BeltBagCells.Clear();
        Inventory.BeltBagCells.AddRange(BeltBagCells);

        Inventory.BodyCells.Clear();
        Inventory.BodyCells.AddRange(BodyCells);

        Inventory.SaveChestCells.Clear();
        Inventory.SaveChestCells.AddRange(SaveChestCells);

        Inventory.MerchanCells.Clear();
        Inventory.MerchanCells.AddRange(MerchanCells);

    }
    public static bool TryAddSameItem(Item itemData)
    {
        _chackLists();
        if (itemData._isAptitudes)
        {
            Cell firstCell = BagCells.FirstOrDefault(cell => cell.id == itemData.id);
            if (firstCell == null)
            {
                Cell secondCell = BagCells.FirstOrDefault(cell => cell.id == 0);
                if (secondCell == null)
                {
                    return false;
                }
                else
                {
                    secondCell.ItemData = itemData;
                    secondCell.id = itemData.id;
                    secondCell.countItem = 1;
                    return true;
                }
            }
            else
            {
                firstCell.countItem += 1;
                return true;
            }
        }
        else
        {
            Cell secondCell = BagCells.FirstOrDefault(cell => cell.id == 0);
            if (secondCell == null)
            {
                return false;
            }
            else
            {
                secondCell.ItemData = itemData;
                secondCell.id = itemData.id;
                secondCell.countItem = 1;
                return true;
            }
        }
    }
    public static void AddCoin(int valueCoins)
    {
        Coins += valueCoins;
    }
    public static bool TrySpendCoin(int valueCoins)
    {
        if (valueCoins > Coins)
        {
            return false;
        }
        else
        {
            Coins -= valueCoins;
            return true;
        }
    }
    private static void _chackLists()
    {
        if (BagCells == null || BagCells.Count < 1)
        {
            BagCells = new List<Cell>();
            for (int i = 0; i < 12; i++)
            {
                BagCells.Add(new Cell());
            }

        }
        if (BeltBagCells == null || BeltBagCells.Count < 1)
        {
            BeltBagCells = new List<Cell>();
            for (int i = 0; i < 6; i++)
            {
                BeltBagCells.Add(new Cell());
            }
        }
        if (BodyCells == null || BodyCells.Count < 1)
        {
            BodyCells = new List<Cell>();
            for (int i = 0; i < 6; i++)
            {
                BodyCells.Add(new Cell());
            }

        }
        if (SaveChestCells == null || SaveChestCells.Count < 1)
        {
            SaveChestCells = new List<Cell>();
            for (int i = 0; i < 16; i++)
            {
                SaveChestCells.Add(new Cell());
            }
        }
        if (MerchanCells == null || MerchanCells.Count < 1)
        {
            MerchanCells = new List<Cell>();
            for (int i = 0; i < 16; i++)
            {
                MerchanCells.Add(new Cell());
            }
        }
    }
}
