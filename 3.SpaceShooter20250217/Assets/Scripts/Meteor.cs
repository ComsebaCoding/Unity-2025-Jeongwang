using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemy // �������̺��� ���ʹ̰� �̹� ��ӹ޾���
{
    private Rigidbody2D myrigid;
    public Vector3 direction = Vector3.zero;    // ���׿��� ������ ����
    public float speed = 3.0f;                  // ���׿��� �ӷ�

    private GameObject dustPrefab;
    // Start is called before the first frame update
    void Start()
    {
        myrigid = GetComponent<Rigidbody2D>();
        if (myrigid == null) Debug.Log("Meteor.cs/Start() : Rigidbody2D is Not Found");
        else myrigid.angularVelocity = Random.Range(120.0f, 360.0f);

        // direction ���Ͱ� �������� ���
        // ���׿� �������� �÷��̾��� ������ direction ������ ����
        if (direction == Vector3.zero)
        {
            // ���� ���� ����
            // ���� ������ ���� = ���� ���� ���� ��ġ - ���� ���� ��ġ
            // ���Ϳ��� A->B ���⺤�� : (B-A).��ֶ����� 
            Vector3 delta = GameManager.instance.GetPlayerPos() - transform.position;
            direction = delta.normalized;
        }

        dustPrefab = Resources.Load<GameObject>("Prefabs/Dust");
        if (dustPrefab == null) Debug.Log("Meteor.cs/Start() : DustPrefab is Not Found");
    }
    
    // override Ű����� ���� �θ��� �Լ��� ��ӹ޾Ƽ� ������ �ߴٴ� ��
    // Update is called once per frame
    override protected void Update()
    {
        // Enemy �� Update�� ���� ����
        base.Update(); // �θ� Ŭ������ Update �Լ�

        // direction �������� �̵�
        transform.position += direction * speed * Time.deltaTime;
    }
 
    protected override void OnDead()
    {
        // TODO : Dust ����
        // ������Ʈ Ǯ�� ���� �� �̹� �ִ� ���������� �˻� ��, ���� ���� ����, ������ �׳� ���پ�
        for (int i = 0; i < 3; ++i)
            Instantiate(dustPrefab, transform.position, Quaternion.identity);
    }


}
