
using UnityEngine;

[CreateAssetMenu(fileName = "LittleHealUsebl", menuName = "ActionUsable/LittleHeal")]

public class LittleHeal : ActionUseblItems
{
    public int ValueHeal = 5;
    public override void Use(CharactersDescription User)
    {
        User.Heal(ValueHeal);
    }
}
