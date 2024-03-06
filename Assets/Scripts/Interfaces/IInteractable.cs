using System;

public interface IInteractable<T>
{
    bool AllowsInteraction(PlayerInteraction controller);
    void OnInteract(PlayerInteraction controller);
    void OnInteract(PlayerInteraction controller, Vegetable veg, Action completed);
    void OnInteract(PlayerInteraction controller,RecipeName veg);
    PowerupType OnInteract();
}

