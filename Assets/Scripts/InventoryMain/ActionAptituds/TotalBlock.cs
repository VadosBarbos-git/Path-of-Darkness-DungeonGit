
using UnityEngine;

[CreateAssetMenu(fileName = "TotalBlock", menuName = "ActionAptituds/TotalBlock")]
public class TotalBlock : ActionAptituds, IBlockSameCells
{
    private int[] valueBlock = new int[3] {0,1,2};
    public int[] GetNombersCell(int PosAptituds)
    { 
        return valueBlock;
    }


    public override void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }


    public override void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {

    }
}
