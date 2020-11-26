using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviourPunCallbacks
{
    

    private void Start()
    {
        //PhotonNetwork.IsMessageQueueRunning = true;
        //scoreManager = GameObject.Find("CanvasGame").GetComponent<ScoreManager>();
    }

    [PunRPC]
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal1"))
        {
            //Scoring
            Destroy(other.gameObject);
            ScoreManager.scoreManager.P1Scores();
            StartCoroutine("Restart");
        }
        else if (other.gameObject.CompareTag("Goal2"))
        {
            //Scoring
            Destroy(other.gameObject);
            ScoreManager.scoreManager.P2Scores();
            StartCoroutine("Restart");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(1);
        photonView.RPC("RestartLevel", RpcTarget.Others);
        yield return null;
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(2); //restart the game
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    [PunRPC]
    private void RestartLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.IsMessageQueueRunning = false;
            
            PhotonNetwork.LoadLevel(2);
        }
    }
}
