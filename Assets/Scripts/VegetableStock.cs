using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class VegetableStock : Inventory
{
    [SerializeField] private ThingContainer[] containers;

    private Dictionary<ThingContainer, Thing> Stock = new Dictionary<ThingContainer, Thing>();
    private int currentContainer;

    protected override void Start()
    {
        base.Start();
        AutoPopulate();
    }

    public void AutoPopulate()
    {
        for (int i = 0; i < Capacity; i++)
        {
            currentContainer = containers[i].index = i;
            Populate(i);
        }
    }

    private void Populate(int containerIndex)
    {
        int randomNumber = Random.Range(0, 10);
        
        Destroy(transform.GetComponent<Thing>());
        
        Vegetable veg = containers[containerIndex].gameObject.AddComponent<Vegetable>();
        veg.vegetableName = (VegetableName) randomNumber;
        
        containers[containerIndex].Push(veg, OnItemRemoved);
        AddThing(veg);
    }

    protected override void AddThing(Thing thing)
    {
        base.AddThing(thing);

        if (!Stock.ContainsKey(containers[currentContainer]))
            Stock.Add(containers[currentContainer], thing);
        else
            Stock[containers[currentContainer]] = thing;
        
        containers[currentContainer].Push(thing, OnItemRemoved);
        
    }
    public void OnItemRemoved(Thing veg, int ContainerIndex)
    {
        currentContainer = ContainerIndex;
        
        RemoveThing(veg);
        Populate(ContainerIndex);
        
        print(ContainerIndex);
    }
}
