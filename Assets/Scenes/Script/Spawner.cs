using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private MakeRandomMap randomMap;
    public GameObject[] prefabs;
    public GameObject enemyBox; // 에너미박스 오브젝트 설정
    private GameObject select = null;
    private Tuple<List<Vector2>, List<Vector2>> spawners;
    private List<Vector2> aList;
    private List<Vector2> bList;
    public GameObject door;
    private int enemy = 0;

     int isstart=0;

    public IEnumerator SpawnEnemiesWithDelay(int index)
    {
        for (int spawnCount = 0; spawnCount < 3; spawnCount++)
        {
            int enemyCount = UnityEngine.Random.Range(3, 10);

            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 spawnPosition = randomMap.GetRandomSpawnPosition(index);

                int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);

                GameObject newEnemy = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);

                newEnemy.transform.parent = enemyBox.transform;
                if(isstart>0)
                    yield return new WaitForSeconds(5.0f);
            }
            isstart++;
        }
        isstart = 0;
    }
}