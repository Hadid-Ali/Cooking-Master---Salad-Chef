using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour, IInteractable<Customer>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image timerImage;
    
    public CustomerState customerState = CustomerState.Idle;
    public Combination requiredCombination;
    
    private Action<Customer> _onCustomerOrderComplete;
    public static Action<Customer,string> OnCustomerPlaceOrder;
    
    private float _currentTime;
    private float _waitTime;
    private bool _isAngry;

    private Timer _timer;

    public void Order(Combination c, float time, Action<Customer> onCustomerOrderComplete)
    {
        gameObject.SetActive(true);
        _isAngry = false;
        
        requiredCombination = c;
        this._onCustomerOrderComplete = onCustomerOrderComplete;
        customerState = CustomerState.Waiting;

        _waitTime = time;
        _timer = Timer.Instance.StartTimer(OnTimerComplete, OnTimerTick, time);
        UpdateUI();
    }

    public void UpdateUI()
    {
        text.SetText($"{customerState} : {requiredCombination.recipeName}");
        
        string ingredients = requiredCombination.recipeName.ToString() + " \n";
        for (int i = 0; i < requiredCombination.ingredients.Count; i++)
            ingredients += $"{requiredCombination.ingredients[i]} \n";
        
        OnCustomerPlaceOrder?.Invoke(this,ingredients);
    }
    


    private void OnTimerTick(float obj)
    {
        _currentTime = obj;
        
        timerImage.fillAmount = _currentTime / 60;
        timerImage.color = _isAngry?  Color.red : Color.white;
    }

    private void OnTimerComplete()
    {
        _timer = null;
        
        _onCustomerOrderComplete.Invoke(this);
        
        PlayerScore.OnScoreSubAll?.Invoke(MetaDataUtility.MetaData.customerLeaveScoreSubtractAmount);
    }

    private void EvaluateOrder(PlayerInteraction interaction, RecipeName recipeName)
    {
        if (requiredCombination.recipeName == recipeName)
        {
            customerState =  CustomerState.Served ;
            
            if ((_currentTime / _waitTime * 100) >= MetaDataUtility.MetaData.powerUpPercentage)
            {
                SpawnPowerUp();
            }

            int calculatedScore =
                MetaDataUtility.MetaData.scoreForEachIngredient * requiredCombination.ingredients.Count;
            
            PlayerScore.OnScoreAdd?.Invoke(interaction, calculatedScore);
            
            text.SetText($"{customerState}");
        
            _timer = null;
            _onCustomerOrderComplete.Invoke(this);
        }
        else if(!_isAngry) 
        {
            _isAngry = true;
            customerState = CustomerState.Angry;
            PlayerScore.OnScoreSub?.Invoke(interaction, 10);
            _timer.DoubleTheSpeed();
            text.SetText($"{customerState} : {requiredCombination.recipeName}");
        }
        else
        {
            PlayerScore.OnScoreSub?.Invoke(interaction, 15);
        }
        
        
    }

    public bool AllowsInteraction(PlayerInteraction controller)
    {
        return customerState == CustomerState.Waiting || customerState == CustomerState.Angry;
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

    public void OnInteract(PlayerInteraction controller, Vegetable veg, Action completed)
    {
        
    }

    public void OnInteract(PlayerInteraction controller, RecipeName veg)
    {
        EvaluateOrder(controller, veg);
    }
}
