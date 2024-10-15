
using System.Collections;
using System.Collections.Generic;  
using UnityEngine; 

public class ArenaControler : MonoBehaviour
{
    public enum TypeHistoryBoxes
    {
        Parry,
        BlockEmpty,
        Hit,
        Close,
        Empty
    }
    public GameObject FatherPlayer;
    public GameObject FatherEnemy;
    public List<GameObject> AllCharactersArena;
    public EnemyAi enemyAi;
    public PanelInventoryArena panelInventory;
    public StartBattleScene startBattleScene;
    public static bool StopForSameAction = false;

    

    public TypeHistoryBoxes[,] HistoryPlayer = new TypeHistoryBoxes[3, 3];
    public TypeHistoryBoxes[,] HistoryEnemy = new TypeHistoryBoxes[3, 3];
    //компоненты 
    public WizualArenaControler wizual;
    private void OnEnable()
    {
        wizual.ShowPersons(FatherPlayer.transform, FatherEnemy.transform, AllCharactersArena);
        wizual.ShowInventoryBattle();
        for (int i = 0; i < 3; i++)
        {
            for (int r = 0; r < 3; r++)
            {
                HistoryEnemy[i, r] = TypeHistoryBoxes.Close;
                HistoryPlayer[i, r] = TypeHistoryBoxes.Close;
            }
        }
        wizual.ShowHistoryPanel();
        wizual.CloseEnemyBoxesPanel();
        wizual.ShowHpPlayerAndAnamy(StartBattleScene.Player, StartBattleScene.Enemy);
        StartBattleScene.Player.ChangeArmorCurent += wizual.ChangePlayerCurentArmor;
    }
    private void OnDisable()
    {
        Destroy(wizual.PlayerObj);
        wizual.PlayerObj = null;
        Destroy(wizual.EnemyObj);
        wizual.EnemyObj = null;
    }

    public void CorutinBatle()
    {
        StartCoroutine(Batle());
    }
    void AwakeActionAptitud(List<Cell> cells, CharactersDescription User, CharactersDescription Target)
    {
        foreach (Cell cell in cells)
        {
            if (cell.id != 0)
            {
                var aptitud = cell.ItemData as ItemAptitudes;
                aptitud.AwakeActionNextStep(User, Target);
            }
        }
    }
    void StartMainActionAptitud(Cell cell, CharactersDescription User, CharactersDescription Target)
    {
        if (cell.id != 0)
        {
            var aptitud = cell.ItemData as ItemAptitudes;
            aptitud.StartMainActionHitInPoint(User, Target);
        }
    }
    IEnumerator stopForSameAction()
    {
        while (StopForSameAction)
        {
            yield return null;
        }
        yield return null;
    }

