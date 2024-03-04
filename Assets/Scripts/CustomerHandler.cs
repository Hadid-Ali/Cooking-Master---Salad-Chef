using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerHandler : MonoBehaviour
{
    [SerializeField] private List<Customer> customers;

    [SerializeField] private float TimeForEachIngrediant;


    private void Start()
    {
        foreach (var v in customers)
        {
            PlaceOrder(v);
        }
    }

    public void OnCustomerOrderComplete(Customer customer)
    {
        StartCoroutine(Wait(customer));
    }

    IEnumerator Wait(Customer customer)
    {
        yield return new WaitForSeconds(3);
        PlaceOrder(customer);
    }

    private void PlaceOrder(Customer customer)
    {
        int randomCombination = Random.Range(1, 2);
        CombinationName comboname = (CombinationName) randomCombination;
        
        customer.Order(comboname, 60f, OnCustomerOrderComplete);
    }
}
