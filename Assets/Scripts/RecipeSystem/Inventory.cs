using System;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{ 
    [field: SerializeField] protected virtual List<Thing> Things {private set; get;}
    [field: SerializeField] public int Capacity {private set; get;}

    public bool IsAtCapacity => Things.Count >= Capacity;
    public bool IsEmpty => Things.Count <= 0;


    protected virtual void Start()
    {
        Things = new List<Thing>();
    }
    protected virtual void AddThing(Thing thing)
    {
        if(Things.Count >= Capacity)
            return;
        
        Things.Add(thing);
    }

    public virtual void RemoveThing(Thing thing)
    {
        if(Things.Count <= 0)
            return;

        Things.Remove(thing);
    }



}
