using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class camaraMoving : MonoBehaviour
{
    public float moveDistance = 3.0f;
    public float moveDuration = 1000.0f;  // 이동 속도를 느리게 하려면 이 값을 증가시킵니다.

    private Vector2 originalPosition;
    private Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down, new Vector2(1, 1), new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1) };
    private int currentDirectionIndex = 0;
    private Tweener currentTween; // currentTween 변수 선언

    void Start()
    {
        // 초기 위치 저장
        originalPosition = transform.position;

        // 초기 움직임 시작
        MoveInDirection(directions[currentDirectionIndex]);
    }

    void MoveInDirection(Vector2 direction)
    {
        // Z 값 유지
        float originalZ = transform.position.z;

        // 직접 속도를 조절하여 이동
        float distance = Vector2.Distance(transform.position, originalPosition + direction * moveDistance);
        float duration = distance / moveDistance * moveDuration;

        // DOTween을 사용하여 트윈(애니메이션) 적용
        currentTween = transform.DOMove(new Vector3(originalPosition.x + direction.x * moveDistance, originalPosition.y + direction.y * moveDistance, originalZ), duration).OnComplete(() =>
        {
            // 모든 방향을 다 이동한 경우 초기 위치로 돌아옴
            if (currentDirectionIndex == directions.Length - 1)
            {
                currentTween = transform.DOMove(new Vector3(originalPosition.x, originalPosition.y, originalZ), moveDuration).OnComplete(() =>
                {
                    // 초기 위치로 돌아온 후에 다시 첫 번째 방향으로 이동
                    currentDirectionIndex = 0;
                    MoveInDirection(directions[currentDirectionIndex]);
                });
            }
            else
            {
                // 다음 방향으로 이동
                currentDirectionIndex++;
                MoveInDirection(directions[currentDirectionIndex]);
            }
        });
    }

    public void MoveToOriginalPosition()
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill(); // Tween 중지
        }
        // 초기 위치로 돌아감
        transform.DOMove(new Vector3(originalPosition.x, originalPosition.y, -10), 1.0f);
    }
}