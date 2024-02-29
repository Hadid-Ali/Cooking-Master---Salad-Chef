using System;
using System.Collections.Generic;
using UnityEngine;

public class Combination : Thing
{
    public List<VegetableName> Ingrediants = new List<VegetableName>();
    public CombinationName CombinationName;
    
    public override T GetItem<T>()
    {
        object value = this;
        return (T) Convert.ChangeType(value, typeof(T));
    }
}
