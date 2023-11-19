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
        // ���� ���� �ޱ�
        damage = damageAmount;

        // �������� ���� �� �ִ� ������ ��쿡�� ó��
        if (canTakeDamage)
        {
            StartCoroutine(TakeDamageWithCooldown());
        }
    }
    private IEnumerator TakeDamageWithCooldown()
    {
        // �������� ���� �� ���� ���·� ����
        canTakeDamage = false;

        // ������ ó��
        animator.SetTrigger("Hurt");
        Hurt();

        // 0.1�� ���� ���
        yield return new WaitForSeconds(0.01f);

        // �������� ���� �� �ִ� ���·� ����
        canTakeDamage = true;
    }
    public void Hurt()
    {
        Debug.Log("hurt");
        hp = hp - damage;
        // ���� ü���� 0���� maxHealth ������ ���� �������� ����
        int clampedHealth = Mathf.Clamp(hp, 0, maxHealth);

        // fillAmount�� ������ �� ��� (0���� 1 ������ ������ ����ȭ)
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
