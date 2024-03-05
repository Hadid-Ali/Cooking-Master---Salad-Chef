using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController m_Controller;
    [SerializeField] private CharacterInput m_Input;
    [SerializeField] private PlayerInteraction m_Intereactor;
    
    [SerializeField] private float playerSpeed = 2.0f;
    
    private Vector3 m_move;
    private float defaultSpeed;
    
    private void Awake()
    {
        m_Input.Initialize(OnMove, OnInteract);
        defaultSpeed = playerSpeed;
    }

    void Update()
    {
        if (m_move != Vector3.zero) gameObject.transform.forward = m_move;
        
        m_Controller.Move(Vector3.down);
        m_Controller.Move(m_move * Time.deltaTime * playerSpeed);
    }

    public void OnMove(Vector2 move)
    {
         m_move = new Vector3(move.x, 0, move.y);
    }

    public void AddSpeed(float speed, float duration)
    {
        playerSpeed += speed;
        Timer.Instance.StartTimer(ResetSpeed, (float i) => { }, duration);
    }

    public void ResetSpeed()
    {
        playerSpeed = defaultSpeed;
    }

    public void OnInteract()
    {
        m_Intereactor.Interact();
    }
}

