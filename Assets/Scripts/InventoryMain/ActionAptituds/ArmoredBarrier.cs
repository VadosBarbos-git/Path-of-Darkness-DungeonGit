
using UnityEngine;
[CreateAssetMenu(fileName = "ArmoredBarrier", menuName = "ActionAptituds/ArmoredBarrier")]

public class ArmoredBarrier : ActionAptituds, IArmorBuff
{
    [SerializeField] private int duration = 1;
    [SerializeField] private int valueBuff = 2;
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        User.AddBuff(this);
    }
    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {

    }

    public ArmoredBarrier(int duration, int valueBuff)
    {
        this.duration = duration;
        this.valueBuff = valueBuff;
    }
    public IBuff Clone()
    {
        return new ArmoredBarrier(this.duration, this.valueBuff);
    }
    public void Apply(CharactersDescription User)
    {

    }

    public void Remove(CharactersDescription User)
    {

    }
    public int GetDuretion()
    {
        return duration;
    }

    public int GetArmorBuff()
    {
        return valueBuff;
    }
}
