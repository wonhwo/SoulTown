using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBody : MonoBehaviour
{
    [SerializeField]
    private player player;
    private bool isHurt;
    // Start is called before the first frame update
    private void Update()
    {
        isHurt = player.isHurt;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHurt) return;
        
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(player.HurtDelay(1.0f));
            // 넉백을 주고자 하는 방향 계산
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // 넉백을 가하는 함수 호출
            player.ApplyKnockback(knockbackDirection);
        }
    }
}
