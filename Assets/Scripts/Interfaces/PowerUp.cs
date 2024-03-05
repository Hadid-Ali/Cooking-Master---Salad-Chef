using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUp : MonoBehaviour,IInteractable<PowerUp>
{
    public PowerupType type;


    public bool AllowsInteraction(PlayerInteraction controller)
    {
        return true;
    }

    public PowerupType OnInteract()
    {
        GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
        
        return type;
    }

    public void OnInteract(PlayerInteraction controller)
    {
        
    }

    public void OnInteract(PlayerInteraction controller, Vegetable veg, Action Completed)
    {
    }

    public void OnInteract(PlayerInteraction controller, CombinationName veg)
    {
        
    }
}
