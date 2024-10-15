using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSaveChest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
{
    Inventory inventory;
    Canvas _canvas;
    public int nomberCell;
    private GameObject _dragObject;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").transform.GetComponent<Inventory>();
        _canvas = GameObject.FindGameObjectWithTag("InventoryCanvas").transform.GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        inventory.CloseDescriptionItem();
        if (Inventory.SaveChestCells[nomberCell].id == 0)
        {
            return;
        }
        Inventory.curentNomberDragCell = nomberCell;
        Inventory.CurentCell = Inventory.SaveChestCells[nomberCell];
        Sprite sprite = Inventory.SaveChestCells[nomberCell].ItemData.SpriteItem;
        inventory.DragObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        inventory.DragObject.SetActive(true);
        _dragObject = inventory.DragObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Inventory.SaveChestCells[nomberCell].id == 0)
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
        Sprite sprite = inventory.backgroundBagCell;
        inventory.DragObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        inventory.DragObject.SetActive(false);
        _dragObject = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Inventory.CurentCell == null)
        {
            return;
        }
        GameObject obj = eventData.pointerDrag;
        if (obj.transform.GetComponent<DragSaveChest>())
        {
            Cell cell = new Cell(Inventory.SaveChestCells[nomberCell]);
            Inventory.SaveChestCells[Inventory.curentNomberDragCell] = cell;
            Inventory.SaveChestCells[nomberCell] = new Cell(Inventory.CurentCell);

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowSaveChestCells();
        }
        else if (obj.transform.GetComponent<DragCell>())
        {
            if (Inventory.CurentCell.ItemData is ItemAptitudes aptitud &&
               Inventory.SaveChestCells.FirstOrDefault(cell => cell.id == aptitud.id) != null)
            {
                int index = Inventory.SaveChestCells.FindIndex(cell => cell.id == Inventory.CurentCell.id);
                Inventory.SaveChestCells[index].countItem += Inventory.CurentCell.countItem;
                Inventory.BagCells[Inventory.curentNomberDragCell] = new Cell();
            }
            else
            {
                Cell cell = new Cell(Inventory.SaveChestCells[nomberCell]);
                Inventory.BagCells[Inventory.curentNomberDragCell] = cell;
                Inventory.SaveChestCells[nomberCell] = new Cell(Inventory.CurentCell);
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBagCells();
            inventory.ShowSaveChestCells();
        }
        else if (obj.transform.GetComponent<BeltDragCell>())
        {
            Cell cell = new Cell(Inventory.SaveChestCells[nomberCell]);
            if (cell.countItem > 1)
            {
                return;
            }
            if (Inventory.SaveChestCells.Any(call => call.id == Inventory.CurentCell.id))
            {
                int index = Inventory.SaveChestCells.FindIndex(cell => cell.id == Inventory.CurentCell.id);
                Inventory.SaveChestCells[index].countItem++;
                Inventory.BeltBagCells[Inventory.curentNomberDragCell] = new Cell();
            }
            else
            {
                Inventory.BeltBagCells[Inventory.curentNomberDragCell] = cell;
                Inventory.SaveChestCells[nomberCell] = new Cell(Inventory.CurentCell);
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBeltBagCells();
            inventory.ShowSaveChestCells();
        }


    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Inventory.SaveChestCells[nomberCell].id != 0)
        {
            inventory.OpenDescriptionItem(Inventory.SaveChestCells[nomberCell]);
        }
    }
}
