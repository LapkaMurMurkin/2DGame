using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        if (Instance is null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(this);
        }
    }
}
