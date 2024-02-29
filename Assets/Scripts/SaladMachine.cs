using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaladMachine : MonoBehaviour, IInteractable<SaladMachine>
{
    [SerializeField] private Container _container;

    [SerializeField] private Combination currentCombination;
    [SerializeField] private float timeToChop;

    [SerializeField] private TextMesh text;
    [SerializeField] private TextMesh text2;

    private bool IsChopping;
    private bool IsEmpty;

    private void Start()
    {
        currentCombination = _container.gameObject.AddComponent<Combination>();
    }

    public void OnUpdateTimer(float time)
    {
        text.text = time.ToString("F1");
    }
    public void OnChopComplete(VegetableName veg)
    {
        IsChopping = false;
        text.text = "0";
        currentCombination.Ingrediants.Add(veg);
        currentCombination.CombinationName  =  MetaDataUtility.CheckCombination(currentCombination.Ingrediants);
        
        text2.text = currentCombination.CombinationName.ToString();
        _container.Push(currentCombination, OnSaladTaken);
    }

    public void OnSaladTaken(Thing thing, int uselessHere)
    {
        currentCombination.CombinationName = CombinationName.None;
        currentCombination.Ingrediants.Clear();

        text2.text = currentCombination.CombinationName.ToString();
    }
    
    public void OnInteract()
    {
        
    }

    public void OnInteract(PlayerInteraction controller)
    {
        
    }

    public void OnInteract(Vegetable veg)
    {
        if(IsChopping)
            return;
        
        IsChopping = true;
        
        Timer.CreateTimerObject(()=> OnChopComplete(veg.VegetableName), OnUpdateTimer, timeToChop);
        print("Worked");
    }
}
