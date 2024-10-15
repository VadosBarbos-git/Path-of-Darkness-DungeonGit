using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    // public Vector2Int ValueItemsInChest;
    public Vector2Int ValueAptitudsInChest;
    public Vector2Int ValueWearablesInChest;
    public Vector2Int ValueCoinsIntChest;

    private ChestItemsAndComponents _chestCompon;
    private int _layerPlayer = 3;
    private List<Item> _itemsInChest = new();
    private List<GameObject> _lootInMap = new();

    private GameObject _itemPrefab;
    private GameObject _coinPrefab;
    private Transform _parentDroppedItemsTransform;

    private int _valueItem = 0;
    private int _valueCoins = 0;
    private bool isOpen = false;
    public SpriteRenderer Chest;
    private Sprite OpenChest;
    void Start()
    {
        _parentDroppedItemsTransform = GameObject.FindGameObjectWithTag("ParentDroppedItems").transform;

        _chestCompon = GameObject.FindGameObjectWithTag("AllItems").GetComponent<ChestItemsAndComponents>();
        OpenChest = _chestCompon.openChest;
        _itemPrefab = _chestCompon.PrefabItem;
        _coinPrefab = _chestCompon.PrefabCoin;
        if (ValueCoinsIntChest == new Vector2Int(0, 0))
        {
            ValueCoinsIntChest = new Vector2Int(5, 15);
            // ValueItemsInChest = new Vector2Int(1, 4);
        }
        ChoysSameItems();
        _valueCoins = Random.Range(ValueCoinsIntChest.x, ValueCoinsIntChest.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen)
        {
            if (collision.gameObject.layer == _layerPlayer)
            {
                DropItems();
                DropCoins();
                isOpen = true;
                Chest.sprite = OpenChest;
            }
        }
    }

    private void DropCoins()
    {
        for (int i = 0; i < _valueCoins; i++)
        {
            Instantiate(_coinPrefab, transform.position, Quaternion.identity, _parentDroppedItemsTransform);
        }
    }

    private void DropItems()
    {
        for (int i = 0; i < _valueItem; i++)
        {
            _lootInMap.Add(Instantiate(_itemPrefab, transform.position, Quaternion.identity, _parentDroppedItemsTransform));
        }

        for (int i = 0; i < _lootInMap.Count; i++)
        {

            _lootInMap[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemsInChest[i].SpriteItem;
            _lootInMap[i].transform.GetChild(0).GetComponent<LootOnTriger>().ItemData = _itemsInChest[i];
            if (_itemsInChest[i]._isAptitudes)
            {
                _lootInMap[i].transform.GetChild(0).transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }
        }
    }
    private void ChoysSameItems()
    {
        int valueAptituds = Random.Range(ValueAptitudsInChest.x, ValueAptitudsInChest.y);
        int valueWearabls = Random.Range(ValueWearablesInChest.x, ValueWearablesInChest.y);
        _valueItem = valueAptituds + valueWearabls;

        for (int i = 0; i < valueAptituds; i++)
        { 
            _itemsInChest.Add(_chestCompon.ItemAptitude[Random.Range(0, _chestCompon.ItemAptitude.Count)]);
        }
        for (int i = 0; i < valueWearabls; i++)
        {
            _itemsInChest.Add(_chestCompon.Items[Random.Range(0, _chestCompon.Items.Count)]); 
        }
    }

}
