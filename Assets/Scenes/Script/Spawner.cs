using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private MakeRandomMap randomMap;
    public GameObject[] prefabs;
    public GameObject enemyBox; // 에너미박스 오브젝트 설정
    private Tuple<List<Vector2>, List<Vector2>> spawners;
    public GameObject door;
    private List<Vector2> b = new List<Vector2>();
    private int count = 0;
    private int index;
    public GameObject Portal;

    public IEnumerator SpawnEnemiesWithDelay(int index)
    {
        this.index = index;
        int enemyCount = 5 + (5 * count); // 10부터 5씩 증가

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = randomMap.GetRandomSpawnPosition(index);

            // 랜덤 인덱스 생성 조건 수정
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
                    randomIndex = 1; // 50% 확률
                }
                else if (randomValue < 0.75f)
                {
                    randomIndex = 2; // 25% 확률
                }
                else
                {
                    randomIndex = 3; // 25% 확률
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
