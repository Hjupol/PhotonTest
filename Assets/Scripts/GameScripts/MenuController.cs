using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject roomCanvas;
    public GameObject creditsCanvas;
    public GameObject menuCanvas;

    public void OnClick_RoomButton()
    {
        menuCanvas.SetActive(false);
        roomCanvas.SetActive(true);
    }

    public void OnClick_CreditsButton()
    {
        menuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void OnClick_BackButton()
    {
        if (creditsCanvas.activeInHierarchy)
        {
            creditsCanvas.SetActive(false);
        }
        else
        {
            roomCanvas.SetActive(false);
        }
        menuCanvas.SetActive(true);
    }

    public void OnClick_ExitButton()
    {
        Application.Quit();
    }


}
