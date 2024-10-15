
 
using UnityEngine;
[CreateAssetMenu(fileName = "PiercingStrike", menuName = "ActionAptituds/PiercingStrike")]
public class PiercingStrike : ActionAptituds, IPiercingStrike
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
