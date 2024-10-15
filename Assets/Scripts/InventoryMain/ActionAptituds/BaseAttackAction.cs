
using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttackAction", menuName = "ActionAptituds/BaseAttackAction")]
public class BaseAttackAction : ActionAptituds
{


    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }

    //��������� �� Target(���� Attack) ��������� �� ���� User(���� ���)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Target.TakeDamage(User.CurentDamage);
    }
}

