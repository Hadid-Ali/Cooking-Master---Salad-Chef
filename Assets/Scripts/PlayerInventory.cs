using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventory : Inventory
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private Transform stackStart;
    
    private void Awake()
    {
        GameEvents.GamePlay.OnPlayerReceiveThing.Register(ReceiveThing);
    }

    private void OnDestroy()
    {
        GameEvents.GamePlay.OnPlayerReceiveThing.UnRegister(ReceiveThing);
    }
    
    public virtual void ReceiveThing(PlayerInteraction player, Thing thing)
    {
        if(player != playerInteraction)
            return;
        
        AddThing(thing);
    }

    public override void AddThing(Thing thing)
    {
        if(Things.Count >= Capacity)
            return;
        
        base.AddThing(thing);
        
        thing.transform.position = stackStart.position + (Vector3.up * Things.Count);
        thing.transform.SetParent(stackStart);
    }



    public Vegetable GetTopVegetable()
    {
        for (int i = 0; i < Things.Count; i++)
        {
            Vegetable veg = Things[i].GetItem<Vegetable>();
            RemoveThing(veg);
            Destroy(veg.gameObject, 3);
            
            return veg;
        }
        
        return null;
    }

   
}
