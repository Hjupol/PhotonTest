using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LeaveGame : MonoBehaviourPunCallbacks
{
    public void OnClick_LeaveRoom()
    {
        
        PhotonNetwork.LeaveRoom(true);
        
    }

    public override void OnLeftRoom()
    {
        ScoreManager.scoreManager.ResetScore();
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }
}
