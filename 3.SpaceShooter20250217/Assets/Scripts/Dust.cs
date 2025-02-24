using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Random.insideUnitCircle.normalized;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime;
    }
    
    // ������Ʈ Ǯ�� ���� �Ŀ��� �� �� �Լ�
    public void DestroySelf()
    {
        Destroy(gameObject); 
    }
}
