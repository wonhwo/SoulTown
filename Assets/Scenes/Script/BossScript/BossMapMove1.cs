using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapMove2 : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject Boss;
    [SerializeField]
    private GameObject BossMap;
    [SerializeField]
    private GameObject BossStatus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = new Vector3(-29.013f, -151.4f, 0);
            Boss.SetActive(true);
            BossMap.SetActive(true);
            BossStatus.SetActive(true);
        }
    }
}
