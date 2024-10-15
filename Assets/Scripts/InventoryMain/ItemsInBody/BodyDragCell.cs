using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyDragCell : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    Inventory inventory;
    Canvas _canvas;


    public TypeWearables typeWearables;
    private GameObject _dragObject;

    void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("Inventory").transform.GetComponent<Inventory>();
        _canvas = GameObject.FindGameObjectWithTag("InventoryCanvas").transform.GetComponent<Canvas>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Inventory.BodyCells.Any(item => item.ItemData is ItemWearables wearables && wearables.typeWearables == typeWearables))
        {
            return;
        }
        Inventory.curentNomberDragCell = -1;
        Cell cell = Inventory.BodyCells.FirstOrDefault(item => item.ItemData is ItemWearables wearables && wearables.typeWearables == typeWearables);
        Inventory.CurentCell = new Cell(cell);
        Sprite sprite = cell.ItemData.SpriteItem;
        inventory.DragObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        inventory.DragObject.SetActive(true);
        _dragObject = inventory.DragObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Inventory.BodyCells.Any(item => item.ItemData is ItemWearables wearables && wearables.typeWearables == typeWearables))
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

        Inventory.curentNomberDragCell = -1;
        Inventory.CurentCell = null;
        Sprite sprite = inventory.backgroundBeltBagCellBlock;
        inventory.DragObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        inventory.DragObject.SetActive(false);
        _dragObject = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        if (obj.transform.GetComponent<DragCell>())
        {
            if (Inventory.CurentCell.ItemData is not ItemWearables)
            {
                return;
            }
            TypeWearables Curenttype = Inventory.CurentCell.ItemData is ItemWearables item ? item.typeWearables : TypeWearables.Head;
            if (typeWearables == Curenttype)
            {
                Cell cell = Inventory.BodyCells.FirstOrDefault(item => item.ItemData is ItemWearables wear && wear.typeWearables == typeWearables);
                int index = -1;
                if (cell == null)
                {
                    cell = new Cell();
                    index = Inventory.BodyCells.FindIndex(cell => cell.id == 0);
                }
                else
                {
                    index = Inventory.BodyCells.FindIndex(cell => cell.ItemData is ItemWearables wear && wear.typeWearables == typeWearables);
                }

                Inventory.BagCells[Inventory.curentNomberDragCell] = new Cell(cell);
                Inventory.BodyCells[index] = new Cell(Inventory.CurentCell);
            }
            Inventory.CurentCell = null;
            Inventory.curentNomberDragCell = -1;
            inventory.ShowBagCells();
            inventory.ShowBodyBagCells();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Cell cell = Inventory.BodyCells.FirstOrDefault(item => item.ItemData is ItemWearables wearables && wearables.typeWearables == typeWearables);
        if (cell != null && cell.id != 0)
        {
            inventory.OpenDescriptionItem(cell);
        }
    }
}
