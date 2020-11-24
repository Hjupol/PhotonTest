using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateExample : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    private void Awake()
    {
        Vector2 offset = Random.insideUnitCircle * 3f;
        Vector3 position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z);
        StartCoroutine("Instantiate", position);
    }


    IEnumerator Instantiate(Vector3 position)
    {
        yield return new WaitForSeconds(1.5f);
        MasterManager.NetworkInstantiate(_prefab, position, Quaternion.identity);
    }
}
