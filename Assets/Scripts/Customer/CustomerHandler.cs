using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerHandler : MonoBehaviour
{
    [SerializeField] private List<Customer> customers;

    private bool GameOver;
    private void Start()
    {
        foreach (var v in customers)
        {
            PlaceOrder(v);
        }

        GameManager.OnPlayerWin += OnGameOver;
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerWin -= OnGameOver;
    }

    private void OnGameOver(PlayerTimer arg1, int arg2)
    {
        GameOver = true;
    }

    public void OnCustomerOrderComplete(Customer customer)
    {
        StartCoroutine(Wait(customer));
    }

    IEnumerator Wait(Customer customer)
    {
        yield return new WaitForSeconds(1.5f);
        PlaceOrder(customer);
    }

    private void PlaceOrder(Customer customer)
    {
        if(GameOver)
            return;
        
        int randomCombination = Random.Range(1, MetaDataUtility.Recipes.Count);

        var comb = customer.gameObject.GetComponent<Combination>();
        
        if (comb == null)
            comb = customer.gameObject.AddComponent<Combination>();
        
        comb.recipeName = MetaDataUtility.Recipes[randomCombination].recipeName; 
        comb.ingredients = MetaDataUtility.Recipes[randomCombination].vegetables;

        float totalTime = MetaDataUtility.MetaData.timeForEachIngredient * comb.ingredients.Count;
        
        customer.Order(comb, totalTime, OnCustomerOrderComplete);
    }
}
