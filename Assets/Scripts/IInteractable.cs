
public interface IInteractable<T>
{
    
    void OnInteract();
    void OnInteract(PlayerInteraction controller);
    void OnInteract(Vegetable veg);
}
