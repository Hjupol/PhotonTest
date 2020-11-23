using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    private RoomCanvases roomCanvases;

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
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
