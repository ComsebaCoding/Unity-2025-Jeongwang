// C#�� ��� define�� �ҽ��ڵ��� �� �������� ���ǰ� �����ϴ�!
//#define CHEATMODE // ġƮ����� �̸��� �������Ѵ�.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameOverUi;
    private GameObject canvas;
    public AudioSource audioSource;
    public AudioClip clickSound;

    private Text ScoreLabel;
    private Text HighScoreLabel;
    int score;
    int highScore;

    // Start is called before the first frame update
    void Start()
    {
#if CHEATMODE && UNITY_EDITOR
        Debug.Log("ġƮ��� Ȱ��ȭ");
#elif CHEATMODE
        Debug.Log("�����Ͱ� �ƴѵ� ġƮ��尡 Ȱ��ȭ��!");
        Application.Quit(); // ���ø����̼� ����, ����� ���� ���α׷� ��Ÿ���� ����
#endif
        Time.timeScale = 1.0f;
        // ���ӿ�����Ʈ.���ε带 �̿��� Ȱ��ȭ�� �θ� ������Ʈ Canvas �˻�
        canvas = GameObject.Find("Canvas");
        if (canvas == null) Debug.Log("GameManager.cs/Start() : Canvas is Not Found");

        // Canvas�� �ڽ� ������Ʈ�� Ʈ�������� GameOverUI �˻� (��Ȱ��ȭ �˻� ����)
        // GameOverUI Ʈ�������� ã�Ƴ� �� �� Ʈ�������� ���� ���� ������Ʈ�� ������.
        gameOverUi = canvas.transform.Find("GameOverUI").gameObject;
        if (gameOverUi == null) Debug.Log("GameManager.cs/Start() : GameOverUI is Not Found");

        ScoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
        if (ScoreLabel == null) Debug.Log("GameManager.cs/Start() : ScoreLabel is Not Found");
        HighScoreLabel = GameObject.Find("HighScoreLabel").GetComponent<Text>();
        if (HighScoreLabel == null) Debug.Log("GameManager.cs/Start() : HighScoreLabel is Not Found");
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreLabel.text = highScore.ToString("D4");
    }

    public void GameOver()
    {
        if (score > highScore)
            PlayerPrefs.SetInt("HighScore", score);
        Time.timeScale = 0.0f;
        gameOverUi.SetActive(true);
    }

    public void RestartGame()
    {
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene(0);
    }

    float gameTimer = 0.0f;
    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        if (gameTimer > 1.0f)
        {
            gameTimer -= 1.0f;
#if CHEATMODE
            score += 2;
#else
            ++score;
#endif
            ScoreLabel.text = score.ToString("D4");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
            audioSource.PlayOneShot(clickSound);

        if (Input.GetKeyDown("escape"))   //Input.GetKeyDown(KeyCode.Escape)
        {
            if (score > highScore)
                PlayerPrefs.SetInt("HighScore", score);
            ExitGame();
        }
    
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ���� �÷��� ����
#elif CHEATMODE
        Debug.Log("ġƮ��尡 �����Ͱ� �ƴѵ� �������ϴ�...!");
        Application.Quit(); // ���ø����̼� ����, ����� ���� ���α׷� ��Ÿ���� ����
#else
        Application.Quit(); // ���ø����̼� ����, ����� ���� ���α׷� ��Ÿ���� ����
#endif
    }
}
