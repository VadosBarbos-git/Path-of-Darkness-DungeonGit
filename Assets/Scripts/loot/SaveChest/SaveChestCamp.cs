using UnityEngine;

public class SaveChestCamp : MonoBehaviour
{
    [SerializeField] private GameObject BottoomChestSave;
    [SerializeField] private SpriteRenderer spriteChest;
    [SerializeField] private Sprite CloseChest;
    [SerializeField] private Sprite OpenChest;
    private int LayerPlayer = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerPlayer)
        {
            BottoomChestSave.SetActive(true);
            spriteChest.sprite = OpenChest;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerPlayer)
        {
            BottoomChestSave.SetActive(false);
            spriteChest.sprite = CloseChest;
        }
    }
}
