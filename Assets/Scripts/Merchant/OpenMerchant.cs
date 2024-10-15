using UnityEngine;

public class OpenMerchant : MonoBehaviour
{
    public GameObject CastomButton;
    private int PlayerLayer = 3;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PlayerLayer)
            CastomButton.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PlayerLayer)
            CastomButton.SetActive(false);
    }
   


}
