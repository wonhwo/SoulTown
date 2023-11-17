using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemyBoxcontroller : MonoBehaviour
{
    // ���ʹ� ������Ʈ�� �����ϱ� ���� �迭 �Ǵ� ����Ʈ
    public Transform[] enemies;
    public Tilemap door; // Inspector���� �Ҵ��� Tilemap

    private void Update()
    {
        findEnemy();
    }
    public void findEnemy()
    {
        enemies = GetComponentsInChildren<Transform>();

        if (enemies.Length > 1) // "EnemyBox" ������Ʈ���� ī��Ʈ���� �ʵ��� 1 �̻����� ����
        {
            door.gameObject.SetActive(true);
        }
        else
        {
            door.gameObject.SetActive(false);
        }
    }
}
