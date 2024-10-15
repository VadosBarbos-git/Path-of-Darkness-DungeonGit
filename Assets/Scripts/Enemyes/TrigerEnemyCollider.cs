
using UnityEngine;

public class TrigerEnemyCollider : MonoBehaviour
{
    public EnemyControler enemyControler;
    public static Transform TrigerCollider;
    public void Start()
    {
        TrigerCollider = gameObject.transform;
    }
    private void OnTriggerEnter2D(Collider2D triger)
    {

        enemyControler.Enter2D(triger, gameObject.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyControler.Exit2D(collision, gameObject.transform);
    }
}
