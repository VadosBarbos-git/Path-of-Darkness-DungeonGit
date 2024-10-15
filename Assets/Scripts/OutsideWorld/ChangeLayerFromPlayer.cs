using UnityEngine;

public class ChangeLayerFromPlayer : MonoBehaviour
{
    Transform Player;
    SpriteRenderer Renderer;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Renderer = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.position.y > transform.position.y)
        {
            Renderer.sortingOrder = 10;
        }
        else
        {
            Renderer.sortingOrder = 1;
        }
    }
}
