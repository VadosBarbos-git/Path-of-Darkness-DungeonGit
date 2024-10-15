using UnityEngine;
[CreateAssetMenu(fileName = "ShieldAndHeal", menuName = "ActionAptituds/ShieldAndHeal")]

public class ShieldAndHeal : ActionAptituds, IBuff
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
    public ShieldAndHeal(int duration, int valueBuff)
    {
        this.duration = duration;
        this.valueBuff = valueBuff;
    }
    public IBuff Clone()
    {
        return new ShieldAndHeal(duration, valueBuff);
    }
    public void Apply(CharactersDescription User)
    {
        User.Heal(valueBuff);
    }

    public void Remove(CharactersDescription User)
    {

    }
    public int GetDuretion()
    {
        return duration;
    }
}
