using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    
    private IInteractable<SaladMachine> _currentObjectS;
    private IInteractable<Container> _currentObjectC;
    
    
    [SerializeField] private bool _canInteract;

    private bool _isWaiting;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable<SaladMachine>>() != null)
        {
            _currentObjectS = other.GetComponent<IInteractable<SaladMachine>>();
            _canInteract = true;
        }
        else if (other.GetComponent<IInteractable<Container>>() != null)
        {
            _currentObjectC = other.GetComponent<IInteractable<Container>>();
            _canInteract = true;
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable<SaladMachine>>() != null)
        {
            _currentObjectS = null;
            _canInteract = false;
        }
        else if (other.GetComponent<IInteractable<Container>>() != null)
        {
            _currentObjectC = null;
            _canInteract = false;
        }
    }

    public void Interact()
    {
        if (!_canInteract || _isWaiting)
            return;

        if (_currentObjectC != null && !_inventory.IsAtCapacity)
        {
            _currentObjectC.OnInteract(this);
            print("Container interaction by player");
        }
        else if (_currentObjectS != null)
        {
            _currentObjectS.OnInteract(_inventory.GetTopVegetable());
            print("Salad machine interaction by player");
        }
        
        
        StartCoroutine(WaitCoroutine());
        
    }

    IEnumerator WaitCoroutine()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(.5f);
        _isWaiting = false;
    }
}
