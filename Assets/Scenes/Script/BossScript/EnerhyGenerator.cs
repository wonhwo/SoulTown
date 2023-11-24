using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerhyGenerator : MonoBehaviour
{
    public GameObject prefabToSpawn; // ��ȯ�� ������
    public GameObject[] spawners; // �����ʵ��� �迭
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

    float lastSpawnTime; // ���������� ������ �ð��� �����ϴ� ����
    float delay = 5f; // ������ �ð� (��)

    void SpawnPrefab(Transform spawnPoint)
    {
        // �ʱ⿡�� lastSpawnTime�� �����Ͽ� ���� ������ �����ϵ��� ��
        if (lastSpawnTime == 0f || Time.time - lastSpawnTime >= delay)
        {
            if (transform.childCount == 0)
            {
                Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, gameObject.transform);

                // ������ �ð� ����
                lastSpawnTime = Time.time;
            }
        }
    }
}