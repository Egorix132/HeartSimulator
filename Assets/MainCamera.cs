using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static GameObject Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
}
