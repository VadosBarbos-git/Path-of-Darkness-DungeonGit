
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxArena : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    Canvas _canvas;
    GameObject _dragObject;
    public int nomber;
    public TypeCellInArena typeCell;
    PanelInventoryArena inventoryArena;

    private GameObject descriptionItem;
    private Image descImage;
    private TextMeshProUGUI descName;
    private TextMeshProUGUI descInfo;
    public void Start()
    {
        inventoryArena = GameObject.FindGameObjectWithTag("InventoryArena").GetComponentInChildren<PanelInventoryArena>();
        _canvas = GameObject.FindGameObjectWithTag("ArenaCanvas").transform.GetComponent<Canvas>();

        descriptionItem = inventoryArena.descriptionItem;
        descImage = inventoryArena.descImage;
        descName = inventoryArena.descName;
        descInfo = inventoryArena.descInfo;

        descriptionItem.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!inventoryArena.SetCurentCell(nomber, typeCell))
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
            if (typeCell == TypeCellInArena.boxAttack && inventoryArena.CurentTypeCell == typeCell)
            {
                inventoryArena.BoxAttackMain.Clear();
                inventoryArena.BoxAttackMain = EmptyBoxCells();
                inventoryArena.BoxAttackMain[nomber] = new Cell(inventoryArena.CurentCell);
            }
            else if (typeCell == TypeCellInArena.boxBlock && inventoryArena.CurentTypeCell == typeCell)
            {
                inventoryArena.BoxBlockMain.Clear();
                inventoryArena.BoxBlockMain = EmptyBoxCells();
                inventoryArena.BoxBlockMain[nomber] = new Cell(inventoryArena.CurentCell);
            }
            else if (typeCell == TypeCellInArena.boxBlock && inventoryArena.CurentTypeCell == TypeCellInArena.Block)
            {
                if (inventoryArena.BoxBlockMain.Any(c => c.id != 0))
                {
                    int index = inventoryArena.BlockBelt.FindIndex(c => c == inventoryArena.CurentCell);
                    if ((inventoryArena.BoxBlockMain.FirstOrDefault(c => c.id != 0).id != inventoryArena.BaseBlock.id))
                    {
                        inventoryArena.BlockBelt[index] = new Cell(inventoryArena.BoxBlockMain.FirstOrDefault(c => c.id != 0));
                    }
                    else
                    {
                        inventoryArena.BlockBelt[index] = new Cell();
                    }
                    inventoryArena.BoxBlockMain.Clear();
                    inventoryArena.BoxBlockMain = EmptyBoxCells();
                    inventoryArena.BoxBlockMain[nomber] = new Cell(inventoryArena.CurentCell);
                }
                else
                {
                    inventoryArena.BoxBlockMain.Clear();
                    inventoryArena.BoxBlockMain = EmptyBoxCells();
                    inventoryArena.BoxBlockMain[nomber] = new Cell(inventoryArena.CurentCell);
                    int index = inventoryArena.BlockBelt.FindIndex(c => c == inventoryArena.CurentCell);
                    inventoryArena.BlockBelt[index] = new Cell();
                }
            }
            else if (typeCell == TypeCellInArena.boxAttack && inventoryArena.CurentTypeCell == TypeCellInArena.Attack)
            {
                if (inventoryArena.BoxAttackMain.Any(c => c.id != 0))
                {
                    int index = inventoryArena.AttackBelt.FindIndex(c => c == inventoryArena.CurentCell);
                    if ((inventoryArena.BoxAttackMain.FirstOrDefault(c => c.id != 0).id != inventoryArena.BaseAttack.id))
                    {
                        inventoryArena.AttackBelt[index] = new Cell(inventoryArena.BoxAttackMain.FirstOrDefault(c => c.id != 0));
                    }
                    else
                    {
                        inventoryArena.AttackBelt[index] = new Cell();
                    }
                    inventoryArena.BoxAttackMain.Clear();
                    inventoryArena.BoxAttackMain = EmptyBoxCells();
                    inventoryArena.BoxAttackMain[nomber] = new Cell(inventoryArena.CurentCell);
                }
                else
                {
                    inventoryArena.BoxAttackMain.Clear();
                    inventoryArena.BoxAttackMain = EmptyBoxCells();
                    inventoryArena.BoxAttackMain[nomber] = new Cell(inventoryArena.CurentCell);
                    int index = inventoryArena.AttackBelt.FindIndex(c => c == inventoryArena.CurentCell);
                    inventoryArena.AttackBelt[index] = new Cell();
                }
            }
            else if (typeCell == TypeCellInArena.boxBlock && inventoryArena.CurentTypeCell == TypeCellInArena.BaseBlock)
            {
                if (inventoryArena.BoxBlockMain.Any(c => c.id != 0))
                {
                    if (inventoryArena.BoxBlockMain.FirstOrDefault(c => c.id != 0).id == inventoryArena.CurentCell.id)
                    {
                        inventoryArena.BoxBlockMain.Clear();
                        inventoryArena.BoxBlockMain = EmptyBoxCells();
                        inventoryArena.BoxBlockMain[nomber] = new Cell(inventoryArena.CurentCell);
                    }
                    else
                    {
                        int index = inventoryArena.BlockBelt.FindIndex(c => c.id == 0);
                        inventoryArena.BlockBelt[index] = new Cell(inventoryArena.BoxBlockMain.FirstOrDefault(c => c.id != 0));
                        inventoryArena.BoxBlockMain.Clear();
                        inventoryArena.BoxBlockMain = EmptyBoxCells();
                        inventoryArena.BoxBlockMain[nomber] = new Cell(inventoryArena.CurentCell);
                    }
                }
                else
                {
                    inventoryArena.BoxBlockMain.Clear();
                    inventoryArena.BoxBlockMain = EmptyBoxCells();
                    inventoryArena.BoxBlockMain[nomber] = new Cell(inventoryArena.CurentCell);
                }
            }
            else if (typeCell == TypeCellInArena.boxAttack && inventoryArena.CurentTypeCell == TypeCellInArena.BaseAttack)
            {
                if (inventoryArena.BoxAttackMain.Any(c => c.id != 0))
                {
                    if (inventoryArena.BoxAttackMain.FirstOrDefault(c => c.id != 0).id == inventoryArena.CurentCell.id)
                    {
                        inventoryArena.BoxAttackMain.Clear();
                        inventoryArena.BoxAttackMain = EmptyBoxCells();
                        inventoryArena.BoxAttackMain[nomber] = new Cell(inventoryArena.CurentCell);
                    }
                    else
                    {
                        int index = inventoryArena.AttackBelt.FindIndex(c => c.id == 0);
                        inventoryArena.AttackBelt[index] = new Cell(inventoryArena.BoxAttackMain.FirstOrDefault(c => c.id != 0));
                        inventoryArena.BoxAttackMain.Clear();
                        inventoryArena.BoxAttackMain = EmptyBoxCells();
                        inventoryArena.BoxAttackMain[nomber] = new Cell(inventoryArena.CurentCell);
                    }
                }
                else
                {
                    inventoryArena.BoxAttackMain.Clear();
                    inventoryArena.BoxAttackMain = EmptyBoxCells();
                    inventoryArena.BoxAttackMain[nomber] = new Cell(inventoryArena.CurentCell);
                }
            }

        }
        inventoryArena.ShowInventory();
    }
    List<Cell> EmptyBoxCells() => new List<Cell> { new Cell(), new Cell(), new Cell() };

    public void OnPointerDown(PointerEventData eventData)
    {
        Cell cell = new Cell();
        if (typeCell == TypeCellInArena.boxAttack)
        {
            cell = new Cell(inventoryArena.BoxAttackMain[nomber]);
        }
        else if (typeCell == TypeCellInArena.boxBlock)
        {
            cell = new Cell(inventoryArena.BoxBlockMain[nomber]);
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


