using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropPostion : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public float spawnProbability = 0.10f; // ���� Ȯ�� (5%)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {

            if (Random.value <= spawnProbability && !gameObject.tag.Equals("barrierBraek"))
            {
                // �ı��Ǹ� 5%�� Ȯ���� ���ο� �������� ���� ��ġ�� ����
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            }
        }
    }
}
