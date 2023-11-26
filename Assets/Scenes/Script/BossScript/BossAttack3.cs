using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3 : MonoBehaviour
{

    [SerializeField]
    private Transform player;

    public void moveA3()
    {
        // 플레이어의 현재 위치를 가져와서 보스어택 오브젝트를 해당 위치로 즉시 이동시킵니다.
        Vector3 playerCurrentPosition = player.position;

        // y 값을 2만큼 올립니다.
        playerCurrentPosition.y += 4f;

        transform.position = playerCurrentPosition;
    }
    public void moveReturn()
    {
        // 보스어택 오브젝트를 원래 위치로 돌아가게 합니다. (X: -0.09, Y: 0.37, Z: 0)
        transform.position = new Vector3(90f, -200.0f, 0);
    }
}
