using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SPUM_SpriteList _spriteList;
    private AudioSource damageBGM;
    private GameObject expGameObject;
    private EXPContrallor expContrallor;
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
        damageBGM=GameObject.Find("damageBGM").GetComponent<AudioSource>();
        expGameObject = GameObject.Find("EXPEmptyBar");
        parentOj = transform.parent.parent.gameObject;
        hp = (int)enemyStats.health;
        maxHealth = (int)enemyStats.health;
        expContrallor=expGameObject.GetComponent<EXPContrallor>();
    }
    private bool expCharged = false;
    void Update()
    {
       
        if (hp < 1)
        {
            if (!expCharged)
            {
                if (gameObject.name.Equals("slime(Clone)")) 
                    expContrallor.EXPcharged(gameObject.name); 
                else
                    expContrallor.EXPcharged(transform.parent.parent.name);
            }

            expCharged = true;
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
        if(!gameObject.name.Equals("slime(Clone)")) _spriteList.ToggleTransparency();
        // 0.1�� ���� ���
        yield return new WaitForSeconds(0.1f);
        if (!gameObject.name.Equals("slime(Clone)")) _spriteList.ToggleTransparency();
        // �������� ���� �� �ִ� ���·� ����
        canTakeDamage = true;
    }
    public void Hurt()
    {
        Debug.Log("hurt");
        damageBGM.Play();
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
