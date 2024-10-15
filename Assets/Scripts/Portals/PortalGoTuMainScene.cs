using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalGoTuMainScene : MonoBehaviour
{
    public int ValueHard = 1;
    public static bool CanIPortal = true;


    private int _playerLayer = 3;
    void OnTriggerEnter2D(Collider2D triger)
    {

        if (triger.transform.gameObject.layer == _playerLayer && CanIPortal)
        {
            CanIPortal = false;
            MapRendering.seed = Random.Range(1, 9999);
            //анимацыя телепорта 
            SceneManager.LoadScene("SampleScene");
            //передать сложность создаваемой карты 
            CanIPortal = true;
        }

    }


}
