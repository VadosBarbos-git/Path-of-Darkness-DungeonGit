
using UnityEngine;

public enum TypeActionAptitud
{
    Attack,
    Block
}
public class ActionAptituds : ScriptableObject
{ 
    //������ NextStep
    public virtual void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }

    //��������� �� Target(���� Attack) ��������� �� ���� User(���� ���)
    public virtual void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {

    }
    //������������� ���� ���������� 

}
