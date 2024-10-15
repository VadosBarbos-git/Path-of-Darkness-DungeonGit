
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dodge", menuName = "ActionAptituds/Dodge")]

public class Dodge : ActionAptituds , ICustomBlock
{
    public int GetPosBlock(List<int> AttacksOponent)
    {
        return AttacksOponent[0];
    }

    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        base.AwakeActionNextStep(User, Target);
    } 
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        base.StartMainActionHitInPoint(User, Target);
    }
}
