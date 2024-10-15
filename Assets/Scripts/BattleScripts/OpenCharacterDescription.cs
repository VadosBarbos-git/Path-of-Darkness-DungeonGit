
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenCharacterDescription : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject PanelDexcription;
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI Armor;
    public bool thisPlayer;
    public void OnPointerDown(PointerEventData eventData)
    {
        CharactersDescription Person = thisPlayer ? StartBattleScene.Player : StartBattleScene.Enemy;
        PanelDexcription.SetActive(true);
        Name.text = Person.Name;
        Health.text = $"{Person.CurentHealth}/{Person.MaxHp}";
        Damage.text = Person.CurentDamage.ToString();
        Armor.text = Person.CurentArmor.ToString();
        if (!thisPlayer)
        {
            Icon.sprite = StartBattleScene.spriteEnemy;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PanelDexcription.SetActive(false);
    }

}
