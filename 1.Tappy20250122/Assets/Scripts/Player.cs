using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myrigid;
    public float power = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        myrigid = gameObject.GetComponent<Rigidbody2D>();
        if (myrigid == null)
        {
            // Debug.Log("Player.cs : Rigidbody2D is Not Found.");
            Debug.Log("Player.cs���� Player�� ������ Rigidbody2D�� ã�� �� �������ϴ�.");
        }
    }

    public void Up()
    {
        if (myrigid) myrigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        else Debug.Log("Jump�� �õ������� Player�� Rigidbody2D�� �����ϴ�.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.UpArrow)
        //    || Input.GetKeyDown(KeyCode.Mouse0)
        //    || Input.GetKeyDown(KeyCode.Mouse1)
            )
        {
            Up();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit!");
    }
}
