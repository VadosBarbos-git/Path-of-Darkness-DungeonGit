
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMerchant : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
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
        if (Inventory.MerchanCells[nomberCell].id == 0)
        {
            return;
        }
        Inventory.curentNomberDragCell = nomberCell;
        Inventory.CurentCell = Inventory.MerchanCells[nomberCell];
        Sprite sprite = Inventory.MerchanCells[nomberCell].ItemData.SpriteItem;
        inventory.DragObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        inventory.DragObject.SetActive(true);
        _dragObject = inventory.DragObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Inventory.MerchanCells[nomberCell].id == 0)
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
            Cell cell = new Cell(Inventory.MerchanCells[nomberCell]);
            Inventory.MerchanCells[Inventory.curentNomberDragCell] = cell;
            Inventory.MerchanCells[nomberCell] = new Cell(Inventory.CurentCell);

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowMerchantCells();
        }
        else if (obj.transform.GetComponent<DragCell>())
        {
            if (Inventory.CurentCell.ItemData is ItemAptitudes aptitud &&
               Inventory.MerchanCells.FirstOrDefault(cell => cell.id == aptitud.id) != null)
            {
                int index = Inventory.MerchanCells.FindIndex(cell => cell.id == Inventory.CurentCell.id);
                Inventory.MerchanCells[index].countItem += Inventory.CurentCell.countItem;
                Inventory.BagCells[Inventory.curentNomberDragCell] = new Cell();
                SaleItem((int)(Inventory.MerchanCells[index].ItemData.Price * Inventory.CurentCell.countItem));
            }
            else
            {
                Cell cell = new Cell(Inventory.MerchanCells[nomberCell]);
                Inventory.BagCells[Inventory.curentNomberDragCell] = cell;
                Inventory.MerchanCells[nomberCell] = new Cell(Inventory.CurentCell);
                SaleItem(Inventory.CurentCell.ItemData.Price * Inventory.CurentCell.countItem);
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBagCells();
            inventory.ShowMerchantCells();
        }
        else if (obj.transform.GetComponent<BeltDragCell>())
        {
            Cell cell = new Cell(Inventory.MerchanCells[nomberCell]);
            if (cell.countItem > 1)
            {
                return;
            }
            if (Inventory.MerchanCells.Any(call => call.id == Inventory.CurentCell.id))
            {
                int index = Inventory.MerchanCells.FindIndex(cell => cell.id == Inventory.CurentCell.id);
                Inventory.MerchanCells[index].countItem++;
                Inventory.BeltBagCells[Inventory.curentNomberDragCell] = new Cell();
                SaleItem(Inventory.MerchanCells[index].ItemData.Price);
            }
            else
            {
                Inventory.BeltBagCells[Inventory.curentNomberDragCell] = cell;
                Inventory.MerchanCells[nomberCell] = new Cell(Inventory.CurentCell);
                SaleItem(Inventory.CurentCell.ItemData.Price);
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBeltBagCells();
            inventory.ShowMerchantCells();
        }


    }

    void SaleItem(int price)
    {
        StaticInventory.AddCoin(price);
        inventory.ShowCoins();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Inventory.MerchanCells[nomberCell].id != 0)
        {
            inventory.OpenDescriptionItem(Inventory.MerchanCells[nomberCell]);
        }
    }
}
