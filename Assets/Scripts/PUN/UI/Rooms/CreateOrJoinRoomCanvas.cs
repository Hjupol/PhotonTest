using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu createRoomMenu;
    [SerializeField]
    private RoomListingsMenu roomListingsMenu;

    private RoomCanvases roomCanvases;

    public void FirsInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
        createRoomMenu.FirsInitialize(canvases);
        roomListingsMenu.FirstInitialize(canvases);
    }
}
