using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierContraller : MonoBehaviour
{

    [SerializeField]
    private BossBody body;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("barrierBraek"))
        {
            StartCoroutine(body.BreakBarrier());
            Destroy(collision.gameObject);
        }
    }
}
