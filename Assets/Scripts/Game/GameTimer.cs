using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 30f;
    private float timeRemaining;

    public TMP_Text timerText;

    void Start()
    {
        timeRemaining = gameDuration;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0) { timeRemaining = 0; }

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
        }

        // If time runs out, end the game
        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Game Over");  // Placeholder for now.
    }
}
