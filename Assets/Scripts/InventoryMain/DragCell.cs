
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
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
        if (Inventory.BagCells[nomberCell].id == 0)
        {
            return;
        }
        Inventory.curentNomberDragCell = nomberCell;
        Inventory.CurentCell = Inventory.BagCells[nomberCell];
        Sprite sprite = Inventory.BagCells[nomberCell].ItemData.SpriteItem;
        inventory.DragObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        inventory.DragObject.SetActive(true);
        _dragObject = inventory.DragObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Inventory.BagCells[nomberCell].id == 0)
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
        if (obj.transform.GetComponent<DragMerchant>())
        {
            if (!tryBuyItem(Inventory.CurentCell.ItemData.Price * Inventory.CurentCell.countItem))
            {
                return;
            }
            else
            {
                inventory.ShowCoins();
            }

            if (Inventory.CurentCell.ItemData is ItemAptitudes aptitud &&
                Inventory.BagCells.FirstOrDefault(cell => cell.id == aptitud.id) != null)
            {
                int index = Inventory.BagCells.FindIndex(cell => cell.id == Inventory.CurentCell.id);
                Inventory.BagCells[index].countItem += Inventory.CurentCell.countItem;
                Inventory.MerchanCells[Inventory.curentNomberDragCell] = new Cell();
            }
            else
            {
                Cell cell = new Cell(Inventory.BagCells[nomberCell]);
                Inventory.MerchanCells[Inventory.curentNomberDragCell] = cell;
                Inventory.BagCells[nomberCell] = new Cell(Inventory.CurentCell);
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowMerchantCells();
            inventory.ShowBagCells();
        }
        else if (obj.transform.GetComponent<DragSaveChest>())
        {

            if (Inventory.CurentCell.ItemData is ItemAptitudes aptitud &&
                Inventory.BagCells.FirstOrDefault(cell => cell.id == aptitud.id) != null)
            {
                int index = Inventory.BagCells.FindIndex(cell => cell.id == Inventory.CurentCell.id);
                Inventory.BagCells[index].countItem += Inventory.CurentCell.countItem;
                Inventory.SaveChestCells[Inventory.curentNomberDragCell] = new Cell();
            }
            else
            {
                Cell cell = new Cell(Inventory.BagCells[nomberCell]);
                Inventory.SaveChestCells[Inventory.curentNomberDragCell] = cell;
                Inventory.BagCells[nomberCell] = new Cell(Inventory.CurentCell);
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowSaveChestCells();
            inventory.ShowBagCells();
        }
        else if (obj.transform.GetComponent<DragCell>())
        {
            Cell cell = new Cell(Inventory.BagCells[nomberCell]);
            Inventory.BagCells[Inventory.curentNomberDragCell] = cell;
            Inventory.BagCells[nomberCell] = new Cell(Inventory.CurentCell);

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBagCells();
        }
        else if (obj.transform.GetComponent<BeltDragCell>())
        {
            Cell cell = new Cell(Inventory.BagCells[nomberCell]);
            if (cell.countItem > 1)
            {
                return;
            }
            if (Inventory.BagCells.Any(call => call.id == Inventory.CurentCell.id))
            {
                int index = Inventory.BagCells.FindIndex(cell => cell.id == Inventory.CurentCell.id);
                Inventory.BagCells[index].countItem++;
                Inventory.BeltBagCells[Inventory.curentNomberDragCell] = new Cell();
            }
            else
            {
                Inventory.BeltBagCells[Inventory.curentNomberDragCell] = cell;
                Inventory.BagCells[nomberCell] = new Cell(Inventory.CurentCell);
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBeltBagCells();
            inventory.ShowBagCells();
        }
        else if (obj.transform.GetComponent<BodyDragCell>())
        {
            if (Inventory.CurentCell.ItemData is not ItemWearables)
            {
                Debug.LogError("Ошибка На теле находится несоответсвующий Item");
                return;
            }
            TypeWearables Curenttype = Inventory.CurentCell.ItemData is ItemWearables item ? item.typeWearables : TypeWearables.Head;

            if (Inventory.BagCells[nomberCell].id == 0 ||
                Inventory.BagCells[nomberCell].ItemData is ItemWearables wear && wear.typeWearables == Curenttype)
            {
                Cell cell = Inventory.BagCells[nomberCell];
                Inventory.BagCells[nomberCell] = new Cell(Inventory.CurentCell);
                int index = Inventory.BodyCells.FindIndex(cell => cell.ItemData is ItemWearables wear && wear.typeWearables == Curenttype);
                Inventory.BodyCells[index] = new Cell(cell);
            }
            Inventory.CurentCell = null;
            Inventory.curentNomberDragCell = -1;
            inventory.ShowBagCells();
            inventory.ShowBodyBagCells();
        }

    }
    bool tryBuyItem(int price)
    {
        return StaticInventory.TrySpendCoin(price);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Inventory.BagCells[nomberCell].id != 0)
        {
            inventory.OpenDescriptionItem(Inventory.BagCells[nomberCell]);
        }
    }


}
