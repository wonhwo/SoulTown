using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerhyGenerator : MonoBehaviour
{
    public GameObject prefabToSpawn; // 소환할 프리팹
    public GameObject[] spawners; // 스포너들의 배열
    bool monsterCount1 = false;
    bool monsterCount2 = false;
    bool monsterCount3 = false;
    bool monsterCount4 = false;
    private void Update()
    {
        SpanerChildCount();
        if (monsterCount1 && monsterCount2 && monsterCount3 && monsterCount4)
        {
            SpawnPrefab(transform);
            trueMonsterCount();
        }
    }
    void SpanerChildCount()
    {
        if (spawners[0].transform.childCount != 0)
        {
            monsterCount1 = true;
        }
        if (spawners[1].transform.childCount != 0)
        {
            monsterCount2 = true;
        }

        if (spawners[2].transform.childCount != 0)
        {
            monsterCount3 = true;
        }

        if (spawners[3].transform.childCount != 0)
        {
            monsterCount4 = true;
        }
    }
    void trueMonsterCount()
    {
        monsterCount1 = false;
        monsterCount2 = false;
        monsterCount3 = false;
        monsterCount4 = false;
    }

    float lastSpawnTime; // 마지막으로 스폰된 시간을 저장하는 변수
    float delay = 5f; // 딜레이 시간 (초)

    void SpawnPrefab(Transform spawnPoint)
    {
        // 초기에는 lastSpawnTime을 설정하여 최초 스폰이 가능하도록 함
        if (lastSpawnTime == 0f || Time.time - lastSpawnTime >= delay)
        {
            if (transform.childCount == 0)
            {
                Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, gameObject.transform);

                // 스폰된 시간 갱신
                lastSpawnTime = Time.time;
            }
        }
    }
}