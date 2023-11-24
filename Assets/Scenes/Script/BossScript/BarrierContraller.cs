using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierContraller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("barrierBraek"))
        {
            Destroy(collision.gameObject);
        }
    }
}
