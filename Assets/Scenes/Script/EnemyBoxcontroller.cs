using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemyBoxcontroller : MonoBehaviour
{
    // 에너미 오브젝트를 참조하기 위한 배열 또는 리스트
    public Transform[] enemies;
    public Tilemap door; // Inspector에서 할당할 Tilemap

    private void Update()
    {
        findEnemy();
    }
    public void findEnemy()
    {
        enemies = GetComponentsInChildren<Transform>();

        if (enemies.Length > 1) // "EnemyBox" 오브젝트까지 카운트하지 않도록 1 이상으로 변경
        {
            door.gameObject.SetActive(true);
        }
        else
        {
            door.gameObject.SetActive(false);
        }
    }
}
