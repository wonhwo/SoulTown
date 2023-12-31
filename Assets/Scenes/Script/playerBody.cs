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
       //if (isHurt) return;
        
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(player.HurtDelay(1.2f,20));
        }
        if ( collision.CompareTag("BossAttack1"))
        {
            StartCoroutine(player.HurtDelay(1.2f, 35));
        }
        if (collision.CompareTag("BossAttack2"))
        {
            StartCoroutine(player.HurtDelay(1.2f, 50));
        }
        if (collision.CompareTag("BossAttack3"))
        {
            StartCoroutine(player.HurtDelay(1.2f, 20));
        }
        if (collision.CompareTag("Energy"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("FireBall"))
        {
            StartCoroutine(player.HurtDelay(1.2f, 20));
            StartCoroutine(player.LowerSpeedForDuration());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("FinalAttack"))
        {
            StartCoroutine(player.HurtDelay(1.2f, 100));
        }
        if (collision.CompareTag("potion1"))
        {
            player.healHP(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("potion2"))
        {
            player.healHP(2);
            Destroy(collision.gameObject);
        }
    }
}
