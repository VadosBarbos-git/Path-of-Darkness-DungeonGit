
using System.Collections.Generic;
using UnityEngine;

public class CharactersDescription : MonoBehaviour
{
    [SerializeField]
    private string _name;
    public string Name { get { return _name; } }
    [SerializeField]
    private float _id;
    public float Id { get { return _id; } }
    [SerializeField]
    private int _health;
    public int Health { get { return _health; } }
    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } }
    [SerializeField]
    private int _armor;
    public int Armor { get { return _armor; } }

    public List<ItemAptitudes> Aptituds;
    public ItemAptitudes BaseAttack;
    public ItemAptitudes BaseBlock;

    public int MaxHp { get; private set; }
    public int CurentHealth;
    public int CurentDamage;
    public int CurentArmor;
    [HideInInspector] public bool takeDamage = false;

    private List<BuffInstance> activeBuffs = new List<BuffInstance>();

    #region Delegats Events
    public delegate void changeCurentArmor(bool biger);
    public event changeCurentArmor ChangeArmorCurent;

    #endregion
    public CharactersDescription(string Name, float Id, int Health, int Damage, int Armor)
    {
        _name = Name;
        _id = Id;
        _health = Health;
        _damage = Damage;
        _armor = Armor;
        CurentArmor = Armor;
        CurentDamage = Damage;
        CurentHealth = Health;
        MaxHp = Health;
    }
    public void AddBuff(IBuff buff)
    {
        IBuff copyBuff = buff.Clone();
        copyBuff.Apply(this);
        activeBuffs.Add(new BuffInstance(copyBuff));
        updateArmor();
        updateDamage();
    }
    void updateArmor()
    {
        int totalArmorModifire = 0;
        foreach (var item in activeBuffs)
        {
            if (item.buff is IArmorDeBuff ArmDe)
            {
                totalArmorModifire -= ArmDe.GetRedusceArmor();
            }
            if (item.buff is IArmorBuff ArmBuff)
            {
                totalArmorModifire += ArmBuff.GetArmorBuff();
            }
        }
        CurentArmor = Mathf.Clamp(Armor + totalArmorModifire, 0, 100);
    }
    void updateDamage()
    {
        int totalDamageModifire = 0;
        foreach (var item in activeBuffs)
        {
            if (item.buff is IDamageBuff DanageBuff)
            {
                totalDamageModifire += DanageBuff.GetDamageBuff(this);
            }
            if (item.buff is IDamageDeBuff DamageDe)
            {
                totalDamageModifire -= DamageDe.GetDamageDeBuff(this);
            }

        }
        CurentDamage = Mathf.Clamp(Damage + totalDamageModifire, 1, 9999);
    }
    public void UpdateBuffs()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            BuffInstance buffInstance = activeBuffs[i];

            buffInstance.UpdateDuration();
            if (buffInstance.IsExpired())
            {
                buffInstance.buff.Remove(this);  // Убираем эффект
                activeBuffs.RemoveAt(i);         // Удаляем из списка
            }
        }
        updateArmor();
        updateDamage();
    }
    public void TakeDamage(int damage, bool armorBypass = false)
    {
        Debug.Log($"{Name} пострадал на = {damage} CurentHP = {CurentHealth} Curent Armot ={CurentArmor} ");
        if (!armorBypass)
        {
            damage -= CurentArmor;
        }

        if (damage > 0)
        {
            takeDamage = true;
            CurentHealth = (CurentHealth - damage) > 0 ? CurentHealth - damage : 0;
        }
        else
        {
            takeDamage = true;
            CurentHealth = (CurentHealth - 1) > 0 ? CurentHealth - 1 : 0;

        }

    }
    public void Heal(int value)
    {
        if (CurentHealth + value >= MaxHp)
        {
            CurentHealth = MaxHp;
        }
        else
        {
            CurentHealth += value;
        }
    }
    public bool ImAlive() => CurentHealth > 0;
    public void AddBonusMaxHp(int AllBonusesHp)
    {
        if (CurentHealth >= MaxHp)
        {
            MaxHp = Health + AllBonusesHp;
            CurentHealth = MaxHp;
        }
        else
        {
            MaxHp = Health + AllBonusesHp;
            //CurentHealth = CurentHealth;
        }
    }
    public void AddBonusMaxDamage(int AllDamage)
    {
        CurentDamage = Damage + AllDamage;
    }
    public void AddBonusMaxArmor(int AllArmors)
    {
        CurentArmor = Armor + AllArmors;
    }



}
