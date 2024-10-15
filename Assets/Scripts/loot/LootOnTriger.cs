using UnityEngine;

public class LootOnTriger : MonoBehaviour
{
    public Item ItemData;
    public float followSpeed = 15f; // Скорость движения монеты к игроку
    public int timer = 30;

    private int _layerPlayer = 3;
    private float pickupDistance = 0.2f; // Дистанция, при которой монета будет "собрана"
    private Transform player;  // Ссылка на объект игрока

    private bool take = false;
    private bool takeJastUnce = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _layerPlayer)
        {
            if (takeJastUnce)
            {
                TakeItem(); 
            }
        }
    }

    void FixedUpdate()
    {
        timer--;
        if (timer < 0 && take)
        {

            // Проверка дистанции до игрока
            float distanceToPlayer = Vector2.Distance(transform.parent.position, player.position);

            // Если монета достаточно близко к игроку, "собрать" её
            if (distanceToPlayer <= pickupDistance)
            {
                Destroy(transform.parent.gameObject);
                return;
            }

            // Двигаем монету к игроку
            Vector2 direction = (player.position - transform.parent.position).normalized;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, player.position, followSpeed * Time.deltaTime);
        }
    }
    private void TakeItem()
    {
        if (StaticInventory.TryAddSameItem(ItemData))
        {
            take = true;
            takeJastUnce = false; 
        }
        else
        {
            Debug.Log(" Инвентарь Заполнен ");
            take = false;
        }
    }
}
