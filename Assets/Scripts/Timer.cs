using UnityEngine;
using System;
using System.Collections;

public static class Timer
{
    public static void CreateTimerObject(Action delayedAction, Action<float> repeatedAction, float delay)
    {
        CoroutineRunner.Instance.StartCoroutine(Execute(delayedAction, repeatedAction, delay));
        
    }

    private static IEnumerator Execute(Action delayedAction, Action<float> repeatedAction, float delay)
    {
        float timeLeft = delay;
        float lastUpdateTime = timeLeft;

        while (timeLeft > 0f)
        {
            yield return null; 
            
            timeLeft -= Time.deltaTime;
            
            if (Mathf.Abs(timeLeft - lastUpdateTime) > 0.1f)// To reduce number of callbacks
            {
                repeatedAction?.Invoke(timeLeft);
                lastUpdateTime = timeLeft;
            }
            repeatedAction?.Invoke(timeLeft);
        }
        delayedAction?.Invoke();   

    }
    

    private class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;
        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }
    }
}