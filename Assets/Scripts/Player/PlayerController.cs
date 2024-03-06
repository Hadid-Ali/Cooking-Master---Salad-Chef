using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private CharacterInput input;
    [SerializeField] private PlayerInteraction interactor;
    
    [SerializeField] private float playerSpeed = 2.0f;
    
    private Vector3 _move;
    private float _defaultSpeed;
    
    private void Awake()
    {
        input.Initialize(OnMove, OnInteract);
        _defaultSpeed = playerSpeed;
    }

    void Update()
    {
        if (_move != Vector3.zero) gameObject.transform.forward = _move;
        
        controller.Move(Vector3.down);
        controller.Move(_move * Time.deltaTime * playerSpeed);
    }

    public void OnMove(Vector2 move)
    {
         _move = new Vector3(move.x, 0, move.y);
    }

    public void AddSpeed(float speed, float duration)
    {
        playerSpeed += speed;
        Timer.Instance.StartTimer(ResetSpeed, (float i) => { }, duration);
    }

    public void ResetSpeed()
    {
        playerSpeed = _defaultSpeed;
    }

    public void OnInteract()
    {
        interactor.Interact();
    }
}

