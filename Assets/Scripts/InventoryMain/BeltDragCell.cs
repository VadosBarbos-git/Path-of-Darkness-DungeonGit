using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeltDragCell : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    Inventory inventory;
    Canvas _canvas;

    public int nomberCell;
    public bool IsAttackAptitude = false;
    private GameObject _dragObject;

    void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("Inventory").transform.GetComponent<Inventory>();
        _canvas = GameObject.FindGameObjectWithTag("InventoryCanvas").transform.GetComponent<Canvas>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventory.CloseDescriptionItem();
        if (Inventory.BeltBagCells[nomberCell].id == 0)
        {
            return;
        }
        Inventory.curentNomberDragCell = nomberCell;
        Inventory.CurentCell = new Cell(Inventory.BeltBagCells[nomberCell]);
        Sprite sprite = Inventory.BeltBagCells[nomberCell].ItemData.SpriteItem;
        inventory.DragObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        inventory.DragObject.SetActive(true);
        _dragObject = inventory.DragObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Inventory.BeltBagCells[nomberCell].id == 0)
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
        if (Inventory.CurentCell == null || !Inventory.CurentCell.ItemData._isAptitudes)
        {
            return;
        }
        if (IsAttackAptitude && Inventory.CurentCell.ItemData is ItemAptitudes aptitud && aptitud.typeAptitude != TypeAptitudes.Attack)
        {
            return;
        }
        else if (!IsAttackAptitude && Inventory.CurentCell.ItemData is ItemAptitudes aptitud2 && aptitud2.typeAptitude != TypeAptitudes.Block)
        {
            return;
        }

        /* if (Inventory.CurentCell.ItemData._isAptitudes && IsAttackAptitude)
         {
             ItemAptitudes it = (ItemAptitudes)Inventory.CurentCell.ItemData;
             if (it.typeAptitude == TypeAptitudes.Block)
             {

             }
         }*/
        GameObject obj = eventData.pointerDrag;
      /*  if (obj.transform.GetComponent<DragMerchant>())
        {

            if (Inventory.CurentCell.countItem > 1)
            {
                if (Inventory.BeltBagCells[nomberCell].id == 0)
                {
                    Inventory.MerchanCells[Inventory.curentNomberDragCell].countItem--;
                    Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                    Inventory.BeltBagCells[nomberCell].countItem = 1;
                }
                else
                {
                    Inventory.MerchanCells[Inventory.curentNomberDragCell].countItem--;
                    if (Inventory.MerchanCells.Any(call => call.id == Inventory.BeltBagCells[nomberCell].id))
                    {
                        int index = Inventory.SaveChestCells.FindIndex(cell => cell.id == Inventory.BeltBagCells[nomberCell].id);
                        Inventory.MerchanCells[index].countItem++;
                    }
                    else
                    {
                        int index = Inventory.MerchanCells.FindIndex(cell => cell.id == 0);
                        Inventory.MerchanCells[index] = new Cell(Inventory.BeltBagCells[nomberCell]);
                    }
                    Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                    Inventory.BeltBagCells[nomberCell].countItem = 1;
                }
            }
            else
            {
                if (Inventory.MerchanCells.Any(call => call.id == Inventory.BeltBagCells[nomberCell].id))
                {
                    int index = Inventory.MerchanCells.FindIndex(cell => cell.id == Inventory.BeltBagCells[nomberCell].id);
                    Inventory.MerchanCells[index].countItem++;
                    Inventory.MerchanCells[Inventory.curentNomberDragCell] = new Cell();
                }
                else
                {
                    Cell cell = new(Inventory.BeltBagCells[nomberCell]);
                    Inventory.MerchanCells[Inventory.curentNomberDragCell] = cell;
                }
                Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                Inventory.BeltBagCells[nomberCell].countItem = 1;
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;


            inventory.ShowBeltBagCells();
            inventory.ShowBagCells();
            inventory.ShowMerchantCells();        }
        else*/  if (obj.transform.GetComponent<DragSaveChest>())
        {

            if (Inventory.CurentCell.countItem > 1)
            {
                if (Inventory.BeltBagCells[nomberCell].id == 0)
                {
                    Inventory.SaveChestCells[Inventory.curentNomberDragCell].countItem--;
                    Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                    Inventory.BeltBagCells[nomberCell].countItem = 1;
                }
                else
                {
                    Inventory.SaveChestCells[Inventory.curentNomberDragCell].countItem--;
                    if (Inventory.SaveChestCells.Any(call => call.id == Inventory.BeltBagCells[nomberCell].id))
                    {
                        int index = Inventory.SaveChestCells.FindIndex(cell => cell.id == Inventory.BeltBagCells[nomberCell].id);
                        Inventory.SaveChestCells[index].countItem++;
                    }
                    else
                    {
                        int index = Inventory.SaveChestCells.FindIndex(cell => cell.id == 0);
                        Inventory.SaveChestCells[index] = new Cell(Inventory.BeltBagCells[nomberCell]);
                    }
                    Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                    Inventory.BeltBagCells[nomberCell].countItem = 1;
                }
            }
            else
            {
                if (Inventory.SaveChestCells.Any(call => call.id == Inventory.BeltBagCells[nomberCell].id))
                {
                    int index = Inventory.SaveChestCells.FindIndex(cell => cell.id == Inventory.BeltBagCells[nomberCell].id);
                    Inventory.SaveChestCells[index].countItem++;
                    Inventory.SaveChestCells[Inventory.curentNomberDragCell] = new Cell();
                }
                else
                {
                    Cell cell = new(Inventory.BeltBagCells[nomberCell]);
                    Inventory.SaveChestCells[Inventory.curentNomberDragCell] = cell;
                }
                Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                Inventory.BeltBagCells[nomberCell].countItem = 1;
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;


            inventory.ShowBeltBagCells();
            inventory.ShowBagCells();
            inventory.ShowSaveChestCells();
        }
        else if (obj.transform.GetComponent<DragCell>())
        {

            if (Inventory.CurentCell.countItem > 1)
            {
                if (Inventory.BeltBagCells[nomberCell].id == 0)
                {
                    Inventory.BagCells[Inventory.curentNomberDragCell].countItem--;
                    Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                    Inventory.BeltBagCells[nomberCell].countItem = 1;
                }
                else
                {
                    Inventory.BagCells[Inventory.curentNomberDragCell].countItem--;
                    if (Inventory.BagCells.Any(call => call.id == Inventory.BeltBagCells[nomberCell].id))
                    {
                        int index = Inventory.BagCells.FindIndex(cell => cell.id == Inventory.BeltBagCells[nomberCell].id);
                        Inventory.BagCells[index].countItem++;
                    }
                    else
                    {
                        int index = Inventory.BagCells.FindIndex(cell => cell.id == 0);
                        Inventory.BagCells[index] = new Cell(Inventory.BeltBagCells[nomberCell]);
                    }
                    Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                    Inventory.BeltBagCells[nomberCell].countItem = 1;
                }
            }
            else
            {
                if (Inventory.BagCells.Any(call => call.id == Inventory.BeltBagCells[nomberCell].id))
                {
                    int index = Inventory.BagCells.FindIndex(cell => cell.id == Inventory.BeltBagCells[nomberCell].id);
                    Inventory.BagCells[index].countItem++;
                    Inventory.BagCells[Inventory.curentNomberDragCell] = new Cell();
                }
                else
                {
                    Cell cell = new(Inventory.BeltBagCells[nomberCell]);
                    Inventory.BagCells[Inventory.curentNomberDragCell] = cell;
                }
                Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);
                Inventory.BeltBagCells[nomberCell].countItem = 1;
            }

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;


            inventory.ShowBeltBagCells();
            inventory.ShowBagCells();

        }
        else if (obj.transform.GetComponent<BeltDragCell>())
        {
            Cell cell = new Cell(Inventory.BeltBagCells[nomberCell]);
            Inventory.BeltBagCells[Inventory.curentNomberDragCell] = cell;
            Inventory.BeltBagCells[nomberCell] = new Cell(Inventory.CurentCell);

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBeltBagCells();
        }


    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Inventory.BeltBagCells[nomberCell].id != 0)
        {
            inventory.OpenDescriptionItem(Inventory.BeltBagCells[nomberCell]);
        }
    }
}
