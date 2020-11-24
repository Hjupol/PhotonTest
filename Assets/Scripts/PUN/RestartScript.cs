using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviourPun
{
    void Start()
    {
        //PhotonNetwork.IsMessageQueueRunning = true;
        //StartCoroutine(ReloaSceneCor());
        PhotonNetwork.AutomaticallySyncScene = true;
        RestartLevel();

    }

    

    IEnumerator ReloaSceneCor()
    {
        //send RPC to other clients to load my scene
        photonView.RPC("RestartLevel", RpcTarget.Others);
        yield return null;
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(1); //restart the game
    }

    [PunRPC]
    private void RestartLevel()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.IsMessageQueueRunning = false;
        
        PhotonNetwork.LoadLevel(1);
        //photonView.RPC("RestartLevel", RpcTarget.Others);
    }


}
