
using UnityEngine;
[CreateAssetMenu(fileName = "ItemObjWearables", menuName = "ScriptableItems/ItemWearables")]
public class ItemWearables : Item
{
    [SerializeField] private TypeWearables _typeWearables;
    public TypeWearables typeWearables { get { return _typeWearables; } }
    [SerializeField] protected TypeBonus typeBonusWearables;
    public TypeBonus _typeBonusWearables { get { return typeBonusWearables; } }

    [SerializeField] protected int ValueBonusWearables;
    public int _valueBonusWearables { get { return ValueBonusWearables; } }
    private void OnEnable()
    {
        isAptitudes = false;
        isStakable = false;
    }
}
