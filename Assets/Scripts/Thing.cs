using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Thing : MonoBehaviour
{
    public abstract T GetItem<T>();

}
