using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    private static Timer _instance; 
    private static List<Timer> timers = new List<Timer>(); 

    private float timerDuration; 
    private float timerElapsed; 
    private Action<float> onTimerUpdate; 
    private Action onTimerEnd; 
    private Coroutine timerCoroutine;

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
        newTimer.onTimerUpdate = onTimerUpdate;
        newTimer.onTimerEnd = onTimerEnd;
        newTimer.timerCoroutine = newTimer.StartCoroutine(newTimer.TimerCoroutine());
        return newTimer;
    }

    private IEnumerator TimerCoroutine()
    {
        _isTicking = true;
        timerElapsed = timerDuration;
        while (timerElapsed > 0)
        {
            yield return null;
            timerElapsed -= Time.deltaTime;
            onTimerUpdate?.Invoke(timerElapsed);
        }

        onTimerEnd?.Invoke();
        
        _isTicking = false;
        timers.Remove(this);
        Destroy(this.gameObject);
    }


    public void AddTime(float additionalTime)
    {
        timerElapsed += additionalTime;
        
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = StartCoroutine(TimerCoroutine());
        }
    }
    
}