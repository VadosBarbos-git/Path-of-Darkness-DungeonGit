using System.Collections; 
using UnityEngine;

public class AnimationDrop : MonoBehaviour
{

    public float dropDuration = 0.5f; // Длительность выпадения

    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 controlPoint;
    private float timeElapsed;

    void Start()
    {
        // Определяем начальную точку
        startPoint = transform.position;

        // Генерация случайной конечной точки на окружности радиуса 1
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float radius = 1f;
        endPoint = new Vector2(
            startPoint.x + Mathf.Cos(angle) * radius,
            startPoint.y + Mathf.Sin(angle) * radius
        );

        // Определяем контрольную точку выше сундука
        controlPoint = startPoint + Vector2.up * 2f;

        // Запускаем выпадение предмета
        StartCoroutine(DropItem());
    }

    IEnumerator DropItem()
    {
        // Создаем предмет


        // Анимация по кривой Безье
        while (timeElapsed < dropDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / dropDuration;

            // Кривая Безье
            Vector2 m1 = Vector2.Lerp(startPoint, controlPoint, t);
            Vector2 m2 = Vector2.Lerp(controlPoint, endPoint, t);
            transform.position = Vector2.Lerp(m1, m2, t);

            yield return null;
        }

        // Обеспечение точного приземления
        transform.position = endPoint;
    }

}
