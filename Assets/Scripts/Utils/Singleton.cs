using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }
    public bool IsInitialized { get { return instance != null; } }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("[SINGLETON] Try to instanciate two singleton of type " + typeof(T).Name);
            Destroy(gameObject);
        }
        instance = (T)this;

    }

    // Update is called once per frame
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }

    }
}
