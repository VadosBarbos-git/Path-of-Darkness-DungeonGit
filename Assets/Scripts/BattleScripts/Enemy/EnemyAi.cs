using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public List<Cell> Attack;
    public List<Cell> Block;

    List<ItemAptitudes> AllAptituds = new();
    List<Cell> AllBlocks = new();
    List<Cell> AllAttacks = new();
    public List<Cell> PlanAttack = new List<Cell>();
    public List<Cell> PlanBlock = new List<Cell>();
    Cell BaseAttack;
    Cell BaseBlock;
    bool isStartBatle = false;
    public ArenaControler arenaControler;
    public void OnEnable()
    {
        isStartBatle = true;
        ClearComponent();
        MakeAChoice();
        arenaControler.wizual.ShowEnemyPlanActions();
    }
    public void MakeAChoice()
    {

        if (isStartBatle)
        {

            StartBatle();
            MakeCells();
            MakePlanChoyses();
            isStartBatle = false;
        }
        else
        {
            ChoysSameAttack();
            ChoysSameBlock();

            PlanAttack[0] = null;
            ClearSameListFromEmptyCell(PlanAttack);
            PlanBlock[0] = null;
            ClearSameListFromEmptyCell(PlanBlock);


        }

    }
    void ClearComponent()
    {
        Attack = new List<Cell>();
        Block = new List<Cell>();
        AllAptituds = null;
        BaseBlock = new Cell(StartBattleScene.Enemy.BaseBlock, 1);
        BaseAttack = new Cell(StartBattleScene.Enemy.BaseAttack, 1);
        PlanAttack = new List<Cell>();
        PlanBlock = new List<Cell>();
    }
    void MakePlanChoyses()
    {


        for (int i = 0; i < 30; i++)
        {
            if (AllAttacks.Count > 0)
            {
                int index = Random.Range(0, AllAttacks.Count);
                PlanAttack.Add(AllAttacks[index]);
                AllAttacks[index] = new Cell();
                ClearSameListFromEmptyCell(AllAttacks);
            }
            else
            {
                PlanAttack.Add(new Cell(StartBattleScene.Enemy.BaseAttack, 1));
            }
        }
        for (int i = 0; i < 30; i++)
        {
            if (AllBlocks.Count > 0)
            {
                int index = Random.Range(0, AllBlocks.Count);
                PlanBlock.Add(AllBlocks[index]);
                AllBlocks[index] = new Cell();
                ClearSameListFromEmptyCell(AllBlocks);
            }
            else
            {
                PlanBlock.Add(new Cell(StartBattleScene.Enemy.BaseBlock, 1));
            }
        }
    }



    void StartBatle()
    {
        Attack = new List<Cell>();
        Block = new List<Cell>();
        for (int i = 0; i < 3; i++)
        {
            Attack.Add(new Cell());
            Block.Add(new Cell());
        }
        AllAptituds = StartBattleScene.Enemy.Aptituds;
        BaseBlock = new Cell(StartBattleScene.Enemy.BaseBlock, 1);
        BaseAttack = new Cell(StartBattleScene.Enemy.BaseAttack, 1);

    }
    void MakeCells()
    {
        List<ItemAptitudes> sameAptituds = new List<ItemAptitudes>();
        for (int i = 0; i < AllAptituds.Count; i++)
        {
            if (AllAptituds[i].typeAptitude == TypeAptitudes.Attack)
            {
                sameAptituds.Add(AllAptituds[i]);
            }
        }
        foreach (var item in sameAptituds)
        {
            AllAttacks.Add(new Cell(item, 1));
        }
        sameAptituds.Clear();
        for (int i = 0; i < AllAptituds.Count; i++)
        {
            if (AllAptituds[i].typeAptitude == TypeAptitudes.Block)
            {
                sameAptituds.Add(AllAptituds[i]);
            }
        }
        foreach (var item in sameAptituds)
        {
            AllBlocks.Add(new Cell(item, 1));
        }


    }
    void ChoysSameAttack()
    {
        if (PlanAttack.Count > 0)
        {
            int posAttack = 0;//Random.Range(0, Attack.Count);
            Attack[posAttack] = new Cell(PlanAttack[0]);
            Debug.Log("Соперник бьет в " + posAttack);
        }
        else
        {
            int posAttack = 0;//Random.Range(0, Attack.Count);
            Attack[posAttack] = new Cell(BaseAttack);
            Debug.Log("Соперник бьет в " + posAttack);
        }
    }
    void ChoysSameBlock()
    {
        if (PlanBlock.Count > 0)
        {
            int posBlock = 0;//Random.Range(0, Block.Count);
            Block[posBlock] = new Cell(PlanBlock[0]);
            Debug.Log("Соперник ставит блок  в " + posBlock);

        }
        else
        {
            int posBlock = 0;//Random.Range(0, Block.Count);
            Block[posBlock] = new Cell(BaseBlock);
            Debug.Log("Соперник ставит блок  в " + posBlock);
        }
    }
    void ClearSameListFromEmptyCell(List<Cell> listCells)
    {
        List<Cell> d = new List<Cell>();


        for (int i = 0; i < listCells.Count; i++)
        {
            if (listCells[i] == null || listCells[i].id == 0)
            {

            }
            else
            {
                d.Add(listCells[i]);
            }
        }

        listCells.Clear();
        listCells.AddRange(d);
    }

}

