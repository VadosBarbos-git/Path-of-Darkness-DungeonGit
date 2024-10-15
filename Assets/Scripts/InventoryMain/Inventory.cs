using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject Bag;
    public GameObject BodyBag;
    public GameObject BeltBag;
    public GameObject ChestSave;
    public GameObject MerchanObj;

    public Sprite backgroundBagCell;
    public Sprite backgroundBeltBagCellAttack;
    public Sprite backgroundBeltBagCellBlock;

    public List<Sprite> BodybackgraundSprits;

    public List<Item> Aptitude;
    public List<Item> Wearables;
    public List<Item> MerchentItems;

    public static List<Cell> BagCells = new List<Cell>();
    public static List<Cell> BeltBagCells = new List<Cell>();
    public static List<Cell> BodyCells = new List<Cell>();
    public static List<Cell> SaveChestCells = new List<Cell>();
    public static List<Cell> MerchanCells = new List<Cell>();

    public static Cell CurentCell;
    public static int curentNomberDragCell;
    public GameObject DragObject;
    public TextMeshProUGUI textCoin;
    [SerializeField] private GameObject DescriptionGameObject;
    public static bool isFirstStart = false;

    private void Start()
    {

        StaticInventory.OpenInventory();
        if (!isFirstStart)
        {
            for (int i = 0; i < MerchentItems.Count; i++)
            {
                int value = 1;
                if (MerchentItems[i] is ItemAptitudes)
                {
                    value = Random.Range(1, 5);
                }
                MerchanCells[i] = new Cell(MerchentItems[i], value);
            }
            isFirstStart = true;
        }
        // AddSameItem();
        ShowBagCells();
        ShowBodyBagCells();
        ShowBeltBagCells();
        ShowSaveChestCells();
        ShowMerchantCells();


        ShowCoins();
    }
    private void OnEnable()
    {
        StaticInventory.OpenInventory();
        ShowBagCells();
        ShowBodyBagCells();
        ShowBeltBagCells();
        ShowSaveChestCells();
        ShowMerchantCells();
        ShowCoins();
    }
    private void OnDisable()
    {
        StaticInventory.CloseInventory();
        AddBonusesToPlayer();
    }
    void AddSameItem()
    {
        for (int i = 0; i < 9; i++)
        {
            int aptitud = Random.Range(0, Aptitude.Count);
            if (!Stack(Aptitude[aptitud]))
            {
                Cell ap = BagCells.FirstOrDefault(cell => cell.id == 0);
                ap.ItemData = Aptitude[aptitud];
                ap.id = ap.ItemData.id;
                ap.countItem = 1;
            }
        }

        for (int i = 0; i < 6; i++)
        {

            int wearables = Random.Range(0, Wearables.Count);

            if (!Stack(Wearables[wearables]))
            {
                Cell we = BagCells.FirstOrDefault(cell => cell.id == 0);
                we.ItemData = Wearables[wearables];
                we.id = we.ItemData.id;
                we.countItem = 1;
            }
        }


    }
    bool Stack(Item item)
    {
        if (item._isStakable && BagCells.Any(cell => cell.id != 0 && cell.ItemData.id == item.id))
        {
            Cell cell = BagCells.FirstOrDefault(cell => cell.ItemData.id == item.id);
            cell.countItem += 1;
            return true;
        }
        return false;
    }
    public void ShowBagCells()
    {
        for (int i = 0; i < Bag.transform.childCount; i++)
        {
            if (BagCells[i].id == 0)
            {
                Bag.transform.GetChild(i).GetComponent<Image>().sprite = backgroundBagCell;
                Bag.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Bag.transform.GetChild(i).GetComponent<Image>().sprite = BagCells[i].ItemData.SpriteItem;
                if (BagCells[i].ItemData._isStakable)
                {
                    Bag.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    Bag.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = BagCells[i].countItem.ToString();

                }
                else
                {
                    Bag.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
    public void ShowSaveChestCells()
    {
        if (ChestSave == null)
        {
            return;
        }
        for (int i = 0; i < ChestSave.transform.childCount; i++)
        {
            if (SaveChestCells[i].id == 0)
            {
                ChestSave.transform.GetChild(i).GetComponent<Image>().sprite = backgroundBagCell;
                ChestSave.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                ChestSave.transform.GetChild(i).GetComponent<Image>().sprite = SaveChestCells[i].ItemData.SpriteItem;
                if (SaveChestCells[i].ItemData._isStakable)
                {
                    ChestSave.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    ChestSave.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = SaveChestCells[i].countItem.ToString();

                }
                else
                {
                    ChestSave.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
    public void ShowMerchantCells()
    {
        if (MerchanObj == null)
        {
            return;
        }
        for (int i = 0; i < MerchanObj.transform.childCount; i++)
        {
            if (MerchanCells[i].id == 0)
            {
                MerchanObj.transform.GetChild(i).GetComponent<Image>().sprite = backgroundBagCell;
                MerchanObj.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                MerchanObj.transform.GetChild(i).GetComponent<Image>().sprite = MerchanCells[i].ItemData.SpriteItem;
                if (MerchanCells[i].ItemData._isStakable)
                {
                    MerchanObj.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    MerchanObj.transform.GetChild(i).GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = MerchanCells[i].countItem.ToString();

                }
                else
                {
                    MerchanObj.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
    public void ShowBeltBagCells()
    {
        int indexCell = 0;
        int indexAB = 0;

        for (int j = 0; j < BeltBagCells.Count; j++)
        {
            indexCell = j > 2 ? j - 3 : j;
            indexAB = j > 2 ? 3 : 2;


            if (BeltBagCells[j].id == 0)
            {
                if (indexAB < 3)
                {
                    BeltBag.transform.GetChild(indexAB).GetChild(indexCell).GetComponent<Image>().sprite = backgroundBeltBagCellAttack;

                }
                else
                {
                    BeltBag.transform.GetChild(indexAB).GetChild(indexCell).GetComponent<Image>().sprite = backgroundBeltBagCellBlock;
                }
            }
            else
            {
                BeltBag.transform.GetChild(indexAB).GetChild(indexCell).GetComponent<Image>().sprite = BeltBagCells[j].ItemData.SpriteItem;
            }
        }



    }
    public void ShowBodyBagCells()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 6; i++)
        {
            list.Add(BodyBag.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < list.Count; i++)
        {
            TypeWearables typeWear = list[i].transform.GetChild(0).GetComponent<BodyDragCell>().typeWearables;
            Cell cell = BodyCells.FirstOrDefault(cell => cell.id != 0 && cell.ItemData is ItemWearables wear && wear.typeWearables == typeWear);
            Sprite sprite = cell == null ? BodybackgraundSprits[i] : cell.ItemData.SpriteItem;


            BodyBag.transform.GetChild(i).GetComponent<Image>().sprite = sprite;

        }
    }
    public void ShowCoins()
    {
        textCoin.text = StaticInventory.Coins.ToString();
    }
    public static void AddBonusesToPlayer()
    {
        int BonusArmor = 0;
        int BonusDamage = 0;
        int BonusHP = 0;

        for (int i = 0; i < StaticInventory.BodyCells.Count; i++)
        {
            if (StaticInventory.BodyCells[i].id != 0 && StaticInventory.BodyCells[i].ItemData is ItemWearables)
            {
                ItemWearables itemWearabl = StaticInventory.BodyCells[i].ItemData is ItemWearables ?
                    StaticInventory.BodyCells[i].ItemData as ItemWearables : null;


                switch (itemWearabl._typeBonusWearables)
                {
                    case TypeBonus.Armor:
                        BonusArmor += itemWearabl._valueBonusWearables;
                        break;
                    case TypeBonus.Hp:
                        BonusHP += itemWearabl._valueBonusWearables;
                        break;
                    case TypeBonus.Damage:
                        BonusDamage += itemWearabl._valueBonusWearables;
                        break;
                    default:
                        Debug.LogError("не предусмотрен такой тип бонуса  ");
                        break;
                }
            }
        }
        Debug.Log("Hp Bonus" + BonusHP);
        Debug.Log("Armor Bonus" + BonusArmor);
        Debug.Log("Damage Bonus" + BonusDamage);
        StaticCharacterPlayer.Player.AddBonusMaxArmor(BonusArmor);
        StaticCharacterPlayer.Player.AddBonusMaxDamage(BonusDamage);
        StaticCharacterPlayer.Player.AddBonusMaxHp(BonusHP);

    }

    public void OpenDescriptionItem(Cell cell)
    {
        DescriptionGameObject.SetActive(true);
        Image image = DescriptionGameObject.transform.GetChild(0).GetComponent<Image>();
        TextMeshProUGUI textMeshName = DescriptionGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMeshDescription = DescriptionGameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMeshPrice = DescriptionGameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        image.sprite = cell.ItemData.SpriteItem;
        textMeshName.text = cell.ItemData.nameItem;
        textMeshDescription.text = cell.ItemData._description;
        textMeshPrice.text = cell.ItemData.Price.ToString();

    }
    public void CloseDescriptionItem()
    {
        DescriptionGameObject.SetActive(false);
    }
}
