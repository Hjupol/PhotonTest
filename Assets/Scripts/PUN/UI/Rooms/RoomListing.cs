using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    public Text _text;
    

    public RoomInfo RoomInfo { get; private set; }
    
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name; 
    }

    public void OnClick_Button()
    {
        Text nickInput = GameObject.Find("NickNameInputText").GetComponent<Text>(); ;
        if (!string.IsNullOrWhiteSpace(nickInput.text))
        {
            PhotonNetwork.NickName = nickInput.text;
        }
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
