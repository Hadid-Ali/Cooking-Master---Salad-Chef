
using System;

public interface IInteractable<T>
{
    bool AllowsInteraction(PlayerInteraction controller);
    void OnInteract(PlayerInteraction controller);
    void OnInteract(PlayerInteraction controller, Vegetable veg, Action Completed);
    void OnInteract(PlayerInteraction controller,CombinationName veg);
    PowerupType OnInteract();
}

