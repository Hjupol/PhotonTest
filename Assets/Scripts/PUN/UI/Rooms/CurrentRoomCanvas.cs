using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomCanvas : MonoBehaviour
{
    private RoomCanvases roomCanvases;

    public Text roomName;

    [SerializeField]
    private PlayerListingsMenu _playerListingsMenu;
    [SerializeField]
    private LeaveRoomMenu _leaveRoomMenu;
    public LeaveRoomMenu leaveRoomMenu {  get { return _leaveRoomMenu; } }

    public void FirsInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
        _leaveRoomMenu.FirstInitialize(canvases);
        _playerListingsMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        roomName.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
