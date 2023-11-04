using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability : MonoBehaviour
{
    int hp = 100;
    Animator animator;
    [SerializeField]
    GameObject gameObject;

    // ������ �ı��Ǿ��� �� ȣ��� �ݹ� ��������Ʈ
    public Action OnUnitDestroyed;

    void Start()
    {
        animator = GetComponent < Animator>();
    }

    void Update()
    {
        if (hp < 1)
        {
            animator.SetTrigger("Death");
            StartCoroutine(DestroyAfterDelay(1.0f));
        }
    }

    public void Hurt()
    {
        hp = hp - 50;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

        // ���� �ı� �̺�Ʈ�� ȣ��
        OnUnitDestroyed?.Invoke();
    }

}
