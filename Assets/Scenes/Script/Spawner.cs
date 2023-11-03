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
    static int mapCounting = 0;
    GameObject select = null;
    private Tuple<List<Vector2>, List<Vector2>> spawners;
    private List<Vector2> aList;
    private List<Vector2> bList;



    public void Return_RandomPosition()
    {
        
        for(int i = 0; i < 5; i++)
        {
            Vector2 a = randomMap.GetRandomSpawnPosition(mapCounting);
            Debug.Log(a);
            // 에너미박스 오브젝트의 자식으로 프리팹 추가
            GameObject newEnemy = Instantiate(prefabs[0], a, Quaternion.identity);
            newEnemy.transform.parent = enemyBox.transform; // 에너미박스의 자식으로 설정
        }

        mapCounting++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Return_RandomPosition();
            
        }
    }
}
