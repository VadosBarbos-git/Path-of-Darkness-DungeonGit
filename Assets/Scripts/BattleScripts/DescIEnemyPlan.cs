
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescIEnemyPlan : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject descriptionItem;
    private Image descImage;
    private TextMeshProUGUI descName;
    private TextMeshProUGUI descInfo;
    public bool isPlanAttack;
    public int nomber;
    private EnemyAi enemyAi;
    // Start is called before the first frame update
    void Start()
    {
        enemyAi = GameObject.FindGameObjectWithTag("EnemyAi").GetComponent<EnemyAi>();

        descImage = descriptionItem.transform.GetChild(0).GetComponent<Image>();
        descName = descriptionItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        descInfo = descriptionItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Cell cell = new Cell();
        if (isPlanAttack)
        {
            cell = enemyAi.PlanAttack[nomber];
        }
        else
        {
            cell = enemyAi.PlanBlock[nomber];
        }
        if (cell.id == 0)
        { return; }
        descriptionItem.SetActive(true);
        descImage.sprite = cell.ItemData.SpriteItem;
        descName.text = cell.ItemData.nameItem;
        descInfo.text = cell.ItemData._description;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        descriptionItem.SetActive(false);
    }
}
