using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߻� Ŭ����, ���� ������Ʈ�� ����� �� ����
// �ݵ�� ������Ʈ�� �θ�� ��ӹ޾Ƽ� ����� ��
abstract public class Enemy : MonoBehaviour
{
    public int hp;
    // virtual Ű����� ���� �Լ��� ����� Ű�����̴�.
    // ���� �Լ��� ���� �ڽĿ��� ��� ������ �� �ڽ��� ���� �����ؼ� ����� ������ �� �ִ�.
    // Update is called once per frame
    virtual protected void Update()
    {
        // �ű״�Ʃ�� : ������Ʈ�� ȭ�� �߽����κ��� ������ �Ÿ�
        if (transform.position.magnitude > 15.0f)
        {
            // ȭ�� �߽����κ��� 15���� �־����� ���� ������Ʈ�� ����.
            Destroy(gameObject);
            // ������Ʈ Ǯ�� ������ �� ��Ʈ���̸� ������ ����
        }
    }

    // ������ ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        --hp;
        if (hp <= 0)
        {
            OnDead(); // �߻� �Լ��̹Ƿ� �ڽ��� �������༭ �ڽ� ���� �ٸ� Dead Action �ߵ�!
            Destroy(gameObject);
        }
    }

    // �߻� �Լ�, �ڽ� Ŭ������ �����������
    abstract protected void OnDead(); 
}
