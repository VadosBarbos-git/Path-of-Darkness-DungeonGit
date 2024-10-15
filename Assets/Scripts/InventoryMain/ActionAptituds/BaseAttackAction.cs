
using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttackAction", menuName = "ActionAptituds/BaseAttackAction")]
public class BaseAttackAction : ActionAptituds
{


    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }

    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Target.TakeDamage(User.CurentDamage);
    }
}

