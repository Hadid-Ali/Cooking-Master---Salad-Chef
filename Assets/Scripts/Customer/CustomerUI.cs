using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    
    [SerializeField] private Customer customer1;
    [SerializeField] private Customer customer2;
    
    private string _s1;
    private string _s2;
    private void Awake()
    {
        Customer.OnCustomerPlaceOrder += OnCustomerPlaceOrder;
    }

    private void OnDestroy()
    {
        Customer.OnCustomerPlaceOrder -= OnCustomerPlaceOrder;
    }

    private void OnCustomerPlaceOrder(Customer customer,string obj)
    {
        if (customer == customer1)
            _s1 = obj;
        else if(customer2 == customer)
            _s2 = obj;
        
        text.SetText($"Customer1 :  {_s1}   Customer2 :  {_s2} ");
    }
}
