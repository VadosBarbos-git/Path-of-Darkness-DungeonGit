using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using UnityEngine.UI;
using static ArenaControler;

public class WizualArenaControler : MonoBehaviour
{
    public EnemyAi enemyAi;
    public PanelInventoryArena panelInventory;
    public ArenaControler arenaControler;
    //public GameObject MainBoxesPlayer;
    //public GameObject MainBoxesEnemy;
    public Sprite BacgroundCellBoxAttack;
    public Sprite BacgroundCellBoxBlock;
    public Sprite CellLock;
    public Sprite CellEmpty;
    public Sprite CellBlockEmpty;
    public Sprite CellParry;
    public Sprite CellHit;
    public GameObject HistoryBox1;
    public GameObject HistoryBox2;
    public GameObject HistoryBox3;
    public Slider PlayerHealth;
    public Slider EnemyHealth;
    [HideInInspector] public Animator animatorPlayer;
    [HideInInspector] public Animator animatorEnemy;
    public GameObject DeathPanel;
    public GameObject WinPanel;
    [HideInInspector] public GameObject PlayerObj;
    [HideInInspector] public GameObject EnemyObj;
    public List<GridLayoutGroup> PlayerAttack;
    public List<GridLayoutGroup> PlayerBlock;
    public List<GridLayoutGroup> EnemyAttack;
    public List<GridLayoutGroup> EnemyBlock;
    private bool isFirstOnTop = true;
    public GameObject MainBoxPlayerAttack;
    public GameObject MainBoxPlayerBlock;
    public GameObject MainBoxEnemyBlock;
    public GameObject MainBoxEnemyAttack;
    public List<Image> PlanAttacksEnemy;
    public List<Image> PlanBlocksEnemy;

   
    #region Animation 
    public void ChangePlayerCurentArmor(bool biger)
    {

    }

