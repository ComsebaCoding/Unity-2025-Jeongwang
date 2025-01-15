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
        // Resources ���� ������ Prefab ������ Enemy �������� �ε��ؿ´�
        enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");

        // ScoreText��� �̸��� GameObject�� ã�� Text ������Ʈ�� �����´�
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        if (enemyPrefab == null)
        {
            Debug.Log("Enemy Prefab�� ã�� �� �������ϴ�.");
        }
        if (scoreText == null)
        {
            Debug.Log("ScoreText ������Ʈ�� Text ������Ʈ�� ã�� �� �������ϴ�.");
        }
    }
    static public int score = 0;
    // Update is called once per frame
    void Update()
    {
        // �� �����Ӹ��� ���ھ �����ϴµ�, ���ŵɶ��� UI�� ������ �� ������?
        scoreText.text = "Score : " + score;// score.ToString("D4"); // score ���� 10���� 4�ڸ��� ����
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer > respawnCooltime)
        {
            spawnTimer -= respawnCooltime;
            Instantiate(enemyPrefab, new Vector3(Random.Range(-4.5f, 4.5f), 1.0f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
        }
    }
}