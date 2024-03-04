using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private CharacterInput _input;
    [SerializeField] private PlayerScore _playerScore;
    
    private IInteractable<SaladMachine> _saladMachine;
    private IInteractable<ThingContainer> _container;
    private IInteractable<Customer> _customer;
    
    
    private bool CanInteract => _saladMachine != null || _container != null || _customer != null|| _isWaiting;
    private bool _isWaiting;
    
    private Vegetable _veg;
    private Combination _comb;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable<SaladMachine>>() != null)
            _saladMachine = other.GetComponent<IInteractable<SaladMachine>>();
        
        else if (other.GetComponent<IInteractable<ThingContainer>>() != null)
            _container = other.GetComponent<IInteractable<ThingContainer>>();
        
        else if (other.GetComponent<IInteractable<Customer>>() != null)
            _customer = other.GetComponent<IInteractable<Customer>>();
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable<SaladMachine>>() != null)
            _saladMachine = null;
        
        else if (other.GetComponent<IInteractable<ThingContainer>>() != null)
            _container = null;
        
        else if (other.GetComponent<IInteractable<Customer>>() != null)
            _customer = null;
        
    }

    public void Interact()
    {
        if (!CanInteract)
            return;
        

        if (_container != null && !_inventory.IsAtCapacity)
        {
            if(!_container.AllowsInteraction(this))
                return;
            
            _container.OnInteract(this);
            print("Container interaction by player");
        }
        else if (_saladMachine != null && !_inventory.IsEmpty)
        {
            if(!_saladMachine.AllowsInteraction(this))
                return;
                
            _veg = _inventory.GetTopVegetable();
            
            print(_veg);
            if(_veg == null)
                return;
            
            _saladMachine.OnInteract(this, _veg, SaladMachineInteractionComplete);
            _input.PauseInput();
            
            print($"Salad machine : {_veg}");
        }
        else if (_customer != null && !_inventory.IsEmpty)
        {
            if(!_customer.AllowsInteraction(this))
                return;
            
            _comb = _inventory.GetTopMostCombination();
            
            print(_comb);
            
            if(_comb == null)
                return;
            
            _customer.OnInteract(this, _comb.CombinationName);
            _inventory.RemoveThing(_comb);
        }

        StartCoroutine(WaitCoroutine());
    }
    



    private void SaladMachineInteractionComplete()
    {
        _inventory.RemoveThing(_veg);
        _input.ContinueInput();
    }

    IEnumerator WaitCoroutine()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(.5f);
        _isWaiting = false;
    }
}
