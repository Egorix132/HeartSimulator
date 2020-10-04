using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static readonly List<string> eternalObjects = new List<string>();
    void Start()
    {
        if (eternalObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
        }
        else
        {
            eternalObjects.Add(gameObject.name);
            DontDestroyOnLoad(gameObject);
            return;
        }
    }
}
