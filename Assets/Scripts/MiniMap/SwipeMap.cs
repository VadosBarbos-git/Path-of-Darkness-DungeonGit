
using System.Collections;
using UnityEngine;

public class SwipeMap : MonoBehaviour
{
    public float maxDistance = 50f;
    public float maxDistanceY = 30f;
    public float swipeSpeed = 0.1f;
    public float initialMoveY = 10f;// �������� ����������� ������ ��� ������
    private float moveSpeed = 8f;
    public Transform PlayerTransform;

    private Vector2 touchStart;       // ��������� ������� �������
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
        // ��������, ���� ���� ������� �� ������
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // ��������� ��������� ����� �������
                    touchStart = touch.position;
                    initialCameraPosition = gameObject.transform.position;
                    break;

                case TouchPhase.Moved:
                    // ��������� �������� ������
                    Vector2 touchDelta = touch.position - touchStart;

                    // ����������� �������� � ������� ����������
                    Vector3 movement = new Vector3(-touchDelta.x * swipeSpeed, -touchDelta.y * swipeSpeed, 0);

                    // ���������� ������ �� ������ ��������
                    Vector3 newPosition = initialCameraPosition + movement;

                    // ������������ ������� ������ �� ������� �� ���������
                    newPosition = LimitCameraPosition(newPosition);

                    // ��������� ����� ������� � ������
                    gameObject.transform.position = newPosition;
                    break;
            }
        }
    }
    Vector3 LimitCameraPosition(Vector3 newPosition)
    {
        Vector3 playerPosition = PlayerTransform.position;

        // ������������ �������� �� ������
        Vector3 offset = newPosition - playerPosition;

        // ������������ �������� ������ �� X � Z (���� ��� 3D ��� ������ X ��� 2D)
        float distanceX = Mathf.Clamp(offset.x, -maxDistance, maxDistance);
        float distanceY = Mathf.Clamp(offset.y, -maxDistanceY, maxDistanceY);  // ������������ �� Y
        float distanceZ = Mathf.Clamp(offset.z, -maxDistance, maxDistance);    // ����������� �� Z (���� ������������ � 3D)

        // ���������� ����� ������� ������ � ������ �����������
        return new Vector3(playerPosition.x + distanceX, playerPosition.y + distanceY, playerPosition.z + distanceZ);
    }
}
