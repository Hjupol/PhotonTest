using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateExample : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Transform p1Spawn;
    [SerializeField]
    private Transform p2Spawn;

    private void Awake()
    {
        Vector2 offset = Random.insideUnitCircle * 3f;
        Vector3 position;

        position = new Vector3(transform.position.x+offset.x, transform.position.y+offset.y, transform.position.z);
        //Use this to spawn only two players in their respective positions
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    position = new Vector3(p1Spawn.position.x, p1Spawn.position.y, p1Spawn.position.z);
        //}
        //else
        //{
        //    position = new Vector3(p2Spawn.position.x, p2Spawn.position.y, p2Spawn.position.z);
        //}
        StartCoroutine("Instantiate", position);
    }


    IEnumerator Instantiate(Vector3 position)
    {
        yield return new WaitForSeconds(1.5f);
        MasterManager.NetworkInstantiate(_prefab, position, Quaternion.identity);
    }
}
