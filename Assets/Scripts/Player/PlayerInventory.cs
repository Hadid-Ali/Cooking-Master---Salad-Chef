using System.Collections.Generic;
using TMPro;
using UnityEngine;

    
public class PlayerInventory : Inventory
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private Transform stackStart;

    [SerializeField] private GameObject visualbox;
     private Dictionary<Thing,Transform> positions = new Dictionary<Thing,Transform>();
    
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
        
        if (positions.ContainsKey(thing))
        {
             positions[thing].gameObject.SetActive(true);
        }
        else
        {
            Transform t = Instantiate(visualbox, stackStart.position + (Vector3.up * Things.Count),
                Quaternion.identity).transform;

            positions.Add(thing, t);
            positions[thing].transform.SetParent(stackStart);
        }

        base.AddThing(thing);
        
        positions[thing].GetComponentInChildren<TextMeshProUGUI>().text = thing.Name();
    }

    public override void RemoveThing(Thing thing)
    {
        if (positions.ContainsKey(thing))
            positions[thing].gameObject.SetActive(false);
        
        base.RemoveThing(thing);
    }



    public Vegetable GetTopVegetable()
    {
        foreach (var t in Things)
        {
            if (t.type != 1)
                continue;
            
            Vegetable veg = t.GetItem<Vegetable>();
            
            return veg;
        }

        return null;
    }

    public Combination GetTopMostCombination()
    {
        for (int i = 0; i < Things.Count; i++)
        {
            if (Things[i].type != 2)
                continue;
            
            Combination veg = Things[i].GetItem<Combination>();
            
            return veg;
        }
        
        return null;
    }

   
}
