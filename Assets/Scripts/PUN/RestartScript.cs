using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviourPun
{
    void Start()
    {
        //StartCoroutine(ReloaSceneCor());
        RestartLevel();
    }


    IEnumerator ReloaSceneCor()
    {
        //send RPC to other clients to load my scene
        photonView.RPC("RestartLevel", RpcTarget.Others);
        yield return null;
        PhotonNetwork.LoadLevel(1); //restart the game
    }

    //[PunRPC]
    private void RestartLevel()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(1);
        //photonView.RPC("RestartLevel", RpcTarget.Others);
    }


}
