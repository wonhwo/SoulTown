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
    private GameObject select = null;
    private Tuple<List<Vector2>, List<Vector2>> spawners;
    private List<Vector2> aList;
    private List<Vector2> bList;
    public Ability ability;
    public GameObject door;
    private int enemy = 0;

    private void Awake()
    {
        ability = GetComponent<Ability>();
    }

    public void ReturnRandomPosition(int index)
    {
        enemy = UnityEngine.Random.Range(3, 10);

        for (int i = 0; i < enemy; i++)
        {
            Vector2 a = randomMap.GetRandomSpawnPosition(index);
            Debug.Log(a);
            // 에너미박스 오브젝트의 자식으로 프리팹 추가
            GameObject newEnemy = Instantiate(prefabs[0], a, Quaternion.identity);
            newEnemy.transform.parent = enemyBox.transform; // 에너미박스의 자식으로 설정
        }
    }

    public void EnemyCounter()
    {
        if (ability != null)
        {
            int ec = ability.SendEnemyCount();
            Debug.Log(ec);
            if (ec != enemy)
            {
                Debug.Log("test");
            }
        }
        else
        {
            Debug.Log("ability is null");
        }
    }

    private void Update()
    {
        EnemyCounter();
    }
}