
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum TypeCellInArena
{
    boxAttack,
    boxBlock,
    Attack,
    Block,
    BaseBlock,
    BaseAttack
}

public class PanelInventoryArena : MonoBehaviour
{
    public ItemAptitudes BaseAttack;
    public ItemAptitudes BaseBlock;
    [HideInInspector] public List<Cell> AttackBelt = new List<Cell>();
    [HideInInspector] public List<Cell> BlockBelt = new List<Cell>();

    [HideInInspector] public List<Cell> BoxBlockMain = new List<Cell>();
    [HideInInspector] public List<Cell> BoxAttackMain = new List<Cell>();
    public WizualArenaControler wizual;


    public GameObject BaseBlockObj;
    public GameObject BaseAtackObj;
    public GameObject BlockObj;
    public GameObject AttackObj;
    public GameObject DragCellObj;
    [HideInInspector] public Cell CurentCell;
    [HideInInspector] public TypeCellInArena CurentTypeCell;

    public GameObject descriptionItem;
    public Image descImage;
    public TextMeshProUGUI descName;
    public TextMeshProUGUI descInfo;

    public void OnEnable()
    {

        AttackBelt.Clear();
        BlockBelt.Clear();
        BoxBlockMain.Clear();
        BoxAttackMain.Clear();
        for (int i = 0; i < 3; i++)
        {
            BoxBlockMain.Add(new Cell());
            BoxAttackMain.Add(new Cell());
        }
        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
            {
                if (StaticInventory.BeltBagCells != null && StaticInventory.BeltBagCells[i] != null)
                {
                    AttackBelt.Add(new Cell(StaticInventory.BeltBagCells[i]));
                }
                else
                {
                    AttackBelt.Add(new Cell());
                }
            }
            else
            {

                if (StaticInventory.BeltBagCells != null && StaticInventory.BeltBagCells[i] != null)
                {
                    BlockBelt.Add(new Cell(StaticInventory.BeltBagCells[i]));
                }
                else
                {
                    BlockBelt.Add(new Cell());
                }
            }
        }
        ShowInventory();
    }
    private void OnDisable()
    {
        if (StaticInventory.BeltBagCells != null)
        {
            StaticInventory.BeltBagCells.Clear();
            for (int i = 0; i < 6; i++)
            {
                StaticInventory.BeltBagCells.Add(new Cell());
            }
            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    if (AttackBelt != null && AttackBelt[i].id != 0)
                    {
                        StaticInventory.BeltBagCells[i] = new Cell(AttackBelt[i]);
                    }
                }
                else
                {
                    if (BlockBelt != null && BlockBelt[i - 3].id != 0)
                    {
                        StaticInventory.BeltBagCells[i] = new Cell(BlockBelt[i - 3]);
                    }
                }
            }
        }
    }
    public bool SetCurentCell(int nomber)
    {
        CurentCell = _findCellinArray(nomber);
        if (CurentCell.id == 0)
        {
            return false;
        }
        return true;
    }
    public bool SetCurentCell(int nomber, TypeCellInArena type)
    {
        if (type == TypeCellInArena.boxAttack)
        {
            CurentCell = BoxAttackMain[nomber];
        }
        else
        {
            CurentCell = BoxBlockMain[nomber];
        }
        if (CurentCell.id == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public Cell _findCellinArray(int nomber)
    {
        switch (nomber)
        {
            case 0:
                return new Cell(BaseAttack, 1);
            case 1:
                return AttackBelt[0];
            case 2:
                return AttackBelt[1];
            case 3:
                return AttackBelt[2];
            case 4:
                return new Cell(BaseBlock, 1);
            case 5:
                return BlockBelt[0];
            case 6:
                return BlockBelt[1];
            case 7:
                return BlockBelt[2];
            default:
                Debug.LogError("Не подходит номер ячейки ");
                return new Cell();

        }

    }
    public void ShowInventory()
    {
        wizual.ShowInventoryBattle();
        wizual.ShowMainBoxesPlayer();
    }
}
