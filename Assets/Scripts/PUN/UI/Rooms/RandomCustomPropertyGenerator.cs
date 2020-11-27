using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage _image;

    private bool _ready = false;

    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
   

    private void SetCustomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        Vector3 newColor= new Vector3(r, g, b);
        _image.color = new Color(newColor.x,newColor.y,newColor.z);
        

        _myCustomProperties["RandomVector3"] = newColor;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }

    private void SetReadyUp()
    {
        if (_ready)
        {
            _ready = false;
        }
        else
        {
            _ready = true;
        }
        _myCustomProperties["Ready"] = _ready;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
    }

    public void OnClickReady_Button()
    {
        SetReadyUp();
    }

    public void OnClickColor_Button()
    {
        SetCustomColor(); 
    }
}
