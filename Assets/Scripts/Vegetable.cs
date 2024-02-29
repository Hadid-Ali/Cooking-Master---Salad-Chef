using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Vegetable : Thing
{
    public VegetableName VegetableName;
    
    public override T GetItem<T>()
    {
        object value = this;
        return (T) Convert.ChangeType(value, typeof(T));
    }
}
