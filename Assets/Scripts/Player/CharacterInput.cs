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

    private bool _isInputPaused;
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
        if(_isInputPaused)
            return;
        
        Vector2 move = context.ReadValue<Vector2>();
        OnMoveE?.Invoke(move);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(_isInputPaused)
            return;
        
        if(context.action.triggered)
            OnInteractE?.Invoke();
    }

    public void PauseInput()
    {
        _isInputPaused = true;
        OnMoveE?.Invoke(new Vector2(0,0));
    }

    public void ContinueInput() => _isInputPaused = false;
}
