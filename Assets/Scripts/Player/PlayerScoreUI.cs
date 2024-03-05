using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string name;
    
    void Start()
    {
        PlayerScore.OnPlayerScoreUpdated += OnScoreUpdated;
        text.SetText($"{name} : \n 0");
    }

    private void OnDestroy()
    {
        PlayerScore.OnPlayerScoreUpdated -= OnScoreUpdated;
    }

    private void OnScoreUpdated(string arg1, int arg2)
    {
        if(arg1 != name)
            return;
        
        text.SetText($"{name} : \n {arg2}");
    }
    
}
