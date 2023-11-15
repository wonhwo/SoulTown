using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    int hp = 100;
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    public Image HP;
    private static int damage;

    private bool canTakeDamage = true;
    void Update()
    {
        if (hp < 1)
        {

            StartCoroutine(DestroyAfterDelay(0.5f));
        }
    }
    public void SetDamage(int damageAmount)
    {
        // 변수 전달 받기
        damage = damageAmount;
        animator.SetTrigger("Hurt");
        Hurt();


        // 여기에서 health 변수 사용 또는 다른 동작 수행
    }
    public void Hurt()
    {
        hp = hp - damage;
        HP.fillAmount = (float)hp / 100;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

    }
}
