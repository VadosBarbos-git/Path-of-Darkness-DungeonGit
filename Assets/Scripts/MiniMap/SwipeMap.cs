
using System.Collections;
using UnityEngine;

public class SwipeMap : MonoBehaviour
{
    public float maxDistance = 50f;
    public float maxDistanceY = 30f;
    public float swipeSpeed = 0.1f;
    public float initialMoveY = 10f;// Скорость перемещения камеры при свайпе
    private float moveSpeed = 8f;
    public Transform PlayerTransform;

    private Vector2 touchStart;       // Стартовая позиция касания
    private Vector3 initialCameraPosition;

    private void OnEnable()
    {
        transform.position = PlayerTransform.position;
        Vector3 target = new Vector3(PlayerTransform.position.x + 10, PlayerTransform.position.y - initialMoveY, 0);
        StartCoroutine(startMap(target));
    }
    IEnumerator startMap(Vector3 tergetPos)
    {
        while (transform.position.y > tergetPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, tergetPos, moveSpeed * Time.unscaledDeltaTime);
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) break;
            yield return null;
        }
    }
    private void OnDisable()
    {
        transform.position = PlayerTransform.position;
    }

    void Update()
    {
        HandleTouchInput();
    }
    void HandleTouchInput()
    {
        // Проверка, если было касание на экране
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Сохраняем стартовую точку касания
                    touchStart = touch.position;
                    initialCameraPosition = gameObject.transform.position;
                    break;

                case TouchPhase.Moved:
                    // Вычисляем смещение пальца
                    Vector2 touchDelta = touch.position - touchStart;

                    // Преобразуем смещение в мировые координаты
                    Vector3 movement = new Vector3(-touchDelta.x * swipeSpeed, -touchDelta.y * swipeSpeed, 0);

                    // Перемещаем камеру на основе смещения
                    Vector3 newPosition = initialCameraPosition + movement;

                    // Ограничиваем позицию камеры по радиусу от персонажа
                    newPosition = LimitCameraPosition(newPosition);

                    // Применяем новую позицию к камере
                    gameObject.transform.position = newPosition;
                    break;
            }
        }
    }
    Vector3 LimitCameraPosition(Vector3 newPosition)
    {
        Vector3 playerPosition = PlayerTransform.position;

        // Рассчитываем смещение от игрока
        Vector3 offset = newPosition - playerPosition;

        // Ограничиваем движение камеры по X и Z (если это 3D или только X для 2D)
        float distanceX = Mathf.Clamp(offset.x, -maxDistance, maxDistance);
        float distanceY = Mathf.Clamp(offset.y, -maxDistanceY, maxDistanceY);  // Ограничиваем по Y
        float distanceZ = Mathf.Clamp(offset.z, -maxDistance, maxDistance);    // Ограничение по Z (если используется в 3D)

        // Возвращаем новую позицию камеры с учётом ограничений
        return new Vector3(playerPosition.x + distanceX, playerPosition.y + distanceY, playerPosition.z + distanceZ);
    }
}
