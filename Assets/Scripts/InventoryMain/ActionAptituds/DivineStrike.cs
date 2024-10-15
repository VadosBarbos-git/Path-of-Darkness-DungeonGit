 
using UnityEngine;
[CreateAssetMenu(fileName = "DivineStrike", menuName = "ActionAptituds/DivineStrike")]
public class DivineStrike : ActionAptituds
{
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }

    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        int damage = (int)(User.CurentDamage * 2f);
        Target.TakeDamage(damage);

    }
}
