using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float Speed;
    public Joystick joystick;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform Sprite; 
    Vector2 targetPos;
    // Update is called once per frame
    private void Update()
    {
        targetPos.x = joystick.Horizontal;
        targetPos.y = joystick.Vertical;
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + targetPos * Speed * Time.fixedDeltaTime);
        AnimationRun();
    }
    private void AnimationRun()
    {
        ChangeTurnSprit();
        float value = Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y);
        animator.SetFloat("Run", value);

    }
    private void ChangeTurnSprit()
    {
        if (Mathf.Abs(targetPos.x) > 0)
            Sprite.localScale = targetPos.x < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

}
