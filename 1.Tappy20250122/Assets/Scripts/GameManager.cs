using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject gameStartUi;
    public GameObject gameOverUi;

    public static int score;
    int highscore;
    public Text ScoreLabel;
    private Text HighScoreLabel;
    float scoreTimer;

    private AudioSource audioSource;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {       
        Time.timeScale = 0.0f;
        gameStartUi = GameObject.Find("GameStartUI");
        if (gameStartUi == null) Debug.Log("GameStartUI is not Found");
        else gameStartUi.SetActive(true);
        // gameOverUi = GameObject.Find("GameOverUI");
        HighScoreLabel = GameObject.Find("HighScoreLabel").GetComponent<Text>();
        if (HighScoreLabel == null) Debug.Log("HighScoreLabel is not Found");
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);
        HighScoreLabel.text = "HIGHSCORE : " + highscore.ToString("D4");
        scoreTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer > 1.0f)
        {
            scoreTimer -= 1.0f;
            ++score;
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        ScoreLabel.text = score.ToString("D4");
    }

    public void GameStart()
    {
        Time.timeScale = 1.0f;
        gameStartUi.SetActive(false);
        audioSource.PlayOneShot(clickSound);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        if (score > highscore)
            PlayerPrefs.SetInt("highscore", score);            
        gameOverUi.SetActive(true);
        audioSource.PlayOneShot(clickSound);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}