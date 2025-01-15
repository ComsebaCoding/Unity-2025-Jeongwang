using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject enemyPrefab;
    private Text scoreText;

    float spawnTimer;
    public float respawnCooltime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0.0f;
        // Resources 폴더 내에서 Prefab 폴더의 Enemy 프리팹을 로드해온다
        enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");

        // ScoreText라는 이름의 GameObject를 찾아 Text 컴포넌트를 가져온다
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        if (enemyPrefab == null)
        {
            Debug.Log("Enemy Prefab을 찾을 수 없었습니다.");
        }
        if (scoreText == null)
        {
            Debug.Log("ScoreText 오브젝트의 Text 컴포넌트를 찾을 수 없었습니다.");
        }
    }
    static public int score = 0;
    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 스코어를 갱신하는데, 갱신될때만 UI를 갱신할 수 없을까?
        scoreText.text = "Score : " + score;// score.ToString("D4"); // score 값을 10진수 4자리로 번역
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer > respawnCooltime)
        {
            spawnTimer -= respawnCooltime;
            Instantiate(enemyPrefab, new Vector3(Random.Range(-4.5f, 4.5f), 1.0f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
        }
    }
}