using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    [SerializeField] private Image timeBar;
    
    [SerializeField] private CharacterInput input;
    [SerializeField] private PlayerScore playerScore;

    public static Action<PlayerTimer, int> OnPlayerTimerComplete;
    private Timer _currentTimer;

    public string playerName;
    private void Start()
    {
        _currentTimer = Timer.Instance.StartTimer(OnTimerEnd, OnTick, MetaDataUtility.MetaData.playerLiveTime);
        playerName = playerScore.playerName;
    }

    private void OnTick(float obj)
    {
        timeBar.fillAmount = obj / MetaDataUtility.MetaData.playerLiveTime;
    }

    private void OnTimerEnd()
    {
        input.PauseInput();
        OnPlayerTimerComplete?.Invoke(this, playerScore.score);
    }

    public void Addtimer(float time)
    {
        _currentTimer.AddTime(time);
    }
}
