using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;


public class ScoreManager : MonoBehaviourPun
{

    public static ScoreManager scoreManager;

    


    public Text p1ScoreText;
    public Text p2ScoreText;

    public int p1Score;
    public int p2Score;

    public BallController ballController;

    private const byte P1_SCORE_EVENT = 4;
    private const byte P2_SCORE_EVENT = 5;

    void Awake()
    {
        p1Score = 0;
        p2Score = 0;
        if (scoreManager == null)
        {
            scoreManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == P1_SCORE_EVENT)
        {
            int scored = (int)obj.CustomData;
            p1Score = scored;
        }
        if (obj.Code == P2_SCORE_EVENT)
        {
            int scored = (int)obj.CustomData;
            p2Score = scored;
        }
    }


    void Update()
    {
        p1ScoreText.text = "Player 1: " + p1Score.ToString();
        p2ScoreText.text = "Player 2: " + p2Score.ToString();
    }

    

    public void P1Scores()
    {
        p1Score++;
        int datas = p1Score;
        PhotonNetwork.RaiseEvent(P1_SCORE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    public void P2Scores()
    {
        p2Score++;
        int datas = p2Score;
        PhotonNetwork.RaiseEvent(P2_SCORE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }


    [PunRPC]
    public void RPC_P1Scores()
    {
        p1Score++;
    }

    [PunRPC]
    public void RPC_P2Scores()
    {
        p2Score++;
    }
}
