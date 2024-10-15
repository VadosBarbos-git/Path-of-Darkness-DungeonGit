 
using UnityEngine;

[CreateAssetMenu(fileName = "Counterstrike", menuName = "ActionAptituds/Counterstrike")]
public class Counterstrike : ActionAptituds, IAttackBlock
{
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        base.AwakeActionNextStep(User, Target);
    }
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Target.TakeDamage(User.CurentDamage);
    }


}
