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

    [SerializeField] private GameObject visualbox;
     private Dictionary<int,Transform> positions = new Dictionary<int,Transform>();
    
    private void Awake()
    {
        GameEvents.GamePlay.OnPlayerReceiveThing.Register(ReceiveThing);
        visualbox = Resources.Load<GameObject>("Box");
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

    protected override void AddThing(Thing thing)
    {
        if(Things.Count >= Capacity)
            return;
        
        if (positions.Count < 2)
        {
            Transform t = Instantiate(visualbox, stackStart.position + (Vector3.up * Things.Count),
                Quaternion.identity).transform;

            if (positions.Count > 0)
            {
                if(!positions.ContainsKey(Things.Count))
                    positions.Add(Things.Count, t);
            }
            
            positions[Things.Count] = t;
            positions[Things.Count].transform.SetParent(stackStart);
        }
        
        base.AddThing(thing);
        
        positions[Things.Count].gameObject.SetActive(true);
        positions[Things.Count].GetComponentInChildren<TextMesh>().text = thing.Name();
    }

    public override void RemoveThing(Thing thing)
    {
        positions[Things.Count].gameObject.SetActive(false);
        base.RemoveThing(thing);
    }



    public Vegetable GetTopVegetable()
    {
        for (int i = 0; i < Things.Count; i++)
        {
            if (Things[i].type != 1)
                return null;
            
            Vegetable veg = Things[i].GetItem<Vegetable>();
            
            return veg;
        }
        
        return null;
    }

    public Combination GetTopMostCombination()
    {
        for (int i = 0; i < Things.Count; i++)
        {
            if (Things[i].type != 2)
                return null;
            
            Combination veg = Things[i].GetItem<Combination>();
            
            return veg;
        }
        
        return null;
    }

   
}
