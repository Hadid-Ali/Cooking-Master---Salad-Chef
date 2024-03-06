using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Combination : Thing
{
    public List<VegetableName> ingredients = new List<VegetableName>();
    [FormerlySerializedAs("CombinationName")] public RecipeName recipeName;
    
    protected override void Start()
    {
        type = 2;
    }
    
    public override void SetProperties(Thing thing)
    {
        Combination comb = thing.GetItem<Combination>();
        
        recipeName = comb.recipeName;
        ingredients = comb.ingredients;
    }

    public override string Name()
    {
        return recipeName.ToString();
    }

    public override T GetItem<T>()
    {
        object value = this;
        return (T) Convert.ChangeType(value, typeof(T));
    }
}
