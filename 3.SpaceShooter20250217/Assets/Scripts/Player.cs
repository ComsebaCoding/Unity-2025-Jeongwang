using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 direction;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : Ű �Է¿� ���� ���� ����
        direction = Vector2.zero;
        direction.x += Input.GetKey(KeyCode.LeftArrow) ? -1.0f : 0.0f;
        direction.x += Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0.0f;
        direction.y += Input.GetKey(KeyCode.DownArrow) ? -1.0f : 0.0f;
        direction.y += Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0.0f;
        direction.Normalize(); // ����� ���� ������ ũ�⸦ 1�� ���� (�밢��ó��)


        // Vector2.ClampMagnitude(����, ����) // ������ ũ��� ���ڷ� ���� ������ �� ū ���� return 
        // Vector2.Distance(����1, ����2) // �� ���� ������ �Ÿ��� return
        // Vector2.Equals // �� ���ӿ�����Ʈ�� ���� '����'�� �����ϸ� True, �ٸ��� false
        // Vector2.Lerp(����1, ����2, ������) // �������� �������� ����1, ����2�� ����
        // Vector2.Reflect(���⺤��, ��������) // �������� : ���� �ݻ��Ű���� ǥ�鿡 ���� ������ ����
        // ���� ���͸� ���� ���� �������� �ݻ� �������� ������ �ݻ�� ���͸� ��ȯ
        
        // �Է��� ������ ��쿡�� �̵�
        if (direction.sqrMagnitude > 0.1f)
        {
            // ���� ���� = z����� ȸ���� ���Ϸ�����
            float currentAngle = transform.rotation.eulerAngles.z;
            // ���� ������ �ϴ� ���� = Up���Ϳ� direction ������ ����
            // Vector2.SignedAngle(����1, ����2) �Լ��� �� ���� ������ ������ return 
            float targetAngle = Vector2.SignedAngle(Vector2.up, direction);
            
            // ���� �ӵ��� ����
            float curVelocity = 0.0f;
            // ���� ������ �ε巴�� ȸ���� ������ ���ϴ� �Լ�
            currentAngle = Mathf.SmoothDampAngle(
                currentAngle    // ���� ����
                , targetAngle   // ���� ������ �ϴ� Ÿ�� ����
                , ref curVelocity   // ���� �ӵ��� ����ؼ� ������
                , Time.deltaTime    // �������ϰ� ȸ���ϴµ� �ɸ��� �ð�, �����Ӻ��
                , 500); // �ε巴�� ȸ���ϴ� �ְ� �ӵ�

            // Z�� ȸ�� �Լ�
            // Quaternion.Euler() : ���ڷ� ���� ������ ���Ϸ� ������ ��ȯ�ϴ� �Լ�
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentAngle);
        }
        // �÷��̾� ���� �������� �̵�
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
    }
}