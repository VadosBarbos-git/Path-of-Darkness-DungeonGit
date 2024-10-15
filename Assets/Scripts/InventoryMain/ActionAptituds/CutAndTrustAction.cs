
using UnityEngine;
[CreateAssetMenu(fileName = "CutAndTrustAction", menuName = "ActionAptituds/CutAndTrustAction")]
public class CutAndTrustAction : ActionAptituds
{
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }

    //��������� �� Target(���� Attack) ��������� �� ���� User(���� ���)
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        int damage = (int)(User.CurentDamage * 1.3f);
        Target.TakeDamage(damage);

    }
}
