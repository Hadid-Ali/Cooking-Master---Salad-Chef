using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    private static Timer _instance; 
    private static List<Timer> timers = new List<Timer>(); 

    [SerializeField] private float timerDuration; 
    [SerializeField] private float timerElapsed; 
    [SerializeField] private bool doubleSpeed = false;
    private Action<float> _onTimerUpdate; 
    private Action _onTimerEnd; 
    private Coroutine _timerCoroutine;
    
    private bool _isTicking;
    

    public static Timer Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject timerObject = new GameObject("Timer");
                _instance = timerObject.AddComponent<Timer>();
            }
            return _instance;
        }
    }

    public Timer StartTimer(Action onTimerEnd, Action<float> onTimerUpdate, float duration)
    {
        Timer newTimer = timers.Find(x => x._isTicking = false);
        
        if (newTimer == null)
        {
            GameObject oobject = new GameObject();
            oobject.transform.SetParent(_instance.gameObject.transform);
            newTimer = oobject.AddComponent<Timer>();
            timers.Add(newTimer);
        }
        
        newTimer.timerDuration = duration;
        newTimer._onTimerUpdate = onTimerUpdate;
        newTimer._onTimerEnd = onTimerEnd;
        newTimer.doubleSpeed = false;
        newTimer._timerCoroutine = newTimer.StartCoroutine(newTimer.TimerCoroutine());
        return newTimer;
    }

    private IEnumerator TimerCoroutine()
    {
        _isTicking = true;
        timerElapsed = timerDuration;
        while (timerElapsed > 0)
        {
            yield return null;

            timerElapsed -=  doubleSpeed? (Time.deltaTime + Time.deltaTime) : Time.deltaTime;
            _onTimerUpdate?.Invoke(timerElapsed);
        }

        _onTimerEnd?.Invoke();
        
        _isTicking = false;
        timers.Remove(this);
        Destroy(this.gameObject);
    }

    public void DoubleTheSpeed()
    {
        doubleSpeed = true;
    }


    public void AddTime(float additionalTime)
    {
        timerElapsed += additionalTime;
        
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }
    }
    
}