using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ListenOnDisable : MonoBehaviour
{
    public event Action<GameObject> OnDisableGameObject;

    void OnDisable()
    {
        OnDisableGameObject?.Invoke(this.gameObject);
    }
}
