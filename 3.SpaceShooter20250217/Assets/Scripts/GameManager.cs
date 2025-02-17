#define UNITY_ADS // 유니티 광고 관련 함수를 사용하려면 필요한 디파인
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.Log("GameManager.cs/Start() : Player is Not Found");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) ExitGame();
    }

    private void ExitGame()
    {
        // UNITY_EDITOR_WIN 유니티 에디터, 그런데 윈도우즈만
        // UNITY_EDITOR_OSX 유니티 에디터, 그런데 맥북만

        // 에디터가 아닌 경우, 특정 플랫폼일 때의 define
        // UNITY_STANDALONE         모든 컴퓨터
        // UNITY_STANDALONE_WIN     윈도우즈 컴퓨터
        // UNITY_STANDALONE_OSX     맥북
        // UNITY_STANDALONE_LINUX   리눅스
        // UNITY_IOS                아이폰
        // UNITY_ANDROID            안드로이드 폰
        // UNITY_PS4                플레이스테이션4
        // UNITY_XBOXONE            지존박스

        // 에디터인지 빌드한 프로그램인지 무관함
        // UNITY_64     64비트 플랫폼
#if UNITY_64

#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 게임 플레이 정지
#elif UNITY_STANDALONE_WIN
        Application.Quit(); // 어플리케이션 프로그램 종료
#endif
    }
}
