using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject();
                _instance = go.AddComponent(typeof(T)) as T;
                GameObject.DontDestroyOnLoad(go);
#if UNITY_EDITOR
                go.name = typeof(T).Name;
#endif
            }
            return _instance;
        }
    }
}
