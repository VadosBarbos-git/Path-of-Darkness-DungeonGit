using UnityEngine;

public enum TypeAptitudes
{
    Attack,
    Block,
    Bonus
}


public enum TypeWearables
{
    Head,
    Armor,
    LeftHand,
    RightHand,
    LeftLeg,
    RightLeg,

}
public enum TypeBonus
{
    Damage,
    Armor,
    Hp
}

//[CreateAssetMenu(fileName = "ItemObj", menuName = "ScriptableItems/Item")]
public class Item : ScriptableObject
{
    [SerializeField] protected string NameItem;
    public string nameItem { get { return NameItem; } }

    [SerializeField] protected float Id;
    public float id { get { return Id; } }

    [SerializeField] protected bool isStakable;
    public bool _isStakable { get { return isStakable; } }

    protected bool isAptitudes;
    public bool _isAptitudes { get { return isAptitudes; } }
    public Sprite SpriteItem;
    [Multiline(6)]
    [SerializeField] protected string description;
    public string _description { get { return description; } }

    [SerializeField] protected int _prise;
    public int  Price { get { return _prise; } }
}
