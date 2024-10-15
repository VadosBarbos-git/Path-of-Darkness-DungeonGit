

using UnityEngine;
[CreateAssetMenu(fileName = "SunderArmor", menuName = "ActionAptituds/SunderArmor")]
public class SunderArmor : ActionAptituds, IArmorDeBuff
{
    public int duration = 1;//будет действовать текущий удар и следующий 
    public int reduceArmor = 3;


    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        base.AwakeActionNextStep(User, Target);
    }
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Target.AddBuff(this);
        Target.TakeDamage(User.CurentDamage);
    }
    public SunderArmor(int duration, int reduceArmor)
    {
        this.duration = duration;
        this.reduceArmor = reduceArmor;
    }
    public IBuff Clone()
    {
        return new SunderArmor(this.duration, this.reduceArmor);
    }
    public int GetDuretion()
    {
        return duration;
    }
    public void Apply(CharactersDescription User)
    {

    }
    public void Remove(CharactersDescription User)
    {

    }
    public int GetRedusceArmor()
    {
        return reduceArmor;
    }
}
