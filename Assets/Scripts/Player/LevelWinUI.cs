using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelWinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;


    private void Awake()
    {
        button.onClick.AddListener(()=>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        GameManager.OnPlayerWin += OnPlayerWon;
    }

    private void OnPlayerWon(PlayerTimer obj, int highestScore)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        text.SetText($"{obj.playerName} : has won game with : {highestScore} points");
    }
}
