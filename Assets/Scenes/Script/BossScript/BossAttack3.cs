using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3 : MonoBehaviour
{

    [SerializeField]
    private Transform player;

    public void moveA3()
    {
        // �÷��̾��� ���� ��ġ�� �����ͼ� �������� ������Ʈ�� �ش� ��ġ�� ��� �̵���ŵ�ϴ�.
        Vector3 playerCurrentPosition = player.position;

        // y ���� 2��ŭ �ø��ϴ�.
        playerCurrentPosition.y += 4f;

        transform.position = playerCurrentPosition;
    }
    public void moveReturn()
    {
        // �������� ������Ʈ�� ���� ��ġ�� ���ư��� �մϴ�. (X: -0.09, Y: 0.37, Z: 0)
        transform.position = new Vector3(90f, -200.0f, 0);
    }
}
