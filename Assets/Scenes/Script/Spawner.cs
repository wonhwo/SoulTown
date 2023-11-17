using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public EnemyData[] enemyDataArray;
    [SerializeField]
    private MakeRandomMap randomMap;
    public GameObject[] prefabs;
    public GameObject enemyBox; // ���ʹ̹ڽ� ������Ʈ ����
    private GameObject select = null;
    private Tuple<List<Vector2>, List<Vector2>> spawners;
    private List<Vector2> aList;
    private List<Vector2> bList;
    public GameObject door;
    private int enemy = 0;


    public void Return_RandomPosition(int index)
    {
        int enemyCount = UnityEngine.Random.Range(3, 10);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = randomMap.GetRandomSpawnPosition(index);

            int randomIndex = UnityEngine.Random.Range(0, enemyDataArray.Length);
            int randomIndex2 = UnityEngine.Random.Range(0, prefabs.Length);
            EnemyData enemyData = enemyDataArray[randomIndex];

            GameObject newEnemy = Instantiate(prefabs[randomIndex2], spawnPosition, Quaternion.identity);
            EnemyStats enemyStats = newEnemy.GetComponent<EnemyStats>();

            // �̵� �ӵ� �� ü�� ���� ����
            if ( enemyData != null)
            {
                enemyStats.moveSpeed = enemyData.moveSpeed;
                enemyStats.health = enemyData.health;
            }

            newEnemy.transform.parent = enemyBox.transform;
        }
    }
}