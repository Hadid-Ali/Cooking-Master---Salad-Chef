using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Vegetable : Thing
{
    public VegetableName vegetableName;

    public override void SetProperties(Thing thing)
    {
        vegetableName = thing.GetItem<Vegetable>().vegetableName;
    }

    protected override void Start()
    {
        type = 1;
    }

    public override string Name()
    {
        return vegetableName.ToString();
    }

    public override T GetItem<T>()
    {
        object value = this;
        return (T) Convert.ChangeType(value, typeof(T));
    }
}
