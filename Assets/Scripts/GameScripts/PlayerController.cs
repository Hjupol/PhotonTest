using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerController : MonoBehaviourPun
{
    private Rigidbody rb;
    public float speed;


    public float jumpForce;
    public LayerMask groundLayer;
    public CapsuleCollider coll;

    public float dashForce;

    private KeyCode Player1lastKey;
    //private KeyCode Player2lastKey;

    public int player1RemainingDashes;
    private int initialDashes;
    //public int player2RemainingDashes;

    private Renderer pRend;
    private TrailRenderer tRend;
    private Vector3 colorVector;
    private bool colorAssigned = false;
    private const byte COLOR_SET = 2;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;


    private void Awake()
    {
        initialDashes = player1RemainingDashes;
        pRend = GetComponent<Renderer>();
        tRend = GetComponent<TrailRenderer>();
        colorAssigned = false;

        //if (base.photonView.IsMine)
        //{
        //    if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RandomVector3"))
        //    {
        //        colorVector = (Vector3)PhotonNetwork.LocalPlayer.CustomProperties["RandomVector3"];
        //        RpcSetColor(colorVector);
        //    }
        //    //else
        //    //{
        //    //    pRend.material.color = Color.red;

        //    //}
        //}
    }

    
    private void RpcSetColor(Vector3 _colorVector)
    {
        pRend.material.color = new Color(_colorVector.x, _colorVector.y, _colorVector.z);

        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = new Color(_colorVector.x, _colorVector.y, _colorVector.z);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(_colorVector.x, _colorVector.y, _colorVector.z);
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        tRend.colorGradient = gradient;

        object[] datas = { colorVector.x, colorVector.y, colorVector.z };
        PhotonNetwork.RaiseEvent(COLOR_SET, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
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
        if (obj.Code == COLOR_SET)
        {
            object[] datas = (object[])obj.CustomData;
            float r = (float)datas[0];
            float g = (float)datas[1];
            float b = (float)datas[2];
            if (!base.photonView.IsMine &&!colorAssigned)
            {
                pRend.material.color = new Color(r, g, b, 1f);

                gradient = new Gradient();

                // Populate the color keys at the relative time 0 and 1 (0 and 100%)
                colorKey = new GradientColorKey[2];
                colorKey[0].color = new Color(r, g, b);
                colorKey[0].time = 0.0f;
                colorKey[1].color = new Color(r, g, b);
                colorKey[1].time = 1.0f;

                // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
                alphaKey = new GradientAlphaKey[2];
                alphaKey[0].alpha = 1.0f;
                alphaKey[0].time = 0.0f;
                alphaKey[1].alpha = 0.0f;
                alphaKey[1].time = 1.0f;

                gradient.SetKeys(colorKey, alphaKey);
                tRend.colorGradient = gradient;
                colorAssigned = true;
            }
        }

    }

    void Start()
    {
        
        if (base.photonView.IsMine)
        {
            
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RandomVector3"))
            {
                colorVector = (Vector3)PhotonNetwork.LocalPlayer.CustomProperties["RandomVector3"];
                RpcSetColor(colorVector);
            }
            else
            {
                colorVector = new Vector3(255,0,0);
                RpcSetColor(colorVector);
            }
            player1RemainingDashes = initialDashes;
            if (this != null)
            {

                rb = GetComponent<Rigidbody>();
                coll = GetComponent<CapsuleCollider>();

                //transform.localScale = ModeManager.Instance.mode.playerSize;
                if (transform.localScale.x > 1f)
                {
                    jumpForce *= 1.4f;
                }

                Player1lastKey = KeyCode.D;
                //Player2lastKey = KeyCode.LeftArrow;
            }
        }
    }    


    // Update is called once per frame
    void Update()
    {
        
        if (!base.photonView.IsMine)
            return;

        InputManager();
    }

    
    //public void CmdRestartPos(Vector3 position, Quaternion rotation)
    //{
    //    rb = GetComponent<Rigidbody>();
    //    coll = GetComponent<CapsuleCollider>();
    //    player1RemainingDashes = initialDashes;


    //    transform.localScale = ModeManager.Instance.mode.playerSize;
    //    //networkTransform.transformSyncMode = NetworkTransform.TransformSyncMode.SyncTransform;

    //    //transform.position = position;

    //    //networkTransform.transformSyncMode = NetworkTransform.TransformSyncMode.SyncRigidbody3D;

    //    //transform.rotation = rotation;

    //    if (transform.localScale.x > 1f)
    //    {
    //        jumpForce *= 1.4f;
    //    }

    //    Player1lastKey = KeyCode.D;
    //    Player2lastKey = KeyCode.LeftArrow;

    //}

    private void InputManager()
    {
        //if (!ModeManager.Instance.mode.invertedControls)
        if (Input.GetKey(KeyCode.A))
        {
            Player1lastKey = KeyCode.A;
            rb.velocity = new Vector3(-speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Player1lastKey = KeyCode.D;
            rb.velocity = new Vector3(speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y);
        }
        Jump();
        Dash();
        RestartCheck();
    }

    [PunRPC]
    private void RestartCheck()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("Restart");
            }
        }
    }

    IEnumerator Restart()
    {
        
        photonView.RPC("RestartLevel", RpcTarget.Others);
        yield return null;
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(2); //restart the game
    }

    [PunRPC]
    private void RestartLevel()
    {
        PhotonNetwork.LoadLevel(2);
        
    }

    private void Jump()
    {
        //if (!ModeManager.Instance.mode.invertedControls)
            if (isGrounded() && Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
    }

    private void Dash()
    {
        
            if (Input.GetKeyDown(KeyCode.Space) && player1RemainingDashes > 0)
            {
                if (Player1lastKey == KeyCode.A)
                {
                    rb.AddForce(Vector3.left * dashForce, ForceMode.VelocityChange);
                }
                else if (Player1lastKey == KeyCode.D)
                {
                    rb.AddForce(Vector3.right * dashForce, ForceMode.VelocityChange);
                }

                player1RemainingDashes--;
            }
    }

    private bool isGrounded()
    {
        Vector3 endVector = new Vector3(coll.bounds.center.x, coll.bounds.min.y, coll.bounds.center.z);
        return Physics.CheckCapsule(coll.bounds.center, endVector, coll.radius * .9f, groundLayer);
    }
}
