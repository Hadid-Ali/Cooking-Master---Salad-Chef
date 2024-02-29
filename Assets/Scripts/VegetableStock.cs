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
    [SerializeField] private Container[] containers;
    [SerializeField] private GameObject prefab;

    private Dictionary<Container, Thing> Stock = new Dictionary<Container, Thing>();
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
        
        Transform spawnParent = containers[containerIndex].transform.GetChild(0);
        
        GameObject obj =  Instantiate(prefab, spawnParent.position, Quaternion.identity);
        obj.transform.SetParent(spawnParent);
        Vegetable veg = obj.GetComponent<Vegetable>();
            
        VegetableName name = (VegetableName) randomNumber;
        veg.VegetableName = name;

        TextMesh text = veg.gameObject.GetComponentInChildren<TextMesh>();
        text.text = name.ToString();
        
        
        AddThing(veg);
    }

    public override void AddThing(Thing thing)
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
