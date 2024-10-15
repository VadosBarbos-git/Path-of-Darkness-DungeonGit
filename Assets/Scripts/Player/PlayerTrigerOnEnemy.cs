using UnityEngine;

public class PlayerTrigerOnEnemy : MonoBehaviour
{
    public StartBattleScene BattleScene;
    public CharactersDescription PlayerDescription;
    private CharactersDescription EnemyDescription;
    private LayerMask _enemy = 6;
    public bool CanITriger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (CanITriger && collision.gameObject.layer == _enemy)
        {
            EnemyDescription = collision.gameObject.GetComponent<CharactersDescription>();
            BattleScene.StartScene(PlayerDescription, EnemyDescription, collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _enemy)
        {

        }
    }
}
