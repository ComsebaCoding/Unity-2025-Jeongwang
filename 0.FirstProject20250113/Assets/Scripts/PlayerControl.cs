using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Scene�� ���� ���۵� �� 1�� ����Ǵ� �Լ�
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x += speed * Time.deltaTime;
        }
        transform.position += move;
    }
}