using UnityEngine;

public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyInmediatly = false) 
    {
        foreach(Transform child in t)
        {
            if (destroyInmediatly)
                MonoBehaviour.DestroyImmediate(child.gameObject);
            else
                MonoBehaviour.Destroy(child.gameObject);
        }
    }
}
