 
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToHome : MonoBehaviour
{
    int _playerLayer = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _playerLayer)
        {
            //анимацы€ портала 
            SceneManager.LoadScene("MainCamp");
            //перенос инвентар€ 
        }
    }
}
