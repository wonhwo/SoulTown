using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameObject parentOj;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    EnemyStats enemyStats =new EnemyStats();
     int hp;
    int maxHealth;
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    public Image HP;
    private static int damage;

    private bool canTakeDamage = true;
    Transform parentTransform;

    private void Start()
    {
        parentOj = transform.parent.parent.gameObject;
        hp = (int)enemyStats.health;
        maxHealth = (int)enemyStats.health;
    }
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

        // 데미지를 받을 수 있는 상태인 경우에만 처리
        if (canTakeDamage)
        {
            StartCoroutine(TakeDamageWithCooldown());
        }
    }
    private IEnumerator TakeDamageWithCooldown()
    {
        // 데미지를 받을 수 없는 상태로 변경
        canTakeDamage = false;

        // 데미지 처리
        animator.SetTrigger("Hurt");
        Hurt();

        // 0.1초 동안 대기
        yield return new WaitForSeconds(0.01f);

        // 데미지를 받을 수 있는 상태로 변경
        canTakeDamage = true;
    }
    public void Hurt()
    {
        Debug.Log("hurt");
        hp = hp - damage;
        // 현재 체력이 0에서 maxHealth 사이의 값을 가지도록 보정
        int clampedHealth = Mathf.Clamp(hp, 0, maxHealth);

        // fillAmount에 대입할 값 계산 (0에서 1 사이의 값으로 정규화)
        float fillAmount = (float)clampedHealth / maxHealth;
        HP.fillAmount = fillAmount;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(delay);
        Destroy(parentOj);

    }
}
