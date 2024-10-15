using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemCell : MonoBehaviour, IDropHandler
{
    Inventory inventory;
    private GameObject _dragObject;

    private void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("Inventory").transform.GetComponent<Inventory>();

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
            Inventory.SaveChestCells[Inventory.curentNomberDragCell] = new Cell();


            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowSaveChestCells();
            inventory.ShowBagCells();
        }
        else if (obj.transform.GetComponent<DragCell>())
        {

            Inventory.BagCells[Inventory.curentNomberDragCell] = new Cell();

            Inventory.curentNomberDragCell = -1;
            Inventory.CurentCell = null;
            inventory.ShowBagCells();
        }
        else if (obj.transform.GetComponent<BeltDragCell>())
        {

            Inventory.BeltBagCells[Inventory.curentNomberDragCell] = new Cell();
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
            int index = Inventory.BodyCells.FindIndex(cell => cell.ItemData is ItemWearables wear && wear.typeWearables == Curenttype);
            Inventory.BodyCells[index] = new Cell();


            Inventory.CurentCell = null;
            Inventory.curentNomberDragCell = -1;
            inventory.ShowBagCells();
            inventory.ShowBodyBagCells();
        }
    }


}
