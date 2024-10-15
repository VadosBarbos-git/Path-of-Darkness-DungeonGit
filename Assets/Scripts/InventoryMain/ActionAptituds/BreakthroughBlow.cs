
using UnityEngine;
[CreateAssetMenu(fileName = "BreakthroughBlow", menuName = "ActionAptituds/BreakthroughBlow")]
public class BreakthroughBlow : ActionAptituds
{
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        base.AwakeActionNextStep(User, Target);
    }
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Target.TakeDamage(User.CurentDamage, true);
    }
} 
