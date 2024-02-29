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
        float startTime = Time.time;
        
        
        while (true)
        {
            float elapsedTime = Time.time - startTime;
            repeatedAction?.Invoke(elapsedTime);
            yield return new WaitForSeconds(0.1f);

            if (elapsedTime >= delay)
            {
                delayedAction?.Invoke();
                break;
            }
        }
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