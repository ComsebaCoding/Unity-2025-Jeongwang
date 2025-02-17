#define UNITY_ADS // ����Ƽ ���� ���� �Լ��� ����Ϸ��� �ʿ��� ������
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
        // UNITY_EDITOR_WIN ����Ƽ ������, �׷��� �������
        // UNITY_EDITOR_OSX ����Ƽ ������, �׷��� �ƺϸ�

        // �����Ͱ� �ƴ� ���, Ư�� �÷����� ���� define
        // UNITY_STANDALONE         ��� ��ǻ��
        // UNITY_STANDALONE_WIN     �������� ��ǻ��
        // UNITY_STANDALONE_OSX     �ƺ�
        // UNITY_STANDALONE_LINUX   ������
        // UNITY_IOS                ������
        // UNITY_ANDROID            �ȵ���̵� ��
        // UNITY_PS4                �÷��̽����̼�4
        // UNITY_XBOXONE            �����ڽ�

        // ���������� ������ ���α׷����� ������
        // UNITY_64     64��Ʈ �÷���
#if UNITY_64

#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ���� �÷��� ����
#elif UNITY_STANDALONE_WIN
        Application.Quit(); // ���ø����̼� ���α׷� ����
#endif
    }
}
