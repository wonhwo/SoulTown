using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemyBoxcontroller : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    // ���ʹ� ������Ʈ�� �����ϱ� ���� �迭 �Ǵ� ����Ʈ
    public Transform[] enemies;
    public Tilemap door; // Inspector���� �Ҵ��� Tilemap
    [SerializeField]
    nextUI nextUI;

    private bool hasExecuted = true;

    private void Update()
    {
        findEnemy();
    }

    public void findEnemy()
    {
        enemies = GetComponentsInChildren<Transform>();

        if (enemies.Length > 1)
        {
            door.gameObject.SetActive(true);
            hasExecuted = false; // ������ �����Ǹ� �ٽ� ������ �� �ֵ��� �÷��׸� ����
        }
        else
        {
            door.gameObject.SetActive(false);
            spawner.getBoxPoint();
            if (!hasExecuted) // hasExecuted�� false�� ��쿡�� ����
            {

                nextUI.ShowUI();
                hasExecuted = true; // ������ �����Ǿ��� �� ����Ǿ����� ǥ��
            }
        }
    }

}
