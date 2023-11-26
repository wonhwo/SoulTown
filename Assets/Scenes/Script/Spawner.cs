using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
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
    private List<Vector2> b = new List<Vector2>();
    private int count=0;
    private int index;
    public GameObject Portal;
    private void Awake()
    {
        
    }
    public void SpawnEnemiesWithDelay(int index)
    {
        this.index = index;
            int enemyCount = UnityEngine.Random.Range(3, 10);

            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 spawnPosition = randomMap.GetRandomSpawnPosition(index);

                int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);

                GameObject newEnemy = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);

                newEnemy.transform.parent = enemyBox.transform;

            }
        getBoxPoint();
    }
    private void getBoxPoint()
    {
        this.b = randomMap.b;
        count++;
        if (count == b.Count && b != null)
        {
            GameObject spawnedPrefab = Instantiate(Portal, b[index], Quaternion.identity);
            spawnedPrefab.SetActive(false);
            count++;
        }
    }
}