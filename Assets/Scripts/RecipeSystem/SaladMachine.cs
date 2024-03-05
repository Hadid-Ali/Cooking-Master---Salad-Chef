using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaladMachine : MonoBehaviour, IInteractable<SaladMachine>
{
    [SerializeField] private PlayerInteraction Owner;
    [FormerlySerializedAs("_container")] [SerializeField] private ThingContainer thingContainer;
    
    [SerializeField] private Image _image;

    [SerializeField] private Combination _currentCombination;

    private bool _isChopping;

    private VegetableName _currentVegName;
    private Action _onComplete;

    private void Start()
    {
        _currentCombination = gameObject.AddComponent<Combination>();
    }

    private void OnUpdateTimer(float time)
    {
        _image.fillAmount = time / MetaDataUtility.metaData.TimeToChop;
    }
    private void OnChopComplete()
    {
        _isChopping = false;
        
        _image.fillAmount = 0;
        
        _currentCombination.Ingrediants.Add(_currentVegName);
        _currentCombination.CombinationName  =  MetaDataUtility.CheckCombination(_currentCombination.Ingrediants);
        

        thingContainer.Push(_currentCombination, OnSaladTaken, _currentCombination);
        
        _onComplete.Invoke();
    }

    private void OnSaladTaken(Thing thing, int uselessHere)
    {
        _currentCombination.CombinationName = CombinationName.None;
        _currentCombination.Ingrediants.Clear();
        
    }

    public bool AllowsInteraction(PlayerInteraction controller)
    {
        return Owner == controller;
    }

    public PowerupType OnInteract()
    {
        return PowerupType.AddScore;
    }

    public void OnInteract(PlayerInteraction controller)
    {
        
    }

    public void OnInteract(PlayerInteraction controller, Vegetable veg, Action onComplete)
    {
        if(_isChopping || Owner != controller)
            return;
        
        this._onComplete += onComplete;
        
        _isChopping = true;

        _currentVegName = veg.vegetableName;
        Timer.Instance.StartTimer(OnChopComplete, OnUpdateTimer, MetaDataUtility.metaData.TimeToChop);
    }

    public void OnInteract(PlayerInteraction controller, CombinationName veg)
    {
        
    }
}
