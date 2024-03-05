using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour,IInteractable<TrashCan>
{
    
    public bool AllowsInteraction(PlayerInteraction controller)
    {
        return true;
    }

    public void OnInteract(PlayerInteraction controller)
    {
        controller.RemoveCombination();
    }

    public void OnInteract(PlayerInteraction controller, Vegetable veg, Action Completed)
    {
        throw new NotImplementedException();
    }

    public void OnInteract(PlayerInteraction controller, CombinationName veg)
    {
        throw new NotImplementedException();
    }

    public PowerupType OnInteract()
    {
        throw new NotImplementedException();
    }
}
