using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Thing : MonoBehaviour
{
    public int type;
    public abstract string Name();
    public abstract T GetItem<T>();
    public abstract void SetProperties(Thing thing);

    protected virtual void Start()
    {
        type = 0;
    }
}
