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
            // 좌우 이동 감지
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(horizontalInput, 0f);

            // 좌우 이동에 따라 스케일 조절
            if (movement.x > 0) // 오른쪽으로 이동
            {
                transform.localScale = new Vector3(1, 1, 1); // 양수 값으로 스케일 조절
            }
            else if (movement.x < 0) // 왼쪽으로 이동
            {
                transform.localScale = new Vector3(-1, 1, 1); // 음수 값으로 스케일 조절
            }
        }
    }

}

