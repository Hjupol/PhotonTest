using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text roomName;
    [SerializeField]
    private Text nickInput;

    private RoomCanvases roomCanvases;

    public void FirsInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (!string.IsNullOrWhiteSpace(nickInput.text))
        {
            PhotonNetwork.NickName = nickInput.text;
        }
        RoomOptions options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        options.MaxPlayers = 8;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);

    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.", this);
        roomCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: "+ message, this);
    }
}
