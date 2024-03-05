using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    [SerializeField] private Image TimeBar;
    
    [SerializeField] private CharacterInput _input;
    [SerializeField] private PlayerScore _playerScore;

    public static Action<PlayerTimer, int> OnPlayerTimerComplete;
    private Timer _currentTimer;

    public string playerName;
    private void Start()
    {
        _currentTimer = Timer.Instance.StartTimer(OnTimerEnd, OnTick, MetaDataUtility.metaData.playerLiveTime);
        playerName = _playerScore.playerName;
    }

    private void OnTick(float obj)
    {
        TimeBar.fillAmount = obj / MetaDataUtility.metaData.playerLiveTime;
    }

    private void OnTimerEnd()
    {
        _input.PauseInput();
        OnPlayerTimerComplete?.Invoke(this, _playerScore.score);
    }

    public void Addtimer(float time)
    {
        _currentTimer.AddTime(time);
    }
}
