
using UnityEngine;
[CreateAssetMenu(fileName = "ParryingAction", menuName = "ActionAptituds/ParryingAction")]
public class ParryingAction : ActionAptituds
{

    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }
    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        int damage = (int)(User.CurentDamage * 0.3f);
        Target.TakeDamage(damage);
    }
}
