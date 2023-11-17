using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class camaraMoving : MonoBehaviour
{
    public float moveDistance = 3.0f;
    public float moveDuration = 1000.0f;  // �̵� �ӵ��� ������ �Ϸ��� �� ���� ������ŵ�ϴ�.

    private Vector2 originalPosition;
    private Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down, new Vector2(1, 1), new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1) };
    private int currentDirectionIndex = 0;
    private Tweener currentTween; // currentTween ���� ����

    void Start()
    {
        // �ʱ� ��ġ ����
        originalPosition = transform.position;

        // �ʱ� ������ ����
        MoveInDirection(directions[currentDirectionIndex]);
    }

    void MoveInDirection(Vector2 direction)
    {
        // Z �� ����
        float originalZ = transform.position.z;

        // ���� �ӵ��� �����Ͽ� �̵�
        float distance = Vector2.Distance(transform.position, originalPosition + direction * moveDistance);
        float duration = distance / moveDistance * moveDuration;

        // DOTween�� ����Ͽ� Ʈ��(�ִϸ��̼�) ����
        currentTween = transform.DOMove(new Vector3(originalPosition.x + direction.x * moveDistance, originalPosition.y + direction.y * moveDistance, originalZ), duration).OnComplete(() =>
        {
            // ��� ������ �� �̵��� ��� �ʱ� ��ġ�� ���ƿ�
            if (currentDirectionIndex == directions.Length - 1)
            {
                currentTween = transform.DOMove(new Vector3(originalPosition.x, originalPosition.y, originalZ), moveDuration).OnComplete(() =>
                {
                    // �ʱ� ��ġ�� ���ƿ� �Ŀ� �ٽ� ù ��° �������� �̵�
                    currentDirectionIndex = 0;
                    MoveInDirection(directions[currentDirectionIndex]);
                });
            }
            else
            {
                // ���� �������� �̵�
                currentDirectionIndex++;
                MoveInDirection(directions[currentDirectionIndex]);
            }
        });
    }

    public void MoveToOriginalPosition()
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill(); // Tween ����
        }
        // �ʱ� ��ġ�� ���ư�
        transform.DOMove(new Vector3(originalPosition.x, originalPosition.y, -10), 1.0f);
    }
}