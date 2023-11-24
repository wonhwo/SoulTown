using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnergy : MonoBehaviour
{
    private GameObject player;
    public Transform playerTransform; // 플레이어의 Transform을 저장하기 위한 변수
    public float moveSpeed = 2f; // 이동 속도 (초당 이동 거리)
    public float rotationSpeed = 30f; // 회전 속도
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
        // Z축을 중심으로 회전
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardsPlayer()
    {
        // 플레이어를 향해 이동 방향 계산
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // 보스를 플레이어 방향으로 이동
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
            // 천천히 보스 위치로 이동
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

        // 보스 위치로 이동이 완료된 후 추가 작업을 할 수 있습니다.
    }
}
