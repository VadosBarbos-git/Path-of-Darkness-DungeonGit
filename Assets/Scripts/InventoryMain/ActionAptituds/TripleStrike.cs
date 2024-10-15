 
using UnityEngine;

[CreateAssetMenu(fileName = "TripleStrike", menuName = "ActionAptituds/TripleStrike")]

public class TripleStrike : ActionAptituds, IAttackSameCell
{
    private int[] points = new int[3] { 0, 1, 2 };
    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        base.AwakeActionNextStep(User, Target);
    }
    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        int damage = Mathf.RoundToInt(User.CurentDamage * 0.6f);
        Target.TakeDamage(damage);
    }
    public int[] GetNombersCell(int PosAptituds)
    {
        return points;
    }
}

