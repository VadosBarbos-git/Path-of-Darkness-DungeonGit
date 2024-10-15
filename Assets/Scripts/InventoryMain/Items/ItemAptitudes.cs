
using UnityEngine;
[CreateAssetMenu(fileName = "ItemObjAptitude", menuName = "ScriptableItems/ItemAptitudes")]
public class ItemAptitudes : Item
{
    [SerializeField] private TypeAptitudes _typeAptitude;
    public TypeAptitudes typeAptitude { get { return _typeAptitude; } }

    public ActionAptituds Action;

    private void OnEnable()
    {
        isAptitudes = true;
        isStakable = true;
    }
    public void AwakeActionNextStep(CharactersDescription User, CharactersDescription Target)
    {
        Action?.AwakeActionNextStep(User, Target);
    }

    public void StartMainActionHitInPoint(CharactersDescription User, CharactersDescription Target)
    {
        Action?.StartMainActionHitInPoint(User, Target);
    }



}
