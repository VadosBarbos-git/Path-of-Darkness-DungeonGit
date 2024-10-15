
using UnityEngine;

public class OpenMiniMap : MonoBehaviour
{
    public Camera CameraMiniMap;
    public Camera PlayerCamera;

    public void OpenMap()
    {
        if (PlayerCamera.enabled)
        { 
            Time.timeScale = 0f;
            PlayerCamera.enabled = false;
            CameraMiniMap.gameObject.SetActive(true);
        }
        else
        {
            CloseMap();
        }
    }
    public void CloseMap()
    {
        PlayerCamera.enabled = true;
        CameraMiniMap.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