    IEnumerator Batle()
    {

        AwakeActionAptitud(panelInventory.BoxAttackMain, StartBattleScene.Player, StartBattleScene.Enemy);
        AwakeActionAptitud(panelInventory.BoxBlockMain, StartBattleScene.Player, StartBattleScene.Enemy);
        AwakeActionAptitud(enemyAi.Attack, StartBattleScene.Enemy, StartBattleScene.Player);
        AwakeActionAptitud(enemyAi.Block, StartBattleScene.Enemy, StartBattleScene.Player);
        StartBattleScene.Enemy.UpdateBuffs();
        StartBattleScene.Player.UpdateBuffs();
        yield return StartCoroutine(stopForSameAction());

        int AttackFromPlayerPosCell = panelInventory.BoxAttackMain.FindIndex(c => c.id != 0);
        int BlockFromEnemyPosCell = enemyAi.Block.FindIndex(c => c.id != 0);

        int AttackFromEnemyPosCell = enemyAi.Attack.FindIndex(c => c.id != 0);
        int BlockFromPlayerPosCell = panelInventory.BoxBlockMain.FindIndex(c => c.id != 0);

        int index = FindCloseHistoryBox();

        List<int> PlayerBlocksSameCells = new List<int>();
        List<int> EnemyBlocksSameCells = new List<int>();

        List<int> PlayerAttackSameCells = new List<int>();
        List<int> EnemyAttackSameCells = new List<int>();


        CriateBlockAndAttackLists();



        //не забываем чтоб отключить этот метод нужно изменить bool
        StartCoroutine(wizual.HighlightMainBoxPlayerAttackEnemyBlock(true));


        #region StepPlayer

        for (int i = 0; i < 3; i++)
        {
            if (PlayerAttackSameCells.Contains(i) && EnemyBlocksSameCells.Contains(i))
            {
                if (panelInventory.BoxAttackMain[i].ItemData is ItemAptitudes action && action.Action is IPiercingStrike)
                {
                    if (AttackFromEnemyPosCell == AttackFromPlayerPosCell)
                    {
                        HistoryEnemy[index, i] = CheckIAttackBlock(enemyAi.Attack[AttackFromEnemyPosCell]);
                    }
                    else
                    {
                        HistoryEnemy[index, i] = TypeHistoryBoxes.Hit;
                    }
                }
                else
                {
                    HistoryEnemy[index, i] = TypeHistoryBoxes.Parry;
                }
            }
            else if (EnemyBlocksSameCells.Contains(i))
            {
                HistoryEnemy[index, i] = TypeHistoryBoxes.BlockEmpty;
            }
            else if (PlayerAttackSameCells.Contains(i))
            {
                if (AttackFromEnemyPosCell == AttackFromPlayerPosCell)
                {
                    HistoryEnemy[index, i] = CheckIAttackBlock(enemyAi.Attack[AttackFromEnemyPosCell]);
                }
                else
                {
                    HistoryEnemy[index, i] = TypeHistoryBoxes.Hit;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            HistoryEnemy[index, i] = HistoryEnemy[index, i] == TypeHistoryBoxes.Close ? TypeHistoryBoxes.Empty : HistoryEnemy[index, i];
        }

        for (int i = 0; i < 3; i++)
        {
            if (HistoryEnemy[index, i] == TypeHistoryBoxes.Parry)
            {
                StartMainActionAptitud(enemyAi.Block[BlockFromEnemyPosCell], StartBattleScene.Enemy, StartBattleScene.Player);
            }
            else if (HistoryEnemy[index, i] == TypeHistoryBoxes.Hit)
            {
                StartMainActionAptitud(panelInventory.BoxAttackMain[AttackFromPlayerPosCell], StartBattleScene.Player, StartBattleScene.Enemy);
            }
        }

        wizual.ShowBoxPanelsEnemy();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(wizual.AnimatorAttack1Player());
        yield return StartCoroutine(CheckHitAnimation());
        wizual.ShowHpPlayerAndAnamy(StartBattleScene.Player, StartBattleScene.Enemy);


        if (!StartBattleScene.Player.ImAlive())
        {
            yield return new WaitForSeconds(0.5f);
            CLoseArenaOrNextStep();
            wizual.PlayerDead();
            yield break;
        }
        if (!StartBattleScene.Enemy.ImAlive())
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(wizual.HighlightMainBoxPlayerAttackEnemyBlock(false));
            CLoseArenaOrNextStep();
            wizual.PlayerWine();
            yield break;
        }
        #endregion
        yield return StartCoroutine(wizual.HighlightMainBoxPlayerAttackEnemyBlock(false));
        StartCoroutine(wizual.HighlightMainBoxPlayerBlockEnemyAttack(true));
        yield return new WaitForSeconds(1f);

        #region StepEnemy
        for (int i = 0; i < 3; i++)
        {
            if (EnemyAttackSameCells.Contains(i) && PlayerBlocksSameCells.Contains(i))
            {
                if (enemyAi.Attack[i].ItemData is ItemAptitudes apti && apti.Action is IPiercingStrike)
                {
                    if (AttackFromEnemyPosCell == AttackFromPlayerPosCell)
                    {
                        HistoryPlayer[index, i] = CheckIAttackBlock(panelInventory.BoxAttackMain[AttackFromPlayerPosCell]);
                    }
                    else
                    {
                        HistoryPlayer[index, i] = TypeHistoryBoxes.Hit;

                    }
                }
                else
                {
                    HistoryPlayer[index, i] = TypeHistoryBoxes.Parry;
                }
            }
            else if (PlayerBlocksSameCells.Contains(i))
            {
                HistoryPlayer[index, i] = TypeHistoryBoxes.BlockEmpty;
            }
            else if (EnemyAttackSameCells.Contains(i))
            {
                if (AttackFromEnemyPosCell == AttackFromPlayerPosCell)
                {
                    HistoryPlayer[index, i] = CheckIAttackBlock(panelInventory.BoxAttackMain[AttackFromPlayerPosCell]);
                }
                else
                {
                    HistoryPlayer[index, i] = TypeHistoryBoxes.Hit;

                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            HistoryPlayer[index, i] = HistoryPlayer[index, i] == TypeHistoryBoxes.Close ? TypeHistoryBoxes.Empty : HistoryPlayer[index, i];
        }

        for (int i = 0; i < 3; i++)
        {
            if (HistoryPlayer[index, i] == TypeHistoryBoxes.Parry)
            {
                StartMainActionAptitud(panelInventory.BoxBlockMain[BlockFromPlayerPosCell], StartBattleScene.Player, StartBattleScene.Enemy);
            }
            else if (HistoryPlayer[index, i] == TypeHistoryBoxes.Hit)
            {
                StartMainActionAptitud(enemyAi.Attack[AttackFromEnemyPosCell], StartBattleScene.Enemy, StartBattleScene.Player);
            }
        }


        yield return StartCoroutine(wizual.AnimatorAttack1Enemy());
        yield return StartCoroutine(CheckHitAnimation());
        wizual.ShowHpPlayerAndAnamy(StartBattleScene.Player, StartBattleScene.Enemy);
        wizual.ShowHistoryPanel();


        if (!StartBattleScene.Player.ImAlive())
        {
            yield return new WaitForSeconds(0.5f);
            CLoseArenaOrNextStep();
            wizual.PlayerDead();
            yield break;
        }
        if (!StartBattleScene.Enemy.ImAlive())
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(wizual.HighlightMainBoxPlayerAttackEnemyBlock(false));
            CLoseArenaOrNextStep();
            wizual.PlayerWine();
            yield break;
        }

        #endregion
        yield return new WaitForSeconds(0.5f);
        CLoseArenaOrNextStep();
        BatelSteps.go = false;
        yield return StartCoroutine(wizual.HighlightMainBoxPlayerBlockEnemyAttack(false));
        yield return new WaitForSeconds(0.5f);

        void CriateBlockAndAttackLists()
        {
            if (panelInventory.BoxBlockMain[BlockFromPlayerPosCell].ItemData is ItemAptitudes PAptiBlock && PAptiBlock.Action is IBlockSameCells PBSCells)
            {
                PlayerBlocksSameCells.AddRange(PBSCells.GetNombersCell(BlockFromPlayerPosCell));
            }
            else if (panelInventory.BoxBlockMain[BlockFromPlayerPosCell].ItemData is ItemAptitudes P2AptiBlock && P2AptiBlock.Action is IDontBlock)
            {
                PlayerBlocksSameCells.Clear();
            }
            else
            {
                PlayerBlocksSameCells.Add(BlockFromPlayerPosCell);
            }

            if (enemyAi.Block[BlockFromEnemyPosCell].ItemData is ItemAptitudes EAptiBlock && EAptiBlock.Action is IBlockSameCells EBSCells)
            {
                EnemyBlocksSameCells.AddRange(EBSCells.GetNombersCell(BlockFromEnemyPosCell));
            }
            else if (enemyAi.Block[BlockFromEnemyPosCell].ItemData is ItemAptitudes E2AptiBlock && E2AptiBlock.Action is IDontBlock)
            {
                EnemyBlocksSameCells.Clear();
            }
            else
            {
                EnemyBlocksSameCells.Add(BlockFromEnemyPosCell);
            }


            // работаем с Attack
            if (panelInventory.BoxAttackMain[AttackFromPlayerPosCell].ItemData is ItemAptitudes PAptiAttack && PAptiAttack.Action is IAttackSameCell PASCell)
            {
                PlayerAttackSameCells.AddRange(PASCell.GetNombersCell(AttackFromPlayerPosCell));
            }
            else
            {
                PlayerAttackSameCells.Add(AttackFromPlayerPosCell);
            }

            if (enemyAi.Attack[AttackFromEnemyPosCell].ItemData is ItemAptitudes EAptiAttack && EAptiAttack.Action is IAttackSameCell EASCell)
            {
                EnemyAttackSameCells.AddRange(EASCell.GetNombersCell(AttackFromEnemyPosCell));
            }
            else
            {
                EnemyAttackSameCells.Add(AttackFromEnemyPosCell);
            }
             
            

            //Проверка на Custom Block это надо делать в конце 
            if (EnemyBlocksSameCells.Count == 1 && enemyAi.Block[EnemyBlocksSameCells[0]].ItemData is ItemAptitudes apt &&
                apt.Action is ICustomBlock castonEnemyBlock)
            {
                EnemyBlocksSameCells.Clear();
                EnemyBlocksSameCells.Add(castonEnemyBlock.GetPosBlock(PlayerAttackSameCells));
            }
            if (PlayerBlocksSameCells.Count == 1 && panelInventory.BoxBlockMain[PlayerBlocksSameCells[0]].ItemData is ItemAptitudes PApt &&
                PApt.Action is ICustomBlock PcastonEnemyBlock)
            {
                PlayerBlocksSameCells.Clear();
                PlayerBlocksSameCells.Add(PcastonEnemyBlock.GetPosBlock(EnemyAttackSameCells));
            }



        }
        TypeHistoryBoxes CheckIAttackBlock(Cell attackUser)
        {
            if (attackUser.ItemData is ItemAptitudes ap && ap.Action is IAttackBlock)
            {
                return TypeHistoryBoxes.Parry;
            }
            else
            {
                return TypeHistoryBoxes.Hit;
            }

        }
    }
    IEnumerator CheckHitAnimation()
    {
        if (StartBattleScene.Enemy.takeDamage)
        {
            StartBattleScene.Enemy.takeDamage = false;
            yield return StartCoroutine(wizual.AnimatorHitEnemy(StartBattleScene.Enemy.CurentHealth));
        }
        if (StartBattleScene.Player.takeDamage)
        {
            StartBattleScene.Player.takeDamage = false;
            yield return StartCoroutine(wizual.AnimatorHitPlayer(StartBattleScene.Player.CurentHealth));
        }
        yield return null;
    }
    void CLoseArenaOrNextStep()
    {
        ClearMainBoxes();
        wizual.CloseEnemyBoxesPanel();
        wizual.ShowHistoryPanel();
        wizual.ShowMainBoxesPlayer();
    }
    void ClearMainBoxes()
    {
        for (int i = 0; i < panelInventory.BlockBelt.Count; i++)
        {
            panelInventory.BoxAttackMain[i] = new Cell();
        }
        for (int i = 0; i < panelInventory.BoxBlockMain.Count; i++)
        {
            panelInventory.BoxBlockMain[i] = new Cell();
        }
        for (int i = 0; i < enemyAi.Attack.Count; i++)
        {
            enemyAi.Attack[i] = new Cell();
        }
        for (int i = 0; i < enemyAi.Block.Count; i++)
        {
            enemyAi.Block[i] = new Cell();
        }

    }
    int FindCloseHistoryBox()
    {
        if (HistoryPlayer[0, 0] == TypeHistoryBoxes.Close)
        {
            return 0;
        }
        else if (HistoryPlayer[1, 0] == TypeHistoryBoxes.Close)
        {
            return 1;
        }
        else if (HistoryPlayer[2, 0] == TypeHistoryBoxes.Close)
        {
            return 2;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                for (int r = 0; r < 3; r++)
                {
                    HistoryEnemy[i, r] = TypeHistoryBoxes.Close;
                    HistoryPlayer[i, r] = TypeHistoryBoxes.Close;
                }
            }
            return 0;
        }

    }

    IEnumerator WaitForTouchCoroutine()
    {
        // Ждем до тех пор, пока на экране не будет хотя бы одно касание
        while (true)
        {
            // Проверяем количество касаний
            if (Input.touchCount > 0)
            {
                // Берем первое касание
                Touch touch = Input.GetTouch(0);

                // Если это касание только началось (фаза Began)
                if (touch.phase == TouchPhase.Began)
                {
                    // Выходим из цикла и завершаем корутину
                    break;
                }
            }

            // Ждем один кадр перед повторной проверкой
            yield return null;
        }

        // Логика, которая выполняется после касания
        Debug.Log("Экран был нажат!");
    }
}