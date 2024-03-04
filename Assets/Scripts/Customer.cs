using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Customer : MonoBehaviour, IInteractable<Customer>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image timerImage;
    
    public CustomerState customerState = CustomerState.Idle;
    public CombinationName requiredCombinationName;
    
    private Action<Customer> OnCustomerOrderComplete;
    
    private float currentTime;
    private bool _isAngry;
    

    public void Order(CombinationName c, float time, Action<Customer> onCustomerOrderComplete)
    {
        requiredCombinationName = c;
        
        OnCustomerOrderComplete = onCustomerOrderComplete;
        customerState = CustomerState.Waiting;
        Timer.CreateTimerObject(OnTimerComplete, OnTimerTick, time);
    }

    
    IEnumerator  OnOrderServed()
    {
        text.SetText($"{customerState}");
        yield return new WaitForSeconds(2);
        customerState = CustomerState.Gone;
        OnCustomerOrderComplete.Invoke(this);
    }

    private void OnTimerTick(float obj)
    {
        if(customerState != CustomerState.Waiting)
            return;
        
        print("Time Ticking");
        
        currentTime = obj;
        
        text.SetText($"{customerState} : {currentTime:F1}");
        timerImage.fillAmount = currentTime / 60;
    }

    private void OnTimerComplete()
    {
        customerState = CustomerState.Gone;
    }

    private void EvaluateOrder(PlayerInteraction interaction,CombinationName combinationName)
    {
        if (requiredCombinationName == combinationName)
        {
            customerState =  CustomerState.Served ;
            PlayerScore.OnScoreAdd?.Invoke(interaction, 30);
        }
        else if(!_isAngry) 
        {
            _isAngry = true;
            PlayerScore.OnScoreSub?.Invoke(interaction, 10);
        }
        else
        {
            PlayerScore.OnScoreSub?.Invoke(interaction, 30);
        }
        
        if(customerState == CustomerState.Served)
            StartCoroutine(OnOrderServed());
    }

    public bool AllowsInteraction(PlayerInteraction controller)
    {
        return customerState == CustomerState.Waiting ;
    }

    public void OnInteract()
    {
        
    }

    public void OnInteract(PlayerInteraction controller)
    {
        
    }

    public void OnInteract(PlayerInteraction controller, Vegetable veg, Action Completed)
    {
        
    }

    public void OnInteract(PlayerInteraction controller, CombinationName veg)
    {
        EvaluateOrder(controller, veg);
    }
}
