
using UnityEngine;

public class CoinGo : MonoBehaviour
{
    private Transform player;  // Ссылка на объект игрока
    public float followSpeed = 5f; // Скорость движения монеты к игроку
    public float pickupDistance = 0.1f; // Дистанция, при которой монета будет "собрана"
    public int timer = 80; 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    void FixedUpdate()
    {
        timer--;
        if (timer < 1)
        {

            // Проверка дистанции до игрока
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Если монета достаточно близко к игроку, "собрать" её
            if (distanceToPlayer <= pickupDistance)
            {
                CollectCoin();
                return;
            }

            // Двигаем монету к игроку
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }

    void CollectCoin()
    {
        // Здесь можно добавить код для добавления очков, звука или других эффектов при сборе монеты
        Debug.Log("Coin collected!");
        StaticInventory.AddCoin(1);

        // Удаление монеты после сбора
        Destroy(gameObject);
    }
}
