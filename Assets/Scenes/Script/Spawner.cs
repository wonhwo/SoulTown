using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private MakeRandomMap randomMap;
    public GameObject[] prefabs;
    public GameObject enemyBox; // ���ʹ̹ڽ� ������Ʈ ����
    private Tuple<List<Vector2>, List<Vector2>> spawners;
    public GameObject door;
    private List<Vector2> b = new List<Vector2>();
    private int count = 0;
    private int index;
    public GameObject Portal;

    public IEnumerator SpawnEnemiesWithDelay(int index)
    {
        this.index = index;
        int enemyCount = 5 + (5 * count); // 10���� 5�� ����

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = randomMap.GetRandomSpawnPosition(index);

            // ���� �ε��� ���� ���� ����
            int randomIndex;
            if (count<=1)
            {
                randomIndex = UnityEngine.Random.Range(0, 2);
            }
            else if (count <= 2)
            {
                randomIndex = UnityEngine.Random.Range(0, 3);
            }
            else
            {
                float randomValue = UnityEngine.Random.value;

                if (randomValue < 0.5f)
                {
                    randomIndex = 1; // 50% Ȯ��
                }
                else if (randomValue < 0.75f)
                {
                    randomIndex = 2; // 25% Ȯ��
                }
                else
                {
                    randomIndex = 3; // 25% Ȯ��
                }
            }

            GameObject newEnemy = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyBox.transform;
            yield return new WaitForSeconds(1f);
        }
        count++;
    }

    public void getBoxPoint()
    {
        this.b = randomMap.b;
        if (count == b.Count && b != null)
        {
            GameObject spawnedPrefab = Instantiate(Portal, b[index], Quaternion.identity);
            spawnedPrefab.SetActive(true);
            count++;
        }
    }
}
