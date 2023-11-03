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
            // ���ʹ̹ڽ� ������Ʈ�� �ڽ����� ������ �߰�
            GameObject newEnemy = Instantiate(prefabs[0], a, Quaternion.identity);
            newEnemy.transform.parent = enemyBox.transform; // ���ʹ̹ڽ��� �ڽ����� ����
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
