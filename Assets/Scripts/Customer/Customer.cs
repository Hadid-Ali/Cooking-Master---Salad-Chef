using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour, IInteractable<Customer>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image timerImage;
    
    public CustomerState customerState = CustomerState.Idle;
    public CombinationName requiredCombinationName;
    
    private Action<Customer> OnCustomerOrderComplete;
    
    private float _currentTime;
    private float _waitTime;
    private bool _isAngry;
    

    public void Order(CombinationName c, float time, Action<Customer> onCustomerOrderComplete)
    {
        gameObject.SetActive(true);
        
        requiredCombinationName = c;
        OnCustomerOrderComplete = onCustomerOrderComplete;
        customerState = CustomerState.Waiting;

        _waitTime = time;
        Timer.Instance.StartTimer(OnTimerComplete, OnTimerTick, time);
    }

    
    IEnumerator  OnOrderServed()
    {
        text.SetText($"{requiredCombinationName} : {customerState}");
        
        yield return new WaitForSeconds(2);
        OnCustomerOrderComplete.Invoke(this);
    }

    private void OnTimerTick(float obj)
    {
        if(customerState != CustomerState.Waiting)
            return;
        
        _currentTime = obj;
        
        text.SetText($"{requiredCombinationName} : {customerState}");
        timerImage.fillAmount = _currentTime / 60;
    }

    private void OnTimerComplete()
    {
        OnCustomerOrderComplete.Invoke(this);
        
        PlayerScore.OnScoreSubAll?.Invoke(MetaDataUtility.metaData.customerLeaveScoreSubtractAmount);
    }

    private void EvaluateOrder(PlayerInteraction interaction,CombinationName combinationName)
    {
        if (requiredCombinationName == combinationName)
        {
            customerState =  CustomerState.Served ;
            
            if ((_currentTime / _waitTime * 100) >= MetaDataUtility.metaData.powerUpPercentage)
            {
                SpawnPowerUp();
            }
            
            PlayerScore.OnScoreAdd?.Invoke(interaction, 30);
            print("Right Combo");
        }
        else if(!_isAngry) 
        {
            _isAngry = true;
            PlayerScore.OnScoreSub?.Invoke(interaction, 10);
            print("Wrong Combo");
        }
        else
        {
            PlayerScore.OnScoreSub?.Invoke(interaction, 30);
            print("Wrong Combo Angry");
        }
        
        if(customerState == CustomerState.Served)
            StartCoroutine(OnOrderServed());
    }

    public bool AllowsInteraction(PlayerInteraction controller)
    {
        return customerState == CustomerState.Waiting ;
    }

    private void SpawnPowerUp()
    {
        int random = Random.Range(-2, 2);
        Vector3 randomPosition =  new Vector3(random, 0, random);
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("Box"), randomPosition, Quaternion.identity);
        int random2 = Random.Range(0, 3);
        
        gameObject.AddComponent<PowerUp>().type = (PowerupType) random2;
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText($"{(PowerupType) random2}");
        
        print("Power Up Spawned");
    }
    public PowerupType OnInteract() 
    {
        return PowerupType.AddScore;
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
