
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrigerBattleInvemtory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    Canvas _canvas;
    GameObject _dragObject;
    public int nomber;
    public TypeCellInArena typeCell;
    PanelInventoryArena inventoryArena;

    public GameObject descriptionItem;
    private Image descImage;
    private TextMeshProUGUI descName;
    private TextMeshProUGUI descInfo;

    public void Start()
    {
        inventoryArena = GameObject.FindGameObjectWithTag("InventoryArena").GetComponentInChildren<PanelInventoryArena>();
        _canvas = GameObject.FindGameObjectWithTag("ArenaCanvas").transform.GetComponent<Canvas>();


        descImage = descriptionItem.transform.GetChild(0).GetComponent<Image>();
        descName = descriptionItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        descInfo = descriptionItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        descriptionItem.SetActive(false);


    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        descriptionItem.SetActive(false);
        if (!inventoryArena.SetCurentCell(nomber))
        {
            return;
        }
        inventoryArena.CurentTypeCell = typeCell;
        _dragObject = inventoryArena.DragCellObj;
        _dragObject.transform.GetChild(0).GetComponent<Image>().sprite = inventoryArena.CurentCell.ItemData.SpriteItem;
        _dragObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventoryArena.CurentCell.id == 0)
        {
            return;
        }
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
        eventData.position,
            _canvas.worldCamera,
            out localPoint);
        _dragObject.transform.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventoryArena.CurentCell.id == 0)
        {
            return;
        }
        inventoryArena.CurentCell = null;
        _dragObject.SetActive(false);
    }
    public void OnDrop(PointerEventData eventData)
    {

        if (inventoryArena.CurentCell != null && inventoryArena.CurentCell.id != 0)
        {


        }
        inventoryArena.ShowInventory();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Cell cell = inventoryArena._findCellinArray(nomber);
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
