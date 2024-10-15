using System.Collections;

using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSacrificeForArmor", menuName = "ActionAptituds/ShieldSacrificeForArmor")]
public class ShieldSacrificeForArmor : ActionAptituds, IArmorBuff, IDontBlock
{

    [SerializeField] private int duration = 1;
    [SerializeField] private int valueBuff = 4;
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        User.AddBuff(this);
    }
    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {

    }
    public ShieldSacrificeForArmor(int duration, int valueBuff)
    {
        this.duration = duration;
        this.valueBuff = valueBuff;
    }
    public IBuff Clone()
    {
        return new ShieldSacrificeForArmor(duration, valueBuff);
    }
    public void Apply(CharactersDescription User)
    {
        
    }

    public void Remove(CharactersDescription User)
    {
       
    }
    public int GetArmorBuff()
    {
        return valueBuff;
    }
    public int GetDuretion()
    {
        return duration;
    }
}
