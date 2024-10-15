
using UnityEngine;

[CreateAssetMenu(fileName = "VampiricStrike", menuName = "ActionAptituds/VampiricStrike")]
public class VampiricStrike : ActionAptituds, IBuff
{
    [SerializeField] private int duration = 1;
    [SerializeField] private int valueBuff = 4;

    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }
    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Target.TakeDamage(User.CurentDamage);
        User.AddBuff(this);
    }
    public VampiricStrike(int duration, int valueBuff)
    {
        this.duration = duration;
        this.valueBuff = valueBuff;
    }
    public IBuff Clone()
    {
        return new VampiricStrike(duration, valueBuff);
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
