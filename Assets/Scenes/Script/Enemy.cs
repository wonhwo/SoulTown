using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    EnemyStats enemyStats =new EnemyStats();
     int hp;
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    public Image HP;
    private static int damage;

    private bool canTakeDamage = true;
    private void Start()
    {
        hp = (int)enemyStats.health;
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
        animator.SetTrigger("Hurt");
        Hurt();


        // ���⿡�� health ���� ��� �Ǵ� �ٸ� ���� ����
    }
    public void Hurt()
    {
        hp = hp - damage;
        HP.fillAmount = (float)hp / 100;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

    }
}
