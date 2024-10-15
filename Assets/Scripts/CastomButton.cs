
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CastomButton : MonoBehaviour
{
    // ������� UnityEvent, ����� ����� ���� �������� �������� ����� ���������
    public UnityEvent onButtonPressed;
    private Vector3 startPos;
    private Vector3 endPos;
    private float speed = 1;
    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
    }
    private void OnEnable()
    {
        StartCoroutine(rocking());
    }
    void Update()
    {
        // ���������, ���� �� ������� �� ������

        if (Time.timeScale > 0.001f && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ���������, ���� �� ������� ������
            if (touch.phase == TouchPhase.Began)
            {
                // ����������� ������� ������� � ������� ����������
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPos2D = new Vector2(touchPosition.x, touchPosition.y);

                // ��������� Raycast ��� ����������� ��������� � ������
                RaycastHit2D hit = Physics2D.Raycast(touchPos2D, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // �������� �������, ���� ���� ������� �� ������
                    onButtonPressed.Invoke();
                }
            }
        }
    }
    IEnumerator rocking()
    {
        for (int i = 0; i < 50; i++)  
        { 

            while (transform.position != endPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                yield return null;
            }
            while (transform.position != startPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                yield return null;
            }
            yield return null;
        }
    }

}