    public IEnumerator AnimatorHitPlayer(int hp = 100)
    {

        animatorPlayer.Play("Hit");
        //yield return new WaitForEndOfFrame();
        AnimatorStateInfo animationState = animatorPlayer.GetCurrentAnimatorStateInfo(0);
        yield return new WaitUntil(() => animatorPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        yield return StartCoroutine(_animatorChackDeathPlayer(hp));


    }
    public IEnumerator AnimatorHitEnemy(int hp = 100)
    {

        animatorEnemy.Play("Hit");
        //yield return new WaitForEndOfFrame();
        AnimatorStateInfo animationState = animatorEnemy.GetCurrentAnimatorStateInfo(0);
        yield return new WaitUntil(() => animatorEnemy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);
        yield return StartCoroutine(_animatorChackDeathEnemy(hp));

    }
    public IEnumerator AnimatorAttack1Player()
    {
        animatorPlayer.Play("Attack1");
        yield return new WaitForEndOfFrame();
        AnimatorStateInfo animationState = animatorPlayer.GetCurrentAnimatorStateInfo(0);
        yield return new WaitUntil(() => animatorPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

    }
    public IEnumerator AnimatorAttack1Enemy()
    {
        animatorEnemy.Play("Attack1");
        yield return new WaitForEndOfFrame();
        AnimatorStateInfo animationState = animatorEnemy.GetCurrentAnimatorStateInfo(0);
        yield return new WaitUntil(() => animatorEnemy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

    }
    public void AnimatorAttack2Player()
    {
        animatorPlayer.SetTrigger("attack2");
    }
    public void AnimatorAttack2Enemy()
    {
        animatorEnemy.SetTrigger("attack2");
    }
    public IEnumerator HighlightMainBoxPlayerAttackEnemyBlock(bool biger)
    {
        Vector2 startVector = new Vector2(120, 120);
        Vector2 finishVector = new Vector2(130, 130);
        Vector2 SecondStart = new Vector2(120, 120);
        Vector2 SecondFinish = new Vector2(110, 110);

        Vector2 startPosAttackPlayer = new Vector2(-250, 100);
        Vector2 finishPosAttackPlayer = new Vector2(-350, 100);

        Vector2 startPosAttackEnemy = new Vector2(250, 100);
        Vector2 finishPosAttackEnemy = new Vector2(350, 100);

        Vector2 startBlockEnemy = new Vector2(400, 100);
        Vector2 finishBlockEnemy = new Vector2(350, 100);

        Vector2 startBlockPLayer = new Vector2(-400, 100);
        Vector2 finishBlockPlayer = new Vector2(-350, 100);

        Transform PatherAttackPlayer = PlayerAttack[0].transform.parent.transform;
        Transform PatherAttackEnemy = EnemyAttack[0].transform.parent.transform;
        Transform PatherBlockEnemy = EnemyBlock[0].transform.parent.transform;
        Transform PatherBlockPlayer = PlayerBlock[0].transform.parent.transform;
        float speed = 60;
        float speedTransform = 350;
        SwapObjectAttackPlayer();
        if (biger)
        {

            while (startVector.x < finishVector.x)
            {
                startVector = Vector2.MoveTowards(startVector, finishVector, speed * Time.deltaTime);
                PlayerAttack[0].cellSize = startVector;
                PlayerAttack[1].cellSize = startVector;
                PlayerAttack[2].cellSize = startVector;
                EnemyBlock[0].cellSize = startVector;
                EnemyBlock[1].cellSize = startVector;
                EnemyBlock[2].cellSize = startVector;

                if (startPosAttackPlayer.x > finishPosAttackPlayer.x)
                {
                    startPosAttackPlayer = Vector2.MoveTowards(startPosAttackPlayer, finishPosAttackPlayer, speedTransform * Time.deltaTime);
                    PatherAttackPlayer.localPosition = startPosAttackPlayer;
                }
                else
                {
                    PatherAttackPlayer.localPosition = finishPosAttackPlayer;
                }
                if (startBlockEnemy.x > finishBlockEnemy.x)
                {
                    startBlockEnemy = Vector2.MoveTowards(startBlockEnemy, finishBlockEnemy, speedTransform * Time.deltaTime);
                    PatherBlockEnemy.localPosition = startBlockEnemy;
                }
                else
                {
                    PatherBlockEnemy.localPosition = finishBlockEnemy;
                }


                if (SecondFinish.x < SecondStart.x)
                {
                    SecondStart = Vector2.MoveTowards(SecondStart, SecondFinish, speed * Time.deltaTime);
                    PlayerBlock[0].cellSize = SecondStart;
                    PlayerBlock[1].cellSize = SecondStart;
                    PlayerBlock[2].cellSize = SecondStart;
                    EnemyAttack[0].cellSize = SecondStart;
                    EnemyAttack[1].cellSize = SecondStart;
                    EnemyAttack[2].cellSize = SecondStart;
                }


                yield return null;
            }

        }
        else
        {

            while (startVector.x < finishVector.x)
            {
                finishVector = Vector2.MoveTowards(finishVector, startVector, speed * Time.deltaTime);
                PlayerAttack[0].cellSize = finishVector;
                PlayerAttack[1].cellSize = finishVector;
                PlayerAttack[2].cellSize = finishVector;
                EnemyBlock[0].cellSize = finishVector;
                EnemyBlock[1].cellSize = finishVector;
                EnemyBlock[2].cellSize = finishVector;

                if (startPosAttackPlayer.x > finishPosAttackPlayer.x)
                {
                    finishPosAttackPlayer = Vector2.MoveTowards(finishPosAttackPlayer, startPosAttackPlayer, speedTransform * Time.deltaTime);
                    PatherAttackPlayer.localPosition = finishPosAttackPlayer;
                }
                else
                {
                    PatherAttackPlayer.localPosition = startPosAttackPlayer;
                }

                if (startBlockEnemy.x > finishBlockEnemy.x)
                {
                    finishBlockEnemy = Vector2.MoveTowards(finishBlockEnemy, startBlockEnemy, speedTransform * Time.deltaTime);
                    PatherBlockEnemy.localPosition = finishBlockEnemy;
                }
                else
                {
                    PatherBlockEnemy.localPosition = startBlockEnemy;
                }

                if (SecondFinish.x > SecondStart.x)
                {
                    SecondFinish = Vector2.MoveTowards(SecondFinish, SecondStart, speed * Time.deltaTime);
                    PlayerBlock[0].cellSize = SecondFinish;
                    PlayerBlock[1].cellSize = SecondFinish;
                    PlayerBlock[2].cellSize = SecondFinish;
                    EnemyAttack[0].cellSize = SecondFinish;
                    EnemyAttack[1].cellSize = SecondFinish;
                    EnemyAttack[2].cellSize = SecondFinish;
                }



                yield return null;
            }

        }

        yield return null;

    }
    public IEnumerator HighlightMainBoxPlayerBlockEnemyAttack(bool biger)
    {
        Vector2 startPosAttackPlayer = new Vector2(-250, 100);
        Vector2 finishPosAttackPlayer = new Vector2(-350, 100);

        Vector2 startPosAttackEnemy = new Vector2(250, 100);
        Vector2 finishPosAttackEnemy = new Vector2(350, 100);

        Vector2 startBlockEnemy = new Vector2(400, 100);
        Vector2 finishBlockEnemy = new Vector2(350, 100);

        Vector2 startBlockPLayer = new Vector2(-400, 100);
        Vector2 finishBlockPlayer = new Vector2(-350, 100);

        Transform PatherAttackPlayer = PlayerAttack[0].transform.parent.transform;
        Transform PatherAttackEnemy = EnemyAttack[0].transform.parent.transform;
        Transform PatherBlockEnemy = EnemyBlock[0].transform.parent.transform;
        Transform PatherBlockPlayer = PlayerBlock[0].transform.parent.transform;

        float speedTransform = 350;

        Vector2 startVector = new Vector2(120, 120);
        Vector2 finishVector = new Vector2(130, 130);
        Vector2 SecondStart = new Vector2(120, 120);
        Vector2 SecondFinish = new Vector2(110, 110);
        float speed = 60;
        SwapObjectBlockPlayer();
        if (biger)
        {

            while (startVector.x < finishVector.x)
            {
                startVector = Vector2.MoveTowards(startVector, finishVector, speed * Time.deltaTime);
                PlayerBlock[0].cellSize = startVector;
                PlayerBlock[1].cellSize = startVector;
                PlayerBlock[2].cellSize = startVector;
                EnemyAttack[0].cellSize = startVector;
                EnemyAttack[1].cellSize = startVector;
                EnemyAttack[2].cellSize = startVector;

                if (SecondStart.x > SecondFinish.x)
                {
                    SecondStart = Vector2.MoveTowards(SecondStart, SecondFinish, speed * Time.deltaTime);
                    PlayerAttack[0].cellSize = SecondStart;
                    PlayerAttack[1].cellSize = SecondStart;
                    PlayerAttack[2].cellSize = SecondStart;
                    EnemyBlock[0].cellSize = SecondStart;
                    EnemyBlock[1].cellSize = SecondStart;
                    EnemyBlock[2].cellSize = SecondStart;
                }

                if (startBlockPLayer.x < finishBlockPlayer.x)
                {
                    startBlockPLayer = Vector2.MoveTowards(startBlockPLayer, finishBlockPlayer, speedTransform * Time.deltaTime);
                    PatherBlockPlayer.localPosition = startBlockPLayer;
                }
                else
                {
                    PatherBlockPlayer.localPosition = finishBlockPlayer;
                }

                if (startPosAttackEnemy.x < finishPosAttackEnemy.x)
                {
                    startPosAttackEnemy = Vector2.MoveTowards(startPosAttackEnemy, finishPosAttackEnemy, speedTransform * Time.deltaTime);
                    PatherAttackEnemy.localPosition = startPosAttackEnemy;
                }
                else
                {
                    PatherAttackEnemy.localPosition = finishPosAttackEnemy;
                }

                yield return null;
            }
        }
        else
        {

            while (startVector.x < finishVector.x)
            {
                finishVector = Vector2.MoveTowards(finishVector, startVector, speed * Time.deltaTime);
                PlayerBlock[0].cellSize = finishVector;
                PlayerBlock[1].cellSize = finishVector;
                PlayerBlock[2].cellSize = finishVector;
                EnemyAttack[0].cellSize = finishVector;
                EnemyAttack[1].cellSize = finishVector;
                EnemyAttack[2].cellSize = finishVector;

                if (SecondStart.x > SecondFinish.x)
                {
                    SecondFinish = Vector2.MoveTowards(SecondFinish, SecondStart, speed * Time.deltaTime);
                    PlayerAttack[0].cellSize = SecondFinish;
                    PlayerAttack[1].cellSize = SecondFinish;
                    PlayerAttack[2].cellSize = SecondFinish;
                    EnemyBlock[0].cellSize = SecondFinish;
                    EnemyBlock[1].cellSize = SecondFinish;
                    EnemyBlock[2].cellSize = SecondFinish;
                }

                if (startBlockPLayer.x < finishBlockPlayer.x)
                {
                    finishBlockPlayer = Vector2.MoveTowards(finishBlockPlayer, startBlockPLayer, speedTransform * Time.deltaTime);
                    PatherBlockPlayer.localPosition = finishBlockPlayer;
                }
                else
                {
                    PatherBlockPlayer.localPosition = startBlockPLayer;
                }
                if (startPosAttackEnemy.x < finishPosAttackEnemy.x)
                {
                    finishPosAttackEnemy = Vector2.MoveTowards(finishPosAttackEnemy, startPosAttackEnemy, speedTransform * Time.deltaTime);
                    PatherAttackEnemy.localPosition = finishPosAttackEnemy;
                }
                else
                {
                    PatherAttackEnemy.localPosition = startPosAttackEnemy;
                }

                yield return null;
            }

        }

        yield return null;
    }
    
    public void  SwapObjectAttackPlayer()
    {
        MainBoxPlayerBlock.transform.SetSiblingIndex(0); // Ставим image1 под image2
        MainBoxPlayerAttack.transform.SetSiblingIndex(1); // Ставим image2 над image1
        MainBoxEnemyAttack.transform.SetSiblingIndex(0);
        MainBoxEnemyBlock.transform.SetSiblingIndex(1);
    } 
    private void SwapObjectBlockPlayer()
    {
        MainBoxPlayerBlock.transform.SetSiblingIndex(1); // Ставим image1 под image2
        MainBoxPlayerAttack.transform.SetSiblingIndex(0); // Ставим image2 над image1
        MainBoxEnemyAttack.transform.SetSiblingIndex(1);
        MainBoxEnemyBlock.transform.SetSiblingIndex(0);


    }

    IEnumerator _animatorChackDeathPlayer(int hp)
    {

        animatorPlayer.SetInteger("hp", hp);

        yield return new WaitForEndOfFrame();
        AnimatorStateInfo animationState = animatorEnemy.GetCurrentAnimatorStateInfo(0);
        yield return new WaitUntil(() => animatorEnemy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

    }
    IEnumerator _animatorChackDeathEnemy(int hp)
    {

        animatorEnemy.SetInteger("hp", hp);
        yield return new WaitForEndOfFrame();
        AnimatorStateInfo animationState = animatorEnemy.GetCurrentAnimatorStateInfo(0);
        yield return new WaitUntil(() => animatorEnemy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    }
    void _setAnimators()
    {
        animatorPlayer = PlayerObj.transform.GetChild(0).GetComponent<Animator>();
        animatorEnemy = EnemyObj.transform.GetChild(0).GetComponent<Animator>();
    }
    #endregion

  
    public void PlayerDead()
    {
        DeathPanel.SetActive(true);
    }
    public void PlayerWine()
    {
        WinPanel.SetActive(true);
    }
    public void ShowPersons(Transform FatherPlayer, Transform FatherEnemy, List<GameObject> AllCharactersArena)
    {


        GameObject player = AllCharactersArena.FirstOrDefault(obj => obj.GetComponent<CharactersDescription>().Id == StartBattleScene.Player.Id);
        GameObject enemy = AllCharactersArena.FirstOrDefault(obj => obj.GetComponent<CharactersDescription>().Id == StartBattleScene.Enemy.Id);
        if (player != null && enemy != null)
        {
            PlayerObj = Instantiate(player, FatherPlayer);
            EnemyObj = Instantiate(enemy, FatherEnemy);
            _setAnimators();
        }
        else
        {
            Debug.LogError("Не найдены персонажи из списка ");
        }
    }
    public void ShowInventoryBattle()
    {

        panelInventory.BaseAtackObj.transform.GetChild(0).GetComponent<Image>().sprite = panelInventory.BaseAttack.SpriteItem;
        panelInventory.BaseBlockObj.transform.GetChild(0).GetComponent<Image>().sprite = panelInventory.BaseBlock.SpriteItem;

        for (int i = 0; i < 3; i++)
        {
            if (panelInventory.AttackBelt.Count > 0 && panelInventory.AttackBelt[i].id != 0)
            {

                panelInventory.AttackObj.transform.GetChild(i).GetComponent<Image>().sprite = panelInventory.AttackBelt[i].ItemData.SpriteItem;
            }
            else
            {
                panelInventory.AttackObj.transform.GetChild(i).GetComponent<Image>().sprite = BacgroundCellBoxAttack;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (panelInventory.BlockBelt.Count > 0 && panelInventory.BlockBelt[i].id != 0)
            {

                panelInventory.BlockObj.transform.GetChild(i).GetComponent<Image>().sprite = panelInventory.BlockBelt[i].ItemData.SpriteItem;
            }
            else
            {
                panelInventory.BlockObj.transform.GetChild(i).GetComponent<Image>().sprite = BacgroundCellBoxBlock;
            }
        }

    }
    public void ShowMainBoxesPlayer()
    {
        for (int i = 0; i < MainBoxPlayerBlock.transform.childCount; i++)
        {
            Sprite sprite = BacgroundCellBoxBlock;
            if (panelInventory.BoxBlockMain[i].id != 0)
            {
                sprite = panelInventory.BoxBlockMain[i].ItemData.SpriteItem;
            }
            MainBoxPlayerBlock.transform.GetChild(i).GetComponent<Image>().sprite = sprite;
        }
        for (int i = 0; i < MainBoxPlayerAttack.transform.childCount; i++)
        {
            Sprite sprite = BacgroundCellBoxAttack;
            if (panelInventory.BoxAttackMain[i].id != 0)
            {
                sprite = panelInventory.BoxAttackMain[i].ItemData.SpriteItem;
            }
            MainBoxPlayerAttack.transform.GetChild(i).GetComponent<Image>().sprite = sprite;
        }
    }
    public void ShowHistoryPanel()
    {
        GameObject obj = HistoryBox1;
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    obj = HistoryBox1;
                    break;
                case 1:
                    obj = HistoryBox2;
                    break;
                case 2:
                    obj = HistoryBox3;
                    break;
            }

            for (int j = 0; j < 3; j++)
            {
                Sprite sprite = FindSprite(arenaControler.HistoryPlayer[i, j]);
                obj.transform.GetChild(0).GetChild(j).GetComponent<Image>().sprite = sprite;

            }
            for (int j = 0; j < 3; j++)
            {
                Sprite sprite = FindSprite(arenaControler.HistoryEnemy[i, j]);
                obj.transform.GetChild(1).GetChild(j).GetComponent<Image>().sprite = sprite;
            }

        }
    }
    public void ShowBoxPanelsEnemy()
    {
        //Block
        for (int i = 0; i < 3; i++)
        {
            Sprite sprite = BacgroundCellBoxBlock;
            if (enemyAi.Block[i].id != 0)
            {
                sprite = enemyAi.Block[i].ItemData.SpriteItem;
            }
            MainBoxEnemyBlock.transform.GetChild(i).GetComponent<Image>().sprite = sprite;
        }
        //Attack
        for (int i = 0; i < 3; i++)
        {

            Sprite sprite = BacgroundCellBoxAttack;
            if (enemyAi.Attack[i].id != 0)
            {
                sprite = enemyAi.Attack[i].ItemData.SpriteItem;
            }
            MainBoxEnemyAttack.transform.GetChild(i).GetComponent<Image>().sprite = sprite;
        }
    }
    public void ShowEnemyPlanActions()
    {
        for (int i = 0; i < PlanAttacksEnemy.Count; i++)
        {
            PlanAttacksEnemy[i].sprite = enemyAi.PlanAttack[i].ItemData.SpriteItem;
        }
        for (int i = 0; i < PlanBlocksEnemy.Count; i++)
        {
            PlanBlocksEnemy[i].sprite = enemyAi.PlanBlock[i].ItemData.SpriteItem;
        }
    }
    public void CloseEnemyBoxesPanel()
    {
        //Block
        for (int i = 0; i < 3; i++)
        {
            Sprite sprite = CellLock;
            MainBoxEnemyBlock.transform.GetChild(i).GetComponent<Image>().sprite = sprite;
        }
        //Attack
        for (int i = 0; i < 3; i++)
        {

            Sprite sprite = CellLock;

            MainBoxEnemyAttack.transform.GetChild(i).GetComponent<Image>().sprite = sprite;
        }
    }
    public void ShowHpPlayerAndAnamy(CharactersDescription Player, CharactersDescription Enemy)
    {
        PlayerHealth.value = (float)Player.CurentHealth / (float)Player.MaxHp;
        EnemyHealth.value = (float)Enemy.CurentHealth / (float)Enemy.MaxHp;
        Debug.Log($"Enemy MaxHp = {Enemy.MaxHp} Curent Hp = {Enemy.CurentHealth}   Result  = {EnemyHealth.value}");
        Debug.Log($"Player MaxHp = {Player.MaxHp} Curent Hp = {Player.CurentHealth}   Result  = {PlayerHealth.value}");
    }
    Sprite FindSprite(TypeHistoryBoxes type)

    {
        switch (type)
        {
            case TypeHistoryBoxes.Empty:
                return CellEmpty;
            case TypeHistoryBoxes.BlockEmpty:
                return CellBlockEmpty;
            case TypeHistoryBoxes.Parry:
                return CellParry;
            case TypeHistoryBoxes.Hit:
                return CellHit;
            case TypeHistoryBoxes.Close:
                return CellLock;
            default:
                Debug.LogError("Не учтеный тип HistoryBox ");
                return CellLock;

        }

    }
}

