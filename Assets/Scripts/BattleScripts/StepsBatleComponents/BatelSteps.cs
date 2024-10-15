using System.Linq;
using UnityEngine;

public class BatelSteps : MonoBehaviour
{
    public EnemyAi enemyAi;
    public ArenaControler arenaControler;
    public PanelInventoryArena arenaInventoryArena;
    public static bool go = false;
    public bool startChoys = true;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        go = false;
    }
    public void GoNextStep()
    {
        if (!arenaInventoryArena.BoxAttackMain.Any(c => c.id != 0) && !arenaInventoryArena.BoxAttackMain.Any(c => c.id != 0))
        {
            return;
        }
        if (!go)
        {
            go = true;
            //�������� ������ �� ����� 
            //����� ���� Ai
            if (startChoys)
            { 
                enemyAi.MakeAChoice();
            }
            else
            {
                startChoys = false;
            }
            //����� ����� ����� ���������  
            arenaControler.CorutinBatle();

            //�������� ���� ������ 
            //���������� ����� �� ����������� BOXES 
            //��������� ����� ��������� 
            arenaControler.wizual.ShowEnemyPlanActions();
        }
    }
}
