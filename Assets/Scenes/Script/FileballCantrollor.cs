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
        {
            // �¿� �̵� ����
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(horizontalInput, 0f);

            // �¿� �̵��� ���� ������ ����
            if (movement.x > 0) // ���������� �̵�
            {
                transform.localScale = new Vector3(1, 1, 1); // ��� ������ ������ ����
            }
            else if (movement.x < 0) // �������� �̵�
            {
                transform.localScale = new Vector3(-1, 1, 1); // ���� ������ ������ ����
            }
        }
    }

}

