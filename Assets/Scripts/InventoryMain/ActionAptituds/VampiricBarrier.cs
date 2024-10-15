 
using UnityEngine;

[CreateAssetMenu(fileName = "VampiricBarrier", menuName = "ActionAptituds/VampiricBarrier")]

public class VampiricBarrier : ActionAptituds, IBuff
{
    [SerializeField] private int duration = 1;
    [SerializeField] private int valueBuff = 5;

    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }



    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Target.TakeDamage(valueBuff);
        User.AddBuff(this);
    }
    public VampiricBarrier(int duration,int valueBuff)
    {
        this.duration = duration;
        this.valueBuff = valueBuff;
    }
    public IBuff Clone()
    {
        return new VampiricBarrier(duration,valueBuff);
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
