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
    private void Awake()
    {
        
    }
    private void Update()
    {
        flipObject();

    }

    private void flipObject()
    {
        if (rigidbody2 != null)
        {
            // 오른쪽으로 이동 중
            if (rigidbody2.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            // 왼쪽으로 이동 중
            else if (rigidbody2.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

}

