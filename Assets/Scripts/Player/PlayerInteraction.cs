using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private CharacterInput _input;
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private PlayerTimer _playerTimer;
    
    private IInteractable<SaladMachine> _saladMachine;
    private IInteractable<ThingContainer> _container;
    private IInteractable<Customer> _customer;
    private IInteractable<PowerUp> _powerUp;
    private IInteractable<TrashCan> _trashCan;
    
    
    private bool CanInteract => _saladMachine != null || _container != null || _customer != null||_trashCan != null || _powerUp != null ||_isWaiting;
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

        else if (other.GetComponent<IInteractable<PowerUp>>() != null)
            _powerUp = other.GetComponent<IInteractable<PowerUp>>();
        
        else if (other.GetComponent<IInteractable<TrashCan>>() != null)
            _trashCan = other.GetComponent<IInteractable<TrashCan>>();
        
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable<SaladMachine>>() != null)
            _saladMachine = null;
        
        else if (other.GetComponent<IInteractable<ThingContainer>>() != null)
            _container = null;
        
        else if (other.GetComponent<IInteractable<Customer>>() != null)
            _customer = null;
        
        else if (other.GetComponent<IInteractable<PowerUp>>() != null)
            _powerUp = null;
        
        else if (other.GetComponent<IInteractable<TrashCan>>() != null)
            _trashCan = null;
        
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
        }
        else if (_saladMachine != null && !_inventory.IsEmpty)
        {
            if(!_saladMachine.AllowsInteraction(this))
                return;
                
            _veg = _inventory.GetTopVegetable();
            

            if(_veg == null)
                return;
            
            _saladMachine.OnInteract(this, _veg, SaladMachineInteractionComplete);
            _input.PauseInput();
        }
        else if (_customer != null && !_inventory.IsEmpty)
        {
            if(!_customer.AllowsInteraction(this))
                return;
            
            _comb = _inventory.GetTopMostCombination();
            
            
            if(_comb == null)
                return;
            
            _customer.OnInteract(this, _comb.CombinationName);
            _inventory.RemoveThing(_comb);
        }
        else if (_powerUp != null)
        {
            switch (_powerUp.OnInteract())
            {
                case PowerupType.AddScore:
                    PlayerScore.OnScoreAdd?.Invoke(this, MetaDataUtility.metaData.powerUpAddScore);
                    break;
                case PowerupType.TimeIncrease:
                    _playerTimer.Addtimer(MetaDataUtility.metaData.powerUpAddTime);
                    break;
                case PowerupType.MoveSpeed:
                    _controller.AddSpeed(MetaDataUtility.metaData.powerUpAddSpeed, MetaDataUtility.metaData.powerUpAddSpeedDuration);
                    break;
            }

            _powerUp = null;
        }
        else if (_trashCan != null)
        {
            _trashCan.OnInteract(this);
            print("Detected");
        }

        StartCoroutine(WaitCoroutine());
    }

    public void RemoveCombination()
    {
        Thing thing = _inventory.GetTopMostCombination();
            
        if(thing != null)
            _inventory.RemoveThing(thing);
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
