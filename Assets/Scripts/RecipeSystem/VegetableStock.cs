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

    private readonly Dictionary<ThingContainer, Thing> _stock = new Dictionary<ThingContainer, Thing>();
    private int _currentContainer;

    protected override void Start()
    {
        base.Start();
        AutoPopulate();
    }

    public void AutoPopulate()
    {
        for (int i = 0; i < Capacity; i++)
        {
            _currentContainer = containers[i].index = i;
            Populate(i);
        }
    }

    private void Populate(int containerIndex)
    {
        int randomNumber = Random.Range(0, MetaDataUtility.metaData.vegetableTotalNumber);
        
        Destroy(transform.GetComponent<Thing>());
        
        Vegetable veg = containers[containerIndex].gameObject.AddComponent<Vegetable>();
        veg.vegetableName = (VegetableName) randomNumber;
        
        containers[containerIndex].Push(veg, OnItemRemoved);
        AddThing(veg);
    }

    protected override void AddThing(Thing thing)
    {
        base.AddThing(thing);

        if (!_stock.ContainsKey(containers[_currentContainer]))
            _stock.Add(containers[_currentContainer], thing);
        else
            _stock[containers[_currentContainer]] = thing;
        
        containers[_currentContainer].Push(thing, OnItemRemoved);
        
    }
    private void OnItemRemoved(Thing veg, int containerIndex)
    {
        _currentContainer = containerIndex;
        
        RemoveThing(veg);
        Populate(containerIndex);
        
    }
}
