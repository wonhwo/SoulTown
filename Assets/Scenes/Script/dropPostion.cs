using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropPostion : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    public float spawnProbability = 0.10f; // 생성 확률 (5%)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {

            if (Random.value <= spawnProbability && !gameObject.tag.Equals("barrierBraek"))
            {
                // 파괴되면 5%의 확률로 새로운 프리팹을 현재 위치에 생성
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            }
        }
    }
}
