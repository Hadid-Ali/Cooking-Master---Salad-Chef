using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button _button;


    private void Awake()
    {
        _button.onClick.AddListener(()=>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        GameManager.OnPlayerWin += OnPlayerWon;
    }

    private void OnPlayerWon(PlayerTimer obj, int highestscore)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        text.SetText($"{obj.playerName} : has won game with : {highestscore} points");
    }
}
