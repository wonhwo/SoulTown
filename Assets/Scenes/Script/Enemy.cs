using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ability ability;

    private bool canTakeDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon") && canTakeDamage)
        {
            StartCoroutine(HurtWithDelay());
        }
    }

    private IEnumerator HurtWithDelay()
    {
        // 공격을 받을 수 없는 상태로 변경
        canTakeDamage = false;
        animator.SetTrigger("Hurt");
        ability.Hurt();
        // 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        // 0.5초 후에 실행될 내용
        
        // 다른 Hurt 메서드 호출
        

        // 공격을 받을 수 있는 상태로 변경
        canTakeDamage = true;
    }

}
