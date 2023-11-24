using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnergy : MonoBehaviour
{
    private GameObject player;
    public Transform playerTransform; // �÷��̾��� Transform�� �����ϱ� ���� ����
    public float moveSpeed = 2f; // �̵� �ӵ� (�ʴ� �̵� �Ÿ�)
    public float rotationSpeed = 30f; // ȸ�� �ӵ�
    BossController bossController;

    private void Start()
    {
        player = GameObject.Find("UnitRoot");
        bossController = FindObjectOfType<BossController>();
    }

    bool isTriggerOn = false;

    private void Update()
    {
        RotateTowardsPlayer();

        if (!isTriggerOn)
            MoveTowardsPlayer();
    }

    void RotateTowardsPlayer()
    {
        // Z���� �߽����� ȸ��
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardsPlayer()
    {
        // �÷��̾ ���� �̵� ���� ���
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // ������ �÷��̾� �������� �̵�
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Pattern"))
        {
            isTriggerOn = true;
            // õõ�� ���� ��ġ�� �̵�
            StartCoroutine(MoveToBossPosition(bossController.sendTransform().position, 2f));
        }
    }

    IEnumerator MoveToBossPosition(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ�� �̵��� �Ϸ�� �� �߰� �۾��� �� �� �ֽ��ϴ�.
    }
}
