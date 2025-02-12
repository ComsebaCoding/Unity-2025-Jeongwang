// C#의 모든 define은 소스코드의 맨 위에서만 정의가 가능하다!
//#define CHEATMODE // 치트모드라는 이름을 디파인한다.
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
        Debug.Log("치트모드 활성화");
#elif CHEATMODE
        Debug.Log("에디터가 아닌데 치트모드가 활성화됨!");
        Application.Quit(); // 어플리케이션 종료, 빌드된 게임 프로그램 런타임을 말함
#endif
        Time.timeScale = 1.0f;
        // 게임오브젝트.파인드를 이용해 활성화된 부모 오브젝트 Canvas 검색
        canvas = GameObject.Find("Canvas");
        if (canvas == null) Debug.Log("GameManager.cs/Start() : Canvas is Not Found");

        // Canvas의 자식 오브젝트의 트랜스폼인 GameOverUI 검색 (비활성화 검색 가능)
        // GameOverUI 트랜스폼을 찾아낸 뒤 그 트랜스폼이 속한 게임 오브젝트를 가져옴.
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
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 게임 플레이 정지
#elif CHEATMODE
        Debug.Log("치트모드가 에디터가 아닌데 켜졌습니다...!");
        Application.Quit(); // 어플리케이션 종료, 빌드된 게임 프로그램 런타임을 말함
#else
        Application.Quit(); // 어플리케이션 종료, 빌드된 게임 프로그램 런타임을 말함
#endif
    }
}
