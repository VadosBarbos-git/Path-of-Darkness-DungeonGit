
using UnityEngine;

[CreateAssetMenu(fileName = "BaseBlockAction", menuName = "ActionAptituds/BaseBlockAction")]
public class BaseBlockAction : ActionAptituds
{
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }

    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {

    }

}
