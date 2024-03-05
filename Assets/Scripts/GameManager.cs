using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerTimer[] players;

    private Dictionary<PlayerTimer, int> playerScores = new Dictionary<PlayerTimer, int>();

    public static Action<PlayerTimer, int> OnPlayerWin;
    
    private int highestscore;
    private void Awake()
    {
        PlayerTimer.OnPlayerTimerComplete += OnReceivePlayerData;
        highestscore = 0;
    }

    private void OnDestroy()
    {
        PlayerTimer.OnPlayerTimerComplete -= OnReceivePlayerData;
    }

    private void OnReceivePlayerData(PlayerTimer playerTimer, int score)
    {
        if(!playerScores.ContainsKey(playerTimer))
            playerScores.Add(playerTimer, score);

        if (playerScores.Count >= 2)
        {
            OnPlayerWin?.Invoke(CalculateWin(),highestscore);
        }
        
    }

    private PlayerTimer CalculateWin()
    {
        highestscore = -1000;
        PlayerTimer winner = null;
        foreach (var v in playerScores)
        {
            if (v.Value > highestscore)
            {
                highestscore = v.Value; 
                winner = v.Key;
            }
        }

        return winner;

    }
}
