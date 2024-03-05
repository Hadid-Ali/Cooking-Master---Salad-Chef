using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _interaction;
    [SerializeField] private TextMeshProUGUI text;
    
    [SerializeField] public string playerName;
    public int score;

    public static Action<PlayerInteraction, int> OnScoreAdd;
    public static Action<PlayerInteraction, int> OnScoreSub;
    public static Action<int> OnScoreSubAll;
    public static Action<int> OnScoreAddAll;

    public static Action<string, int> OnPlayerScoreUpdated;

    private void Awake()
    {
        OnScoreAdd += AddScore;
        OnScoreSub += SubtractScore;
        OnScoreSubAll += SubtractScoreAll;
        OnScoreAddAll += AddScoreAll;
        
        text.text = $"{playerName} : {score}";
    }

    private void OnDestroy()
    {
        OnScoreAdd -= AddScore;
        OnScoreSub -= SubtractScore;
        OnScoreSubAll -= SubtractScoreAll;
        OnScoreAddAll -= AddScoreAll;
    }

    private void AddScore(PlayerInteraction playerInteraction, int _score)
    {
        if(playerInteraction != _interaction)
            return;
        
        AddScoreAll(_score);
    }
    
    private void SubtractScore(PlayerInteraction playerInteraction, int _score)
    {
        if(playerInteraction != _interaction)
            return;
        
        SubtractScoreAll(_score);
    }

    private void AddScoreAll(int _score)
    {
        score += _score;
        text.text = $"{playerName} : {score}";
        
        OnPlayerScoreUpdated?.Invoke(playerName, score);
    }
    private void SubtractScoreAll(int _score)
    {
        score -= _score;
        text.text = $"{playerName} : {score}";
        
        OnPlayerScoreUpdated?.Invoke(playerName, score);
    }
    
}
