
using UnityEngine;

public enum TypeActionAptitud
{
    Attack,
    Block
}
public class ActionAptituds : ScriptableObject
{ 
    //Запуск NextStep
    public virtual void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {

    }

    //Попадание по Target(Если Attack) Попадание по щиту User(Если щит)
    public virtual void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {

    }
    //Дополнительно если пригодится 

}
