﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField]
    private CreateOrJoinRoomCanvas createOrJoinRoomCanvas;

    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas { get { return createOrJoinRoomCanvas; } }


    [SerializeField]
    private CurrentRoomCanvas currentRoomCanvas;

    public CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCanvas; } }

    private void Awake()
    {
        FirstInitialize();
        //ScoreManager.scoreManager.p1Score = 0;
        //ScoreManager.scoreManager.p2Score = 0;
    }

    private void FirstInitialize()
    {
        CreateOrJoinRoomCanvas.FirsInitialize(this);
        CurrentRoomCanvas.FirsInitialize(this);
    }
}
