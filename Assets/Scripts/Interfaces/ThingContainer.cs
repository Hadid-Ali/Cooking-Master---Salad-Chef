using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ThingContainer : MonoBehaviour, IInteractable<ThingContainer>
{
    [SerializeField] private PlayerInteraction[] owners;   
    [SerializeField] private GameObject visualObjectPrefab;
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] public GameObject obj;
    public int index;


    [SerializeField] private Thing thing;
    private bool HasObject => obj != null;
    public bool isNotPersistant;
    
    
    private readonly GameEvent<Thing, int> _onItemRemoved = new GameEvent<Thing, int>();
    private Action<Thing, int> _cachedAction;

    private void Awake()
    {
        visualObjectPrefab = Resources.Load<GameObject>("Box");
        spawnPoint = transform.GetChild(0);
    }

    public void Push(Thing thing, Action<Thing, int> onItemRemoved)
    {
        this.thing = thing;
        
        _cachedAction = onItemRemoved;
        _onItemRemoved.Register(_cachedAction);

        if (!HasObject)
            obj =  Instantiate(visualObjectPrefab, spawnPoint.position, Quaternion.identity);
        
        if(isNotPersistant)
            obj.SetActive(true);
        
        obj.GetComponentInChildren<TextMeshProUGUI>().SetText(thing.Name());
    }

    public void Push(Thing thing, Action<Thing, int> onItemRemoved, Combination combo)
    {
        this.thing = thing;
        
        _cachedAction = onItemRemoved;
        _onItemRemoved.Register(_cachedAction);

        if (!HasObject)
            obj =  Instantiate(visualObjectPrefab, spawnPoint.position, Quaternion.identity);
        
        if(isNotPersistant)
            obj.SetActive(true);
        
        Combination comb = obj.AddComponent<Combination>();
        comb.ingredients = combo.ingredients;
        comb.recipeName = combo.recipeName;

        this.thing = comb;
        obj.GetComponentInChildren<TextMeshProUGUI>().SetText(thing.Name());
    }
    
    
    public void OnInteract(PlayerInteraction controller)
    {
        if(thing == null )
            return;
        
        if(isNotPersistant && !obj.activeSelf) //Exception for not persistant
            return;
            
        
        GameEvents.GamePlay.OnPlayerReceiveThing.Raise(controller, thing);

        _onItemRemoved.Raise(thing, index);
        _onItemRemoved.UnRegister(_cachedAction);
        
        if(isNotPersistant)
            obj.SetActive(false);
        
    }

    public void OnInteract(PlayerInteraction controller, Vegetable veg, Action completed)
    {
       //Useless
    }

    public void OnInteract(PlayerInteraction controller, RecipeName veg)
    {
        
    }

    public bool AllowsInteraction(PlayerInteraction controller)
    {
        var contains = owners.Contains(controller);
        return contains;
    }

    public PowerupType OnInteract()
    {
        return PowerupType.AddScore;
    }
}
