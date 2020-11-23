using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName ="Singleton/MasterManager")]
public class MasterManager : ScriptableObjectSingleton<MasterManager>
{
    [SerializeField]
    private GameSettings gameSettings;

    public static GameSettings GameSettings { get { return instance.gameSettings; } }
    [SerializeField]
    private List<NetworkedPrefab> _networkedPrefabs = new List<NetworkedPrefab>();

    public static GameObject NetworkInstantiate(GameObject obj,Vector3 position, Quaternion rotation)
    {
        foreach(NetworkedPrefab networkedPrefab in instance._networkedPrefabs)
        {
            if (networkedPrefab.Prefab == obj)
            {
                if (networkedPrefab.Path != string.Empty)
                {
                    GameObject result = PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                    return result;
                }
                else
                {
                    Debug.LogError("Path is empty for gameobject name " + networkedPrefab.Prefab);
                    return null;
                }
            }
        }
        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkedPrefabs()
    {
#if UNITY_EDITOR

        instance._networkedPrefabs.Clear();

        GameObject[] results = Resources.LoadAll<GameObject>("");
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<PhotonView>() != null)
            {
                string path= AssetDatabase.GetAssetPath(results[i]);
                instance._networkedPrefabs.Add(new NetworkedPrefab(results[i],path));
            }
        }
        //for (int i = 0; i < instance._networkedPrefabs.Count; i++)
        //{
        //    UnityEngine.Debug.Log(instance._networkedPrefabs[i].Prefab.name + ", " + instance._networkedPrefabs[i].Path);
        //}
#endif
    }
}
