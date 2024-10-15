using System.Collections; 
using UnityEngine;

public class AnimationDrop : MonoBehaviour
{

    public float dropDuration = 0.5f; // ������������ ���������

    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 controlPoint;
    private float timeElapsed;

    void Start()
    {
        // ���������� ��������� �����
        startPoint = transform.position;

        // ��������� ��������� �������� ����� �� ���������� ������� 1
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float radius = 1f;
        endPoint = new Vector2(
            startPoint.x + Mathf.Cos(angle) * radius,
            startPoint.y + Mathf.Sin(angle) * radius
        );

        // ���������� ����������� ����� ���� �������
        controlPoint = startPoint + Vector2.up * 2f;

        // ��������� ��������� ��������
        StartCoroutine(DropItem());
    }

    IEnumerator DropItem()
    {
        // ������� �������


        // �������� �� ������ �����
        while (timeElapsed < dropDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / dropDuration;

            // ������ �����
            Vector2 m1 = Vector2.Lerp(startPoint, controlPoint, t);
            Vector2 m2 = Vector2.Lerp(controlPoint, endPoint, t);
            transform.position = Vector2.Lerp(m1, m2, t);

            yield return null;
        }

        // ����������� ������� �����������
        transform.position = endPoint;
    }

}
