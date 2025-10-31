using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text maxScoreText;
    public int count = 0;
    const string leaderBoardId = "CgkIl9XD1bEWEAIQAQ";
    void Awake()
    {
        PlayGamesPlatform.Activate();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            scoreText.text = "Score: " + count;
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            scoreText.text = "Счёт: " + count;
        }
    }

    public void IncreaseScore()
    {
        count++;
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            scoreText.text = "Score: " + count;
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            scoreText.text = "Счёт: " + count;
        }

        
        if (!PlayerPrefs.HasKey("MaxScore"))
        {
            if (PlayerPrefs.GetInt("Language", 0) == 0)
            {
                maxScoreText.text = "Max score: " + count;
            }
            else if (PlayerPrefs.GetInt("Language", 0) == 1)
            {
                maxScoreText.text = "Рекорд: " + count;
            }
            PlayerPrefs.SetInt("MaxScore", count);
        }
        else
        {
            if (PlayerPrefs.GetInt("MaxScore") < count)
            {
                if (PlayerPrefs.GetInt("Language", 0) == 0)
                {
                    maxScoreText.text = "Max score: " + count;
                }
                else if (PlayerPrefs.GetInt("Language", 0) == 1)
                {
                    maxScoreText.text = "Рекорд: " + count;
                }
                PlayerPrefs.SetInt("MaxScore", count);
            }
        }
        Social.ReportScore(PlayerPrefs.GetInt("MaxScore"), leaderBoardId, (bool success) => { });
    }
    public void OpenLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}
