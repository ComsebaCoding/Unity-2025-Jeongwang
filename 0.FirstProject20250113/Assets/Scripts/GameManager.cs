using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject enemyPrefab;
    private Text scoreLabel;
    private GameObject GameOverTextLabel;

    float spawnTimer;
    public float respawnCooltime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        spawnTimer = 0.0f;
        // Resources 폴더 내에서 Prefab 폴더의 Enemy 프리팹을 로드해온다
        enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");
        if (enemyPrefab == null)
        {
            Debug.Log("Enemy Prefab을 찾을 수 없었습니다.");
        }
        // ScoreText라는 이름의 GameObject를 찾아 Text 컴포넌트를 가져온다
        scoreLabel = GameObject.Find("ScoreText").GetComponent<Text>();      
        if (scoreLabel == null)
        {
            Debug.Log("ScoreText 오브젝트의 Text 컴포넌트를 찾을 수 없었습니다.");
        }
        /*
        오브젝트나 트랜스폼을 검색하는 함수들은 어지간하면
        Awake나 Start에서 전부 처리하고, Update나 충돌 시점에서는 하지 말자.
          
        GameObject.Find("이름") 함수는 
        Scene의 '켜져 있는(SetActive True)' "이름" 오브젝트를 찾아온다.
        체크가 꺼져있는 오브젝트는 찾아올 수 없다.

        GameObject.FindWithTag("태그") 
        이 태그인 오브젝트 중 하나를 찾아온다.
        마찬가지로 꺼져있는 오브젝트는 찾을 수 없다.

        GameObject.FindObjectsWithTag("태그") 
        이 태그인 오브젝트를 전부 찾아오고, [] 형태로 return한다.
        꺼져 있으면 찾을 수 없다.

        Transform.Find("이름") 함수는
        이 게임오브젝트의 자식 오브젝트 중 "이름" 오브젝트의 트랜스폼을 찾아온다.
        꺼져 있어도 찾아올 수 있다.
        */
        GameOverTextLabel = 
            GameObject.Find("GameOverObject")   // 켜져 있는 게임오브젝트를 "이름"으로 찾아온다.
            .transform.Find("GameOverText")     // 그 게임오브젝트의 자식 오브젝트의 트랜스폼을 "이름"으로 찾아온다.
            .gameObject;                        // 찾아낸 오브젝트의 트랜스폼의 원본 게임오브젝트를 가져온다.
        if (GameOverTextLabel == null)
        {
            Debug.Log("GameOverText 오브젝트를 찾을 수 없었습니다.");
        }
        else
        {
            GameOverTextLabel.SetActive(false);
        }
    }
    static public int score = 0;
    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 스코어를 갱신하는데, 갱신될때만 UI를 갱신할 수 없을까?
        scoreLabel.text = "Score : " + score.ToString("D4"); // score 값을 10진수 4자리로 번역
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer > respawnCooltime)
        {
            spawnTimer -= respawnCooltime;
            Instantiate(enemyPrefab, new Vector3(Random.Range(-4.5f, 4.5f), 1.0f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
        }
    }

    public void GameOver()
    {
        GameOverTextLabel.SetActive(true);
        Time.timeScale = 0.0f;
        // Thread.Sleep(2500);
        // 인보크 함수는 내가 원하는 시간 이후에 특정 함수를 호출한다.
        // 그런데, 이 시간이 지나고 난 다음 Update때 호출된다.
        // 따라서, timeScale이 0이면 업데이트가 안나오니까 호출이 안 된다.
        // Invoke("Restart", 2.5f);
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        Debug.Log("리스타트 코루틴 시작");
        yield return new WaitForSecondsRealtime(2.5f);
        // 현실 시간 기준 2.5초가 지났음
        // RealTime 함수는 Update와 별개로 동작하기 때문에 Timescale 0배속을 우회가능
        Debug.Log("리스타트 코루틴 2.5초 대기 완료");
        SceneManager.LoadScene(0);
    }
}