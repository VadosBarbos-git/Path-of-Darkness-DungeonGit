
using UnityEngine;

[CreateAssetMenu(fileName = "Berserk", menuName = "ActionAptituds/Berserk")]
public class Berserk : ActionAptituds, IDamageBuff, IDontBlock
{

    [SerializeField] private int duration = 1;
    [SerializeField] private float valueBuffDamage = 1;
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        User.AddBuff(this);
    }
    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {

    }
    public Berserk(int duration, float valueDamage)
    {
        this.duration = duration;
        this.valueBuffDamage = valueDamage;
    }
    public IBuff Clone()
    {
        return new Berserk(this.duration, this.valueBuffDamage);
    }
    public int GetDamageBuff(CharactersDescription User)
    {
        return Mathf.RoundToInt(User.Damage * valueBuffDamage);
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
}
