using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class Container : MonoBehaviour, IInteractable<Container>
{
    [SerializeField] private Thing Thing;

    private GameEvent<Thing, int> OnItemRemoved = new GameEvent<Thing, int>();
    public int index;

    private Action<Thing, int> CachedAction;
    
    public void Push(Thing _veg, Action<Thing, int> _OnItemRemoved)
    {
        Thing = _veg;
        
        CachedAction = _OnItemRemoved;
        OnItemRemoved.Register(CachedAction);
    }

    public void OnInteract(PlayerInteraction controller)
    {
        if(Thing == null)
            return;
        
        GameEvents.GamePlay.OnPlayerReceiveThing.Raise(controller, Thing);

        OnItemRemoved.Raise(Thing, index);
        OnItemRemoved.UnRegister(CachedAction);
        
    }

    public void OnInteract(Vegetable veg)
    {
        //UseLess
    }
    public void OnInteract()
    {
        //Useless
    }
}
