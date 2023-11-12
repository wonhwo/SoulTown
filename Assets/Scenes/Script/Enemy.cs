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
        // ������ ���� �� ���� ���·� ����
        canTakeDamage = false;
        animator.SetTrigger("Hurt");
        ability.Hurt();
        // 0.5�� ���
        yield return new WaitForSeconds(0.5f);

        // 0.5�� �Ŀ� ����� ����
        
        // �ٸ� Hurt �޼��� ȣ��
        

        // ������ ���� �� �ִ� ���·� ����
        canTakeDamage = true;
    }

}
