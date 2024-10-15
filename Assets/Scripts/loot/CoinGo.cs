
using UnityEngine;

public class CoinGo : MonoBehaviour
{
    private Transform player;  // ������ �� ������ ������
    public float followSpeed = 5f; // �������� �������� ������ � ������
    public float pickupDistance = 0.1f; // ���������, ��� ������� ������ ����� "�������"
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

            // �������� ��������� �� ������
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // ���� ������ ���������� ������ � ������, "�������" �
            if (distanceToPlayer <= pickupDistance)
            {
                CollectCoin();
                return;
            }

            // ������� ������ � ������
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }

    void CollectCoin()
    {
        // ����� ����� �������� ��� ��� ���������� �����, ����� ��� ������ �������� ��� ����� ������
        Debug.Log("Coin collected!");
        StaticInventory.AddCoin(1);

        // �������� ������ ����� �����
        Destroy(gameObject);
    }
}
