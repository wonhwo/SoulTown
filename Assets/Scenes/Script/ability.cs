using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability : MonoBehaviour
{
    int hp = 100;
    Animator animator;
    [SerializeField]
    GameObject gameObject;

    // 유닛이 파괴되었을 때 호출될 콜백 델리게이트
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

        // 유닛 파괴 이벤트를 호출
        OnUnitDestroyed?.Invoke();
    }

}
