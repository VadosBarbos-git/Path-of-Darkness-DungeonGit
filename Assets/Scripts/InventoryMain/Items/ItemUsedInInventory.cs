
using UnityEngine;

[CreateAssetMenu(fileName = "ItemUsedObj", menuName = "ScriptableItems/ItemUsedInventory")]

public class ItemUsedInInventory : Item
{
    [SerializeField] private ActionUseblItems ActionUsed;
    public void Use(CharactersDescription User)
    {
        ActionUsed?.Use(User);
    }
}
