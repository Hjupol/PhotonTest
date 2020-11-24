using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviourPunCallbacks
{
    [PunRPC]
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal1"))
        {
            //Scoring
            Destroy(other.gameObject);
            StartCoroutine("Restart");
        }
        else if (other.gameObject.CompareTag("Goal2"))
        {
            //Scoring
            Destroy(other.gameObject);
            StartCoroutine("Restart");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(1);
        RestartLevel();
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
                PhotonNetwork.LoadLevel(2);
        }
    }
}
