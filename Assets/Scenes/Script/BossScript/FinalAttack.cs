using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAttack : MonoBehaviour
{
    // �ִϸ��̼��� ������ �ڽ� ������Ʈ���� �迭
    private Animator[] childAnimators;

    void Start()
    {
        // �ڽ� ������Ʈ�� ������ ����ϴ�.
        int childCount = transform.childCount;

        // �ڽ� ������Ʈ�� Animator ������Ʈ�� �迭�� ����ϴ�.
        childAnimators = new Animator[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Animator animator = child.GetComponent<Animator>();

            // ���� �ڽ� ������Ʈ�� Animator ������Ʈ�� �ִٸ� �迭�� �߰��մϴ�.
            if (animator != null)
            {
                childAnimators[i] = animator;
            }
        }
    }
    public void FinalAttackOn()
    {
        foreach (Animator animator in childAnimators)
        {
            animator.SetTrigger("finalAttack");
        }
    }
}
