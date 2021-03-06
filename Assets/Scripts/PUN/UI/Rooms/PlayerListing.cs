﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Text _text;

    public Player Player { get; private set; }

    public bool Ready=false;


    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText(player);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if(targetPlayer!=null && targetPlayer == Player)
        {
            if (changedProps.ContainsKey("RandomNumber"))
                SetPlayerText(targetPlayer);
            if (changedProps.ContainsKey("Ready"))
                SetPlayerText(targetPlayer);

        }
    }

    public void SetPlayerText(Player player)
    {
        int result = -1;
        if (player.CustomProperties.ContainsKey("RandomNumber"))
            result = (int)player.CustomProperties["RandomNumber"];

        if (PhotonNetwork.IsMasterClient)
        {
            if (player.IsMasterClient)
            {
                _text.text = "Host, " + player.NickName;
            }
            else
            {
                if (Ready)
                {
                    _text.text = "Ready, " + player.NickName;
                }
                else
                {
                    _text.text = "Not ready, " + player.NickName;
                }
            }
        }
        else
        {
            if (player.IsMasterClient)
            {
                _text.text = "Host, " + player.NickName;
            }
            else
            {
                if (Ready)
                {
                    _text.text = player.NickName;
                }
                else
                {
                    _text.text = player.NickName;
                }
            }
        }
        
        
    }
}
