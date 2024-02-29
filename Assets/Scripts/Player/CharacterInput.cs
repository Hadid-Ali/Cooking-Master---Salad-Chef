using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private event Action<Vector2> OnMoveE;
    private event Action OnInteractE;
    public InputBinding map;

    private void Awake()
    {


    }

    public void Initialize(Action<Vector2> OnMove, Action OnInteract)
    {
        OnMoveE += OnMove ;
        OnInteractE += OnInteract;
    }

    public void UnInitialize()
    {
        OnMoveE = null;
        OnInteractE = null;
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        OnMoveE?.Invoke(move);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.action.triggered)
            OnInteractE?.Invoke();
    }
}
