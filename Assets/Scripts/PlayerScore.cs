using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _interaction;
    [SerializeField] private TextMeshProUGUI text;
    public string playerName;
    public int score;

    public static Action<PlayerInteraction, int> OnScoreAdd;
    public static Action<PlayerInteraction, int> OnScoreSub;

    private void Awake()
    {
        OnScoreAdd += AddScore;
        OnScoreSub += SubtractScore;
        
        text.text = $"{playerName} : {score}";
    }

    public void AddScore(PlayerInteraction playerInteraction,int _score)
    {
        if(playerInteraction != _interaction)
            return;
        
        score += _score;
        text.text = $"{playerName} : {score}";
    }
    
    public void SubtractScore(PlayerInteraction playerInteraction, int _score)
    {
        if(playerInteraction != _interaction)
            return;
        
        score -= _score;
        text.text = $"{playerName} : {score}";
    }
}
