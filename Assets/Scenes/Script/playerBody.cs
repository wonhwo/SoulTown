using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBody : MonoBehaviour
{
    [SerializeField]
    private player player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(player.HurtDelay(1.0f));
        }
    }
}
