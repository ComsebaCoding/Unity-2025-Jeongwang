using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameOverUi;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        gameOverUi = GameObject.Find("GameOverUI");
        if (gameOverUi == null) Debug.Log("GameManager.cs/Start() : GameOverUI is Not Found");
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOverUi.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
