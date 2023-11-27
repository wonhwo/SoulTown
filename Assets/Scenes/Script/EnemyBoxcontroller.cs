using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemyBoxcontroller : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    // 에너미 오브젝트를 참조하기 위한 배열 또는 리스트
    public Transform[] enemies;
    public Tilemap door; // Inspector에서 할당할 Tilemap
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
            hasExecuted = false; // 조건이 충족되면 다시 실행할 수 있도록 플래그를 리셋
        }
        else
        {
            door.gameObject.SetActive(false);
            spawner.getBoxPoint();
            if (!hasExecuted) // hasExecuted가 false인 경우에만 실행
            {

                nextUI.ShowUI();
                hasExecuted = true; // 조건이 충족되었을 때 실행되었음을 표시
            }
        }
    }

}
