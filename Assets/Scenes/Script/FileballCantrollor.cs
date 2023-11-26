using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileballCantrollor : MonoBehaviour
{
    Rigidbody2D rigidbody2;
    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        

    }
    private void Update()
    {
        flipObject();

    }

    private void flipObject()
    {
        if (rigidbody2 != null)
        {
            // ���������� �̵� ��
            if (rigidbody2.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            // �������� �̵� ��
            else if (rigidbody2.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}

