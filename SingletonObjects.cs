using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObjects<T> : MonoBehaviour where T : SingletonObjects<T>
{
    static T _instance;
    static GameObject _oSingletonObject;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _oSingletonObject = new GameObject("[Singleton] " + typeof(T).ToString());
                _instance = _oSingletonObject.AddComponent<T>();
            }
            return _instance;
        }
    }
}