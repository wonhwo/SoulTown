using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAttack : MonoBehaviour
{
    // 애니메이션을 실행할 자식 오브젝트들의 배열
    private Animator[] childAnimators;

    void Start()
    {
        // 자식 오브젝트의 개수를 얻습니다.
        int childCount = transform.childCount;

        // 자식 오브젝트의 Animator 컴포넌트를 배열에 담습니다.
        childAnimators = new Animator[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Animator animator = child.GetComponent<Animator>();

            // 만약 자식 오브젝트에 Animator 컴포넌트가 있다면 배열에 추가합니다.
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
