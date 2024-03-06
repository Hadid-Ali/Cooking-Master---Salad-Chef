using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaladMachine : MonoBehaviour, IInteractable<SaladMachine>
{
    [SerializeField] private PlayerInteraction owner;
    [FormerlySerializedAs("_container")] [SerializeField] private ThingContainer thingContainer;
    
    [SerializeField] private Image image;

    [SerializeField] private Combination currentCombination;

    private bool _isChopping;

    private VegetableName _currentVegName;
    private Action _onComplete;

    private void Start()
    {
        currentCombination = gameObject.AddComponent<Combination>();
    }

    private void OnUpdateTimer(float time)
    {
        image.fillAmount = time / MetaDataUtility.MetaData.timeToChop;
    }
    private void OnChopComplete()
    {
        _isChopping = false;
        
        image.fillAmount = 0;
        
        currentCombination.ingredients.Add(_currentVegName);
        currentCombination.recipeName  =  MetaDataUtility.CheckCombination(currentCombination.ingredients);
        

        thingContainer.Push(currentCombination, OnSaladTaken, currentCombination);
        
        _onComplete.Invoke();
    }

    private void OnSaladTaken(Thing thing, int uselessHere)
    {
        currentCombination.recipeName = RecipeName.None;
        currentCombination.ingredients.Clear();
        
    }

    public bool AllowsInteraction(PlayerInteraction controller)
    {
        return owner == controller;
    }

    public PowerupType OnInteract()
    {
        return PowerupType.AddScore;
    }

    public void OnInteract(PlayerInteraction controller)
    {
        
    }

    public void OnInteract(PlayerInteraction controller, Vegetable veg, Action completed)
    {
        if(_isChopping || owner != controller)
            return;
        
        this._onComplete += completed;
        
        _isChopping = true;

        _currentVegName = veg.vegetableName;
        Timer.Instance.StartTimer(OnChopComplete, OnUpdateTimer, MetaDataUtility.MetaData.timeToChop);
        
    }

    public void OnInteract(PlayerInteraction controller, RecipeName veg)
    {
        
    }
}
