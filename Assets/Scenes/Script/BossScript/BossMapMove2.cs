using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapMove1 : MonoBehaviour
{
    private GameObject guide;
    private GameObject player;
    private void Start()
    {
        guide= GameObject.Find("asdf");
        player = GameObject.Find("player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = new Vector3(-33.87f, -191.54f,0);

        }
    }
}
