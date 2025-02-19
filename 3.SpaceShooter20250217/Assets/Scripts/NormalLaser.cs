using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLaser : MonoBehaviour
{
    public float LaserSpeed = 5.0f;
    public int AttackDamage = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * LaserSpeed * Time.deltaTime, Space.Self);
        // �ű״�Ʃ�� : ������Ʈ�� ȭ�� �߽����κ��� ������ �Ÿ�
        if (transform.position.magnitude > 15.0f)
        {
            // ȭ�� �߽����κ��� 15���� �־����� ���� ������Ʈ�� ����.
            Destroy(gameObject);
            // ������Ʈ Ǯ�� ������ �� ��Ʈ���̸� ������ ����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); // ���߿� ������Ʈ Ǯ������ ����
    }
}
