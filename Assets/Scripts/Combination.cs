using System;
using System.Collections.Generic;
using UnityEngine;

public class Combination : Thing
{
    public List<VegetableName> Ingrediants = new List<VegetableName>();
    public CombinationName CombinationName;
    
    protected override void Start()
    {
        type = 2;
    }
    
    public override void SetProperties(Thing thing)
    {
        Combination comb = thing.GetItem<Combination>();
        
        CombinationName = comb.CombinationName;
        Ingrediants = comb.Ingrediants;
    }

    public override string Name()
    {
        return CombinationName.ToString();
    }

    public override T GetItem<T>()
    {
        object value = this;
        return (T) Convert.ChangeType(value, typeof(T));
    }
}
