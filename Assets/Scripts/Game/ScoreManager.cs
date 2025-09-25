using TMPro;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; 

    public TMP_Text scoreText; 
    private int score = 0;

    private string filePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "score.txt");

        if (File.Exists(filePath))
        {
            string savedScore = File.ReadAllText(filePath);
            int parsedScore;
            if (int.TryParse(savedScore, out parsedScore))
            {
                score = parsedScore;
            }
        }

        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        SaveScore();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void SaveScore()
    {
        File.WriteAllText(filePath, score.ToString());
        Debug.Log("Score saved to " + filePath);
    }
}